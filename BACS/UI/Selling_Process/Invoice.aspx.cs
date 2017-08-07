
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class Invoice : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;
       
        static string Orderid = "";
        static double VAmount = 0;
        static double VApprovedTotalAmnt = 0;
        static string partyid = "0";
        static  string ProductGroupID=String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
          //  txtInvoiceDate.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (!IsPostBack)
            {
                loadOrder();
                divoerderdetails.Visible = false;

                txtGrandTotal.Text = "0";
            }
        }

        protected void grdOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdOrder.PageIndex = e.NewPageIndex;
            loadOrder();
        }
        protected void loadOrder()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.OrderID,t.OrderNo,t.Date,convert(float,t.ApprovedTotalAmnt) as ApprovedTotalAmnt,p.PartyName,t.PartyID,ProductGroupID,dbo.Get_DueAmount_ByDealerID(t.PartyID) as DueAmount from Local_Order_Master t
                left outer join PartyMaster p on t.PartyID=p.PartyID where t.IsApprove='Y' and t.IsActive='Y' and t.IsInvoice not in('C') order by t.OrderNo desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                grdOrder.DataSource = ds;
                grdOrder.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void CalculateRemainingTotal(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {

                foreach (DataRow dr in table.Rows)
                {
                    VApprovedTotalAmnt += Convert.ToDouble(nullChecker(dr["pending_total"].ToString()));
                }
            }
        }
        private void LoadOrderDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"      select t.OrderDetailID,t.OrderID,t.ProdID,t.Qty,t.ApprovedQty,t.QtyReceived,convert(float,t.Price) Price,t.Total,t.ApprovedTotalAmnt
              ,CONVERT(float,t.ApprovedDiscount) as ApprovedDiscount,CONVERT(float,t.Discount) as Discount,t.ProdName,(isnull(t.StockQty,0)-isnull(t.AvailableStock,0)) StockQty
              ,t.ApprovedQty-ISNULL(t1.invoiceQuantity,0) as pending_quantity
              ,convert(float,((t.Price*(t.ApprovedQty-ISNULL(t1.invoiceQuantity,0)))-((t.ApprovedQty-ISNULL(t1.invoiceQuantity,0))*isnull(t.ApprovedDiscount,0)*t.Price*0.01))) as pending_total
               from 
                 (select t.*,p.ProdName,IStock.StockQty,IStock.AvailableStock from Local_Order_Master_Details t
                 left outer join Product_Master p on t.ProdID=p.ProdID left outer join IStock on p.ProdID=IStock.ProdID  where t.OrderID='" + Orderid+"') t left outer join (select SUM(ISNULL(d.Quantity,0)) as invoiceQuantity,d.ProdID from InvoiceDetails d,InvoiceMaster  m where m.InvID=d.InvID and m.OrderID='"+Orderid+"' group by d.ProdID) t1 on  t.ProdID=t1.ProdID where  (t.ApprovedQty-ISNULL(t1.invoiceQuantity,0))<>0 order by t.OrderDetailID desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                if (ds != null)
                {
                    gvOrderDetails.DataSource = ds;
                    gvOrderDetails.DataBind();
                    CalculateRemainingTotal(ds);
                }
                
            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            }
            for (int i = 0; i <= grdOrder.Rows.Count - 1; i++)
            {
                grdOrder.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
            }
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.Header:
                        //...
                        break;
                    case DataControlRowType.DataRow:

                        e.Row.Attributes.Add("onclick",
                                             Page.ClientScript.GetPostBackEventReference(grdOrder, "Select$" +
                                                                                         e.Row.RowIndex.ToString()));

                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void grdOrder_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            Orderid = grdOrder.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");
            LoadOrderDetails();
            divoerderdetails.Visible = true;
            partyid = grdOrder.Rows[e.NewSelectedIndex].Cells[6].Text.Replace("&nbsp;", "");
            ProductGroupID = grdOrder.Rows[e.NewSelectedIndex].Cells[7].Text.Replace("&nbsp;", "");
            GridViewRow currentRow = (GridViewRow)grdOrder.Rows[e.NewSelectedIndex];           
            Label lblApprovedTotalAmnt = (Label)currentRow.FindControl("lblApprovedTotalAmnt");
            lblApprovedTotalAmnt.Text = VApprovedTotalAmnt.ToString();
            VApprovedTotalAmnt = 0;
            
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
                Int32 PendingQuantiity=Convert.ToInt32(nullChecker(currentRow.Cells[9].Text));

                Label lblStockQty = (Label)currentRow.FindControl("lblStockQty");
                Int32 StockQty = Convert.ToInt32(nullChecker(lblStockQty.Text));
                if (Quantity > PendingQuantiity)
                {
                    txtQuantity.Text = Convert.ToString(PendingQuantiity);
                    txtQuantity.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Order Quantity');", true);
                }
                else if (Quantity > StockQty)
                {
                    txtQuantity.Text = Convert.ToString(PendingQuantiity);
                    txtQuantity.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Stock Quantity');", true);
                }
                else 
                {
                    Label lbltotal = (Label)currentRow.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                    Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                    TextBox txtDiscount = (TextBox)currentRow.FindControl("txtDiscount");
                    double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                    double vdiscount = 0;
                    vdiscount = (Discount / 100) * Price * Quantity;

                    total = (Quantity * Price) - vdiscount;
                    lbltotal.Text = Convert.ToString(total);

                    decimal GrandTotal = 0;
                    foreach (GridViewRow row in gvOrderDetails.Rows)
                    {
                        Label Label1 = (Label)row.FindControl("lbltotal");

                        decimal vtotal = 0;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                            if (vchkselect.Checked)
                            {

                                if (Label1.Text != "")
                                {
                                    vtotal = Convert.ToDecimal(Label1.Text.Trim());
                                }
                                GrandTotal = GrandTotal + vtotal;

                            }

                          

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
            double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
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

                    Label lblPrice = (Label)currentRow.FindControl("lblPrice");
                    double Price = Convert.ToDouble(nullChecker(lblPrice.Text));


                    Label lbltotal = (Label)currentRow.FindControl("lbltotal");
                    double total = Convert.ToDouble(nullChecker(lbltotal.Text));

                    double vdiscount = 0;
                    vdiscount = (Discount / 100) * Price * Quantity;

                    total = (Quantity * Price) - vdiscount;
                    lbltotal.Text = Convert.ToString(total);

                    decimal GrandTotal = 0;
                    foreach (GridViewRow row in gvOrderDetails.Rows)
                    {
                        Label Label1 = (Label)row.FindControl("lbltotal");

                        decimal vtotal = 0;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                            if (vchkselect.Checked)
                            {

                                if (Label1.Text != "")
                                {
                                    vtotal = Convert.ToDecimal(Label1.Text.Trim());
                                }
                                GrandTotal = GrandTotal + vtotal;

                            }



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
            divoerderdetails.Visible = false;
            Orderid = string.Empty;

        }
        protected void chkselect_Click(object sender, EventArgs e)
        {
            CheckBox thisCheckBox = (CheckBox)sender;
            GridViewRow currentRow = (GridViewRow)thisCheckBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

            decimal GrandTotal = 0;
            foreach (GridViewRow row in gvOrderDetails.Rows)
            {
                Label Label1 = (Label)row.FindControl("lbltotal");

                decimal vtotal = 0;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                    if (vchkselect.Checked)
                    {
                        TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                        Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                        Int32 PendingQuantiity = Convert.ToInt32(nullChecker(currentRow.Cells[9].Text));

                        Label lblStockQty = (Label)currentRow.FindControl("lblStockQty");
                        Int32 StockQty = Convert.ToInt32(nullChecker(lblStockQty.Text));
                          if (Quantity < 1)
                        {
                            txtQuantity.Text = Convert.ToString(PendingQuantiity);
                            txtQuantity.Focus();
                            vchkselect.Checked = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept Zero Quantity');", true);

                        }
                        else if (Quantity > PendingQuantiity)
                        {
                            txtQuantity.Text = Convert.ToString(PendingQuantiity);
                            txtQuantity.Focus();
                            vchkselect.Checked = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Order Quantity');", true);

                        }
                        else if (Quantity > StockQty)
                        {
                            txtQuantity.Text = Convert.ToString(PendingQuantiity);
                            txtQuantity.Focus();
                            vchkselect.Checked = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Stock Quantity');", true);
                        }
                        else
                        {
                            if (Label1.Text != "")
                            {
                                vtotal = Convert.ToDecimal(Label1.Text.Trim());
                            }
                            GrandTotal = GrandTotal + vtotal;

                        }
                    }
                }
            }
            txtGrandTotal.Text = GrandTotal.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int checkcount = 0;
            Int32 totalQTY=0;
            foreach (GridViewRow row in gvOrderDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                    if (vchkselect.Checked)
                    {
                    TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                    Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                    totalQTY += Quantity;
                    checkcount = 1;
                    }

                }
            }

          
            if (checkcount == 1)
            {
                try
                {
                    string resOut = string.Empty;
                    //string InvoiceDateF = Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd");
                    string InvoiceDate = DateTime.ParseExact(txtInvoiceDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    string remrks = txtRemarks.Text.Trim();

                    string GrandTotal = txtGrandTotal.Text.Trim();

                    string UserID = Session["UserID"].ToString();


                    string qryOrder = "insert into InvoiceMaster (OrderID,InvDate,Total,IsApprove,IsCancel,IsMoneyReceipt,Remarks,AddedBy,OutstandingAmount,InvoiceQty,PartyID,ProductGroupID) values " +
                        "('" + Orderid + "','" + InvoiceDate + "','" + GrandTotal + "','N','N','N'," +
                        "'" + remrks + "','" + UserID + "','" + GrandTotal + "','" + totalQTY + "','" + partyid + "','" + ProductGroupID + "')" + "SELECT CAST(scope_identity() AS varchar(36))";
                    string InvID = dba.getObjectDataStr(qryOrder, "ConnDB230");

                    string RTInvoiceID = string.Empty;
                    if (InvID != string.Empty)
                    {
                        string dateYear = DateTime.ParseExact(DateTime.Today.ToString("yy"), "yy", CultureInfo.InvariantCulture).ToString("yy", CultureInfo.InvariantCulture);
                        
                        string invNumber = "INV-" + dateYear + "-" + Convert.ToInt64(InvID).ToString("000000000");
                        string sqlInvno = "update InvoiceMaster set InvNumber='" + invNumber + "' where InvID='" + InvID + "'";
                        dba.ExecuteQuery(sqlInvno, "ConnDB230");
                    }
                    foreach (GridViewRow row in gvOrderDetails.Rows)
                    {
                         if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                        if (vchkselect.Checked)
                        {
                            try
                            {

                            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                            Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                            Label lbltotal = (Label)row.FindControl("lbltotal");
                            double total = Convert.ToDouble(nullChecker(lbltotal.Text));
                            Label lblPrice = (Label)row.FindControl("lblPrice");
                            double Price = Convert.ToDouble(nullChecker(lblPrice.Text));
                            TextBox txtDiscount = (TextBox)row.FindControl("txtDiscount");
                            double Discount = Convert.ToDouble(nullChecker(txtDiscount.Text));
                            string productid = row.Cells[6].Text.Trim();
                            string QryOrderDetail = "insert into InvoiceDetails (InvID, ProdID, Quantity, UnitPrice,Discount, Total)" +
                                                   "values ('" + InvID + "','" + productid + "','" + Quantity + "','" + Price + "','"+Discount+"','" + total + "')";

                            resOut = dba.ExecuteQuery(QryOrderDetail, "ConnDB230");


                            int UpdatedQty = 0;
                            string AvailableStock = "select isnull(AvailableStock,0) as AvailableStock from IStock where ProdID='" + productid + "'";
                            DataTable dt = dba.GetDataTable(AvailableStock);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) + Quantity;
                                string QryIStock = @"update IStock 
                                                                    set AvailableStock='" + UpdatedQty + "' where ProdID='" + productid + "'";
                                dba.ExecuteQuery(QryIStock, "ConnDB230");

                            }

                            }

                            catch (Exception ex)
                            {
                                throw new Exception();
                            }

                        }

                    }
                    }

                    string sqlchk = @"select t.order_qty-t1.invoice_qty as pending_quntity from (select sum(ISNULL(t.ApprovedQty, 0)) as order_qty from Local_Order_Master t  where t.OrderID='" + Orderid + "' group by  t.OrderID) t, (select 0 as order_qty,sum(ISNULL(t1.InvoiceQty, 0)) as invoice_qty from InvoiceMaster t1 where t1.OrderID='" + Orderid + "' group by  t1.OrderID) t1";
                    string pending_quntity = dba.GetObjectDataId(sqlchk, "ConnDB230");
                    if (pending_quntity == "0")
                    {
                        string qryOrderMaster =
                            @"update Local_Order_Master 
                      set IsInvoice='C' where OrderID= '" + Orderid + "'";
                        string finalOut = dba.ExecuteQuery(qryOrderMaster, "ConnDB230");
                    }
                    else
                    {
                        string qryOrderMaster =
                         @"update Local_Order_Master 
                      set IsInvoice='P' where OrderID= '" + Orderid + "'";
                        string result = dba.ExecuteQuery(qryOrderMaster, "ConnDB230");

                    }
                    

                    if (resOut == "1")
                    {
                      
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                        loadOrder();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Saved...');", true);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                //lblmessage.Text = "Please Select One Then Try Again";
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Then Try Again...');", true);
            }
        }
    }
}