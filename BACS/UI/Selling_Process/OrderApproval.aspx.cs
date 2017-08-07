
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class OrderApproval : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
         private string uid = string.Empty;
        static string prddtlid = "";
        static string Orderid = "";
        static string partyID = "";

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (Session["UserID"] != null)
            {
                uid = Session["UserID"].ToString();
            }
             
            if (!IsPostBack)
            {
                LoadGrid();
              //  LoadParty();
                LoadPartyWiseApprovedOrder();
                divorderdetails.Visible = false;
                txtGrandTotal.Text = "0";
            }
        }

        //protected T FromXml<T>(String xml)
        //{
        //    T returnedXmlClass = default(T);

        //    try
        //    {
        //        using (TextReader reader = new StringReader(xml))
        //        {
        //            try
        //            {
        //                returnedXmlClass =
        //                    (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        //            }
        //            catch (InvalidOperationException)
        //            {
        //                // String passed is not XML, simply return defaultXmlClass
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return returnedXmlClass;
        //}
        [WebMethod]
        public static List<string> GetAutoCompleteData(string Party)
        {
            // string dealer = txtDealer.Text;

            DatabaseAccess dba = new DatabaseAccess();
            List<string> Dealers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = dba.getconnectionSting();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select t.PartyID,lu.UserName,t.PartyName,lu.UserName + '  =  '  + t.PartyName  as Party,AreaID from PartyMaster t,Login_Users lu 
                           where t.PartyID=lu.UserID and t.IsActive='Y' and lu.UserName + '  =  '  + t.PartyName LIKE '%'+@SearchText+'%' order by lu.UserName";
                    cmd.Parameters.AddWithValue("@SearchText", Party);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dealers.Add(string.Format("{0}//{1}", dr["Party"], dr["PartyID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return Dealers;

        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.OrderID,t.OrderNo,t.Date,convert(float,t.TotalAmount) as TotalAmount,p.PartyName,t.PartyID from Local_Order_Master t 
                left outer join PartyMaster p on t.PartyID=p.PartyID
                where t.IsApprove='N' order by t.OrderNo desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvOrder.DataSource = ds;
                gvOrder.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void LoadOrderDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @" select t.OrderDetailID,t.OrderID,t.ProdID,t.Qty,t.ApprovedQty,t.QtyReceived,CONVERT(float,t.Price) as Price,CONVERT(float,t.Total) as Total,t.ApprovedTotalAmnt,t.ApprovedDiscount,CONVERT(float,t.Discount) as Discount,p.ProdName from Local_Order_Master_Details t 
               left outer join Product_Master p on t.ProdID=p.ProdID
               where t.OrderID='" + Orderid + "'";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvOrderDetails.DataSource = ds;
                gvOrderDetails.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        //protected void LoadParty()
        //{

        //    DataTable dt;
        //    DataSet ds = dba.GetDataSet("EXEC get_dealer ''", "ConnDB230");
        //    if (ds != null)
        //    {
        //        dt = ds.Tables[0];
        //        forDdl.LoadDll(ddlPartyName, dt, "PartyID", "Party", "Select Dealer");
        //    }
        //    else
        //    {

        //        ddlPartyName.Items.Clear();
        //        ddlPartyName.Items.Add(new ListItem("No Dealer Available", "0"));

        //    }
        //}
        private void LoadPartyWiseApprovedOrder()
        {
            partyID = HIDDealerID.Value;
            try
            {
                String query = "";
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(partyID))
                {
                    query = @"select t.ApprovedQty,convert(float,t.ApprovedTotalAmnt) as ApprovedTotalAmnt,u.UserName as approvedby,t.Date as orderdate,t.OrderNo,t.OrderQty,convert(float,t.TotalAmount) as orderamount,p.PartyName from Local_Order_Master t 
                left outer join PartyMaster p on t.PartyID=p.PartyID
                left outer join Login_Users u on t.ApproveBy=u.LogUserID
                where t.IsApprove='Y' 
                and t.PartyID='" + partyID + "' order by t.OrderNo desc";
                }
                else
                {
                    query = @" select t.ApprovedQty,convert(float,t.ApprovedTotalAmnt) as ApprovedTotalAmnt,u.UserName as approvedby,t.Date as orderdate,t.OrderNo,t.OrderQty,convert(float,t.TotalAmount) as orderamount,p.PartyName from Local_Order_Master t 
                left outer join PartyMaster p on t.PartyID=p.PartyID
                left outer join Login_Users u on t.ApproveBy=u.LogUserID
                where t.IsApprove='Y' order by t.OrderNo desc";
                }
                ds = dba.GetDataSet(query, "ConnDB230");
                grvApprovedOrder.DataSource = ds;
                grvApprovedOrder.DataBind();

            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void gvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvOrder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
           
            Orderid = gvOrder.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");
           // partyID = gvOrder.Rows[e.NewSelectedIndex].Cells[6].Text.Replace("&nbsp;", "");
            LoadOrderDetails();
           // LoadPartyWiseApprovedOrder();
            divorderdetails.Visible = true;
            txtGrandTotal.Text = gvOrder.Rows[e.NewSelectedIndex].Cells[3].Text.Replace("&nbsp;", "");
        }
        protected string nullChecker(string str)
        {
            string outut = string.Empty;
            if (str != "&nbsp;" && str != "")
            {
                outut = str;
            }
            else
            {
                outut = "0";
            }
            return outut;
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                int rowindex = 0;
                rowindex = currentRow.RowIndex;

                TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
                Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));

                TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));

                Int32 PendingQuantiity = Convert.ToInt32(nullChecker(currentRow.Cells[7].Text));

                if (Quantity > PendingQuantiity)
                {
                    txtQuantity.Text = Convert.ToString(PendingQuantiity);
                    txtQuantity.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Order Quantity');", true);
                }

                else
                {

                    Label lbltotal = (Label)currentRow.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                    Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));

                    double vdiscount = 0;
                    vdiscount = (Discount / 100) * Price * Quantity;

                    total = Quantity * Price - vdiscount;
                    lbltotal.Text = Convert.ToString(total);

                    decimal GrandTotal = 0;
                    foreach (GridViewRow row in gvOrderDetails.Rows)
                    {
                        Label Label1 = (Label)row.FindControl("lbltotal");

                        decimal vtotal = 0;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            if (Label1.Text != "")
                            {
                                vtotal = Convert.ToDecimal(Label1.Text.Trim());
                            }
                            GrandTotal = GrandTotal + vtotal;

                        }
                    }
                    txtGrandTotal.Text = GrandTotal.ToString();
                }
            }
            catch
            {

            }

        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

            TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
            Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
            if (!(Convert.ToDecimal(txtDiscount.Text.ToString()) < 100))
            {
                txtDiscount.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Discount should less than 100% ...');", true);
            }
            else
            {
                try
                {


                    TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                    Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));

                    Label lbltotal = (Label)currentRow.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                    Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));


                    double vdiscount = 0;
                    vdiscount = (Discount / 100) * Price * Quantity;

                    total = Quantity * Price - vdiscount;
                    lbltotal.Text = Convert.ToString(total);

                    decimal GrandTotal = 0;
                    foreach (GridViewRow row in gvOrderDetails.Rows)
                    {
                        Label Label1 = (Label)row.FindControl("lbltotal");

                        decimal vtotal = 0;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            if (Label1.Text != "")
                            {
                                vtotal = Convert.ToDecimal(Label1.Text.Trim());
                            }
                            GrandTotal = GrandTotal + vtotal;

                        }
                    }
                    txtGrandTotal.Text = GrandTotal.ToString();

                }
                catch
                {

                }
            }
           

        }
        private void Clear()
        {
            txtGrandTotal.Text = string.Empty;
            gvOrderDetails.DataSource = null;
            gvOrderDetails.DataBind(); 
            LoadGrid();
        }
       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

                 string resOut = string.Empty;

                        string addedby = uid;
                        Int32 totalQTY = 0;
                        try
                        {
                            foreach (GridViewRow row in gvOrderDetails.Rows)
                            {
                                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                                totalQTY += Quantity;
                            }
                            double ApprovedTotl = Convert.ToDouble(txtGrandTotal.Text.ToString());

                            string result = "";
                          
                                string qryOrder =
                                 @"update Local_Order_Master 
                                 set IsApprove='Y',ApprovedTotalAmnt='" + ApprovedTotl + "',ApprovedQty='" + totalQTY + "',ApproveBy='" + uid + "' where OrderID= '" + Orderid + "'";
                                 result = dba.ExecuteQuery(qryOrder, "ConnDB230");
                          
                            if (result == "1")
                            {

                                foreach (GridViewRow row in gvOrderDetails.Rows)
                                {
                                    string OrderDetailsID = nullChecker(row.Cells[6].Text);
                                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                                    Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                                    Label lbltotal = (Label)row.FindControl("lbltotal");
                                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                                    Label lblPrice = (Label)row.FindControl("lblPrice");
                                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                                     TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                                     Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                                    string QryOrderDetail = @"update Local_Order_Master_Details 
                                    set ApprovedQty='" + Quantity + "',ApprovedTotalAmnt='" + total + "',ApprovedDiscount='" + Discount + "'  where OrderDetailID='" + OrderDetailsID + "' and OrderID='" + Orderid + "'";

                                    resOut = dba.ExecuteQuery(QryOrderDetail, "ConnDB230");
                                }

                            }
                        }
                        catch
                        {

                        }
                        if (resOut == "1")
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Approved...');",
                                true);
                            Clear();
                            LoadPartyWiseApprovedOrder();
                        //    SendSMS();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Approved...');",
                                true);
                        }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

         
            string addedby = uid;
            try
            {
                double ApprovedTotl = Convert.ToDouble(txtGrandTotal.Text.ToString());
                string qryOrder =
                     @"update Local_Order_Master  
                              set IsApprove='X',ApproveBy='" + uid + "' where OrderID= '" + Orderid + "'";
                string result = dba.ExecuteQuery(qryOrder, "ConnDB230");
                if (result == "1")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Cancelled...');",
                        true);
                    Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Cancelled...');",
                        true);
                }
            }
            catch
            {

            }
           
        }

        protected void grvApprovedOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvApprovedOrder.PageIndex = e.NewPageIndex;
            LoadPartyWiseApprovedOrder();
        }

        //protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadPartyWiseApprovedOrder();
        //}
        protected void btnDealerchange_Click(object sender, EventArgs e)
        {
            //LoadDetailsGrid(HIDDealerID.Value);
            LoadPartyWiseApprovedOrder();

        }
    }
}