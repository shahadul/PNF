
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class InvoiceApproval : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;
        static string OrderId = "";
        static string Invoiceid = "";
        static string partyID = "";
        static string PartyCode = "";
        static string PartyName = "";
        static string PartyMobile = "";
        static string asmMobile = "";
        static string dsmMobile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                uid = Session["UserID"].ToString();
            }
            if (!string.IsNullOrEmpty(Session["Message"].ToString()))
            {
                string msg = Session["Message"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('" + msg + "');", true);
                Session["Message"] = string.Empty;
            }
            if (!IsPostBack)
            {
                LoadGrid();
               // LoadParty();
                LoadPartyWiseApprovedInvoice();
                LoadPartyWiseDeliveredOrder();
                divInvoicedetails.Visible = false;
                txtGrandTotal.Text = "0";
               
            }
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
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.InvID,t.InvNumber,t.InvDate,convert(float,t.Total) as Total,t.OrderID,t.PartyID,pm.PartyCode,pm.PartyName,pm.OwnerContactPhone1 as Partymobile,asm.PhonePersonal as asmMobile,dsm.PhonePersonal as dsmMobile
                from InvoiceMaster t
                left join PartyMaster pm on t.PartyID=pm.PartyID
                left join AreaSalesManMaster asm on pm.AreaSalesManID=asm.AreaSalesManID
                left join DivisionalSalesManMaster dsm on asm.DivisionalSalesManID=dsm.DivisionalSalesManID
                where t.IsApprove='N' and t.IsCancel <>'Y' order by t.InvID DESC";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvInvoice.DataSource = ds;
                gvInvoice.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void LoadInvoiceDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.InvDetailID,t.InvID,t.ProdID,t.Quantity,convert(float,t.UnitPrice) as UnitPrice,CONVERT(float,t.Discount) as Discount,convert(float,t.Total) as Total,t.ApprovedQty,t.ApprovedTotal,CONVERT(float,t.ApprovedDiscount) as ApprovedDiscount,p.ProdName from InvoiceDetails t
                                left join Product_Master p on t.ProdID=p.ProdID
                                where t.InvID='" + Invoiceid + "' order by t.InvDetailID";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvInvoiceDetails.DataSource = ds;
                gvInvoiceDetails.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }

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
        private void LoadPartyWiseApprovedInvoice()
        {
            partyID = HIDDealerID.Value;
            try
            {
                String query = "";
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(partyID))
                {

                    query = @"select t.InvNumber,t.InvID,t.InvDate,t.InvoiceQty,t.ApprovedQty as masterApprovedQty,convert(float,t.ApprovedAmount) as ApprovedAmount
                    ,p.PartyName,a.AreaName,lom.DeliveryStatus,t.IsChallan,t.OrderID,sum(isnull(id.ApprovedQty,0)) as ApprovedQty from InvoiceMaster t 
                    left outer join PartyMaster p on t.PartyID=p.PartyID
                    left outer join AreaMaster a on p.AreaID=a.AreaID
                    left outer join Local_Order_Master lom on t.OrderID=lom.OrderID 
                    left outer join InvoiceDetails id on t.InvID=id.InvID
                    where 
                    t.PartyID='" + partyID + "' and t.IsApprove='Y' and t.IsCancel <>'Y' group by t.InvID,t.InvNumber,t.InvDate,t.InvoiceQty,t.ApprovedQty,ApprovedAmount,p.PartyName,a.AreaName,lom.DeliveryStatus,t.IsChallan,t.OrderID order by t.InvID DESC";
                }
                else
                {

                    query = @"select t.InvNumber,t.InvID,t.InvDate,t.InvoiceQty,t.ApprovedQty as masterApprovedQty,convert(float,t.ApprovedAmount) as ApprovedAmount
                    ,p.PartyName,a.AreaName,lom.DeliveryStatus,t.IsChallan,t.OrderID,sum(isnull(id.ApprovedQty,0)) as ApprovedQty from InvoiceMaster t 
                    left outer join PartyMaster p on t.PartyID=p.PartyID
                    left outer join AreaMaster a on p.AreaID=a.AreaID
                    left outer join Local_Order_Master lom on t.OrderID=lom.OrderID 
                    left outer join InvoiceDetails id on t.InvID=id.InvID
                    where 

                    t.IsApprove='Y' 
                    and t.IsCancel <>'Y' 
 
                    group by t.InvID,t.InvNumber,t.InvDate,t.InvoiceQty,t.ApprovedQty,ApprovedAmount,p.PartyName,a.AreaName,lom.DeliveryStatus,t.IsChallan,t.OrderID
                    order by t.InvID DESC";
                }
                ds = dba.GetDataSet(query, "ConnDB230");
                grvApprovedInvoice.DataSource = ds;
                grvApprovedInvoice.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void LoadPartyWiseDeliveredOrder()
        {
            partyID = HIDDealerID.Value;
            try
            {
                String query = "";
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(partyID))
                {

                    query = @"select t.InvNumber,t.InvID,t.InvDate,t.InvoiceQty,t.ApprovedQty as masterApprovedQty,convert(float,t.ApprovedAmount) as ApprovedAmount
                    ,p.PartyName,a.AreaName,lom.DeliveryStatus,t.OrderID,sum(isnull(id.ApprovedQty,0)) as ApprovedQty from InvoiceMaster t 
                    left outer join PartyMaster p on t.PartyID=p.PartyID
                    left outer join AreaMaster a on p.AreaID=a.AreaID
                    left outer join Local_Order_Master lom on t.OrderID=lom.OrderID 
                    left outer join InvoiceDetails id on t.InvID=id.InvID
                    where t.PartyID='" + partyID + "' and t.IsApprove='Y' and t.IsCancel <>'Y' and lom.DeliveryStatus='Y'  group by t.InvID,t.InvNumber,t.InvDate,t.InvoiceQty,t.ApprovedQty,ApprovedAmount,p.PartyName,a.AreaName,lom.DeliveryStatus,t.OrderID order by t.InvID DESC";
                }
                else
                {
                  
                    query = @"select t.InvNumber,t.InvID,t.InvDate,t.InvoiceQty,t.ApprovedQty as masterApprovedQty,convert(float,t.ApprovedAmount) as ApprovedAmount
                    ,p.PartyName,a.AreaName,lom.DeliveryStatus,t.OrderID,sum(isnull(id.ApprovedQty,0)) as ApprovedQty from InvoiceMaster t 
                    left outer join PartyMaster p on t.PartyID=p.PartyID
                    left outer join AreaMaster a on p.AreaID=a.AreaID
                    left outer join Local_Order_Master lom on t.OrderID=lom.OrderID 
                    left outer join InvoiceDetails id on t.InvID=id.InvID
                    where t.IsApprove='Y' and t.IsCancel <>'Y' and lom.DeliveryStatus='Y' 
                    group by t.InvID,t.InvNumber,t.InvDate,t.InvoiceQty,t.ApprovedQty,ApprovedAmount,p.PartyName,a.AreaName,lom.DeliveryStatus,t.OrderID
                    order by t.InvID DESC";
                }
                ds = dba.GetDataSet(query, "ConnDB230");

                grvDeliverOrder.DataSource = ds;
                grvDeliverOrder.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void gvInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoice.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvInvoice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            Invoiceid = gvInvoice.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");
            OrderId = gvInvoice.Rows[e.NewSelectedIndex].Cells[4].Text.Replace("&nbsp;", "");
         
            LoadInvoiceDetails();
         
            divInvoicedetails.Visible = true;
            txtGrandTotal.Text = gvInvoice.Rows[e.NewSelectedIndex].Cells[3].Text.Replace("&nbsp;", "");
            PartyCode = gvInvoice.Rows[e.NewSelectedIndex].Cells[7].Text.Replace("&nbsp;", "");
            PartyName = gvInvoice.Rows[e.NewSelectedIndex].Cells[8].Text.Replace("&nbsp;", "");
            PartyMobile = gvInvoice.Rows[e.NewSelectedIndex].Cells[9].Text.Replace("&nbsp;", "");
            asmMobile = gvInvoice.Rows[e.NewSelectedIndex].Cells[10].Text.Replace("&nbsp;", "");
            dsmMobile = gvInvoice.Rows[e.NewSelectedIndex].Cells[11].Text.Replace("&nbsp;", "");
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

                TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));


                Int32 PendingQuantiity = Convert.ToInt32(nullChecker(currentRow.Cells[7].Text));
                if (Quantity < 1)
                {
                    txtQuantity.Text = Convert.ToString(PendingQuantiity);
                    txtQuantity.Focus();                  
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept Zero Quantity');", true);

                }
               else if (Quantity > PendingQuantiity)
                {
                    txtQuantity.Text = Convert.ToString(PendingQuantiity);
                    txtQuantity.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Invoice Quantity');", true);
                }

                else
                {

                    Label lbltotal = (Label)currentRow.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                    Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                    TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
                    Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                    total = (Quantity * Price)-(Quantity*Discount);
                    lbltotal.Text = Convert.ToString(total);

                    double vdiscount = 0;
                    vdiscount = (Discount / 100) * Price * Quantity;

                    total = Quantity * Price - vdiscount;
                    lbltotal.Text = Convert.ToString(total);

                    decimal GrandTotal = 0;
                    foreach (GridViewRow row in gvInvoiceDetails.Rows)
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
                    foreach (GridViewRow row in gvInvoiceDetails.Rows)
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
            gvInvoiceDetails.DataSource = null;
            gvInvoiceDetails.DataBind();
            LoadGrid();
        }
        public void SendSMS(double TTLAmnt)
        {
            //PartyMobile = "01755654247";
            //asmMobile = String.Empty;
            //dsmMobile = String.Empty;
            string date = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string smsdtl = @"Dear Valued Customer. Thank You for Purchasing from Power+" + Environment.NewLine + "--Your Details--" + Environment.NewLine + PartyCode + "," + Environment.NewLine + PartyName + "," + Environment.NewLine + "Inv No:" + Invoiceid + "," + Environment.NewLine + "TTL AMN:" + TTLAmnt + "," + Environment.NewLine + "Date:" + date;

            String sid = "FDLElectronics"; String user = "fdlelectronics"; String pass = "74X878m>";
            string URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

            string myParameters = String.Empty;

            if (!string.IsNullOrEmpty(PartyMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + PartyMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    string s = myParameters;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
                }
            }
            if (!string.IsNullOrEmpty(asmMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + asmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    string s = myParameters;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
                }
            }

            if (!string.IsNullOrEmpty(dsmMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + dsmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    string s = myParameters;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string resOut = string.Empty;
            Int32 totalQTY=0;
            double totalapprovedamunt = 0;
            string addedby = uid;
            try
            {
                foreach (GridViewRow row in gvInvoiceDetails.Rows)
                {
                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                    Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                    Label lbltotal = (Label)row.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                    totalapprovedamunt += total;
                    totalQTY += Quantity;
                }
                string qryOrder =
                     @"update InvoiceMaster 
                              set IsApprove='Y',ApprovedBy='" + uid + "',ApprovedQty='" + totalQTY + "',ApprovedAmount='" + totalapprovedamunt + "' where InvID= '" + Invoiceid + "'";
                string result = dba.ExecuteQuery(qryOrder, "ConnDB230");


                if (result == "1")
                {

                    foreach (GridViewRow row in gvInvoiceDetails.Rows)
                    {
                        string InvDetailID = nullChecker(row.Cells[6].Text);
                        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                        Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                        TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                        Double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                        Label lbltotal = (Label)row.FindControl("lbltotal");
                        double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                        Label lblPrice = (Label)row.FindControl("lblPrice");
                        double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                        int vprodId = Convert.ToInt32(nullChecker(row.Cells[5].Text));

                        string QryOrderDetail = @"update InvoiceDetails 
                                    set ApprovedQty='" + Quantity + "',ApprovedDiscount='"+Discount+"',ApprovedTotal='" + total + "' where InvDetailID='" + InvDetailID + "' and InvID='" + Invoiceid + "'";

                        resOut = dba.ExecuteQuery(QryOrderDetail, "ConnDB230");

//                        int UpdatedQty = 0;
//                        string StockQty = "select StockQty from IStock where ProdID='" + vprodId + "'";
//                        DataTable dt = dba.GetDataTable(StockQty);
//                        if (dt != null && dt.Rows.Count > 0)
//                        {
//                            UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) - Quantity;
//                            string QryIStock = @"update IStock 
//                                    set StockQty='" + UpdatedQty + "' where ProdID='" + vprodId + "'";
//                             dba.ExecuteQuery(QryIStock, "ConnDB230");

//                        }
                    }

                }


            }
            catch
            {

            }
          
            if (resOut == "1")
            {
                SendSMS(totalapprovedamunt);
                Clear();
                LoadPartyWiseApprovedInvoice();
                Session["Message"] = "Data Approved...";
                
            }
            else
            {
                Session["Message"] = "Not Approved...";
             
            }
            Response.Redirect("InvoiceApproval.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {


            string addedby = uid;
            try
            {
                double ApprovedTotl = Convert.ToDouble(txtGrandTotal.Text.ToString());
                string qryOrder =
                     @"update InvoiceMaster  
                              set IsApprove='X',ApprovedBy='" + uid + "',IsCancel='Y',CanceldBy='" + uid + "' where InvID= '" + Invoiceid + "'";
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

        protected void grvApprovedInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvApprovedInvoice.PageIndex = e.NewPageIndex;
            LoadPartyWiseApprovedInvoice();
        }

        //protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadPartyWiseApprovedInvoice();
        //    LoadPartyWiseDeliveredOrder();
        //    LoadInvoiceNumber();
        //}
         protected void LoadInvoiceNumber()
        {
             partyID = HIDDealerID.Value;
            string qry;
            qry = "EXEC Get_Number_list 'InvoiceMaster','" + partyID + "','','','Y'";

            DataTable dt = dba.GetDataTable(qry);
            if (dt != null)
            {
                forDdl.LoadDll(ddlInvoiceNumber, dt, "InvID", "InvNumber", "Select Challan Number");
            }
            else
            {
                ddlInvoiceNumber.Items.Clear();
                ddlInvoiceNumber.Items.Add(new ListItem("Challan Number not Available", "0"));

            }

        
        }

         protected void btnChallan_Click(object sender, EventArgs e)
         {
             string NameValueParam = "Challan";
             if (ddlInvoiceNumber.SelectedValue == "" || ddlInvoiceNumber.SelectedIndex==0)
             {

                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Challan Number ...');",
                     true);
             }
             else
             {
                 Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + ddlInvoiceNumber.SelectedValue); 
             }
             
         }
         protected void btnInvoice_Click(object sender, EventArgs e)
         {
             string NameValueParam = "Invoice";
             if (ddlInvoiceNumber.SelectedValue == "" || ddlInvoiceNumber.SelectedIndex == 0)
             {

                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Invoice Number ...');",
                     true);
             }
             else
             {
                 Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + ddlInvoiceNumber.SelectedValue);
             }

         }

         protected void grvApprovedInvoice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
         {
             LoadInvoiceNumber();
             Invoiceid = grvApprovedInvoice.Rows[e.NewSelectedIndex].Cells[9].Text.Replace("&nbsp;", "");
             ddlInvoiceNumber.SelectedValue = Invoiceid;
         }
         protected void chkselect_Click(object sender, EventArgs e)
         {

         }

         protected void btnDeliver_Click(object sender, EventArgs e)
         {
            // string CurrentDate = DateTime.Now.ToString();
             string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
             int checkcount = 0;

             foreach (GridViewRow row in grvApprovedInvoice.Rows)
             {
                 CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                 if (vchkselect.Checked)
                 {
                     checkcount = 1;
                     break;

                 }
             }

             if (checkcount == 1)
             {
                 string vOrderID = string.Empty;
               
                 string resultdtl = string.Empty;
                 foreach (GridViewRow row in grvApprovedInvoice.Rows)
                 {
                     if (row.RowType == DataControlRowType.DataRow)
                     {
                         CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                         if (vchkselect.Checked)
                         {
                             try
                             {
                              vOrderID = row.Cells[10].Text.Trim();
                              string query = "update Local_Order_Master  set DeliveryStatus='Y',DeliveryDate='" + CurrentDate + "' where OrderID='" + vOrderID + "' ";
                              resultdtl = dba.ExecuteQuery(query, "ConnDB230");

                             }

                             catch (Exception ex)
                             {
                                 throw new Exception();
                             }

                         }

                     }

                 }



                 if (resultdtl != "" && resultdtl != "1")
                 {
                    
                 }
                 else
                 {
                     Clear();
                     LoadPartyWiseApprovedInvoice();
                     LoadPartyWiseDeliveredOrder();
                     ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Delivered...');", true);
                 }

             }
             else
             {
               
                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Then Try Again...');", true);
             }
         }

         protected void grvDeliverOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {
             grvDeliverOrder.PageIndex = e.NewPageIndex;
             LoadPartyWiseDeliveredOrder();
         }

         protected void grvDeliverOrder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
         {
             LoadInvoiceNumber();
             Invoiceid = grvDeliverOrder.Rows[e.NewSelectedIndex].Cells[8].Text.Replace("&nbsp;", "");
             ddlInvoiceNumber.SelectedValue = Invoiceid;
         }

         protected void btnCancelNow_Click(object sender, EventArgs e)
         {
             DataSet ds = new DataSet();
             int checkcount = 0;

             foreach (GridViewRow row in grvApprovedInvoice.Rows)
             {
                 CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                 if (vchkselect.Checked)
                 {
                     checkcount = 1;
                     break;

                 }
             }

             if (checkcount == 1)
             {
                 string vInvID = string.Empty;

                 string resultdtl = string.Empty;
                 foreach (GridViewRow row in grvApprovedInvoice.Rows)
                 {
                     if (row.RowType == DataControlRowType.DataRow)
                     {
                         CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                         if (vchkselect.Checked)
                         {
                             try
                             {
                                 vInvID = row.Cells[9].Text.Trim();
                                 string query = "update InvoiceMaster  set IsCancel='Y' where InvID='" + vInvID + "' ";
                                 resultdtl = dba.ExecuteQuery(query, "ConnDB230");
                                 
                                 String querydetail = @"select t.InvDetailID,t.InvID,t.ProdID,t.Quantity from InvoiceDetails t                                
                                where t.InvID='" + Invoiceid + "' order by t.InvDetailID";
                                 ds = dba.GetDataSet(querydetail, "ConnDB230");
                                 if (ds != null)
                                 {
                                     foreach (DataRow rows in ds.Tables[0].Rows)
                                     {
                                         int UpdatedQty = 0;
                                         Int32 vprodId = Convert.ToInt32(rows[2]);
                                         Int32 vprodqty = Convert.ToInt32(rows[3]);
                                         string StockQty = "select StockQty from IStock where ProdID='" + vprodId + "'";

                                         DataTable dt = dba.GetDataTable(StockQty);
                                         if (dt != null && dt.Rows.Count > 0)
                                         {
                                             UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) + vprodqty;
                                             string QryIStock = @"update IStock 
                                    set StockQty='" + UpdatedQty + "' where ProdID='" + vprodId + "'";
                                             dba.ExecuteQuery(QryIStock, "ConnDB230");

                                         } 
                                     }
                                 }
                                 
                             }

                             catch (Exception ex)
                             {
                                 throw new Exception();
                             }

                         }

                     }

                 }
                 if (resultdtl != "" && resultdtl != "1")
                 {

                 }
                 else
                 {
                     Clear();
                     LoadPartyWiseApprovedInvoice();
                     LoadPartyWiseDeliveredOrder();
                     ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Canceled...');", true);
                 }

             }
             else
             {

                 ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Then Try Again...');", true);
             }
         }
         protected void btnDealerchange_Click(object sender, EventArgs e)
         {
             LoadPartyWiseApprovedInvoice();
             LoadPartyWiseDeliveredOrder();
             LoadInvoiceNumber();

         }
    }
}