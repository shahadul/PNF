using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class Challan : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;

       
     //   static string partyid = "0";
        static string ProductGroupID = String.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtChallanDate.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (!IsPostBack)
            {
                loadInvoice();
               
                LoadChallanNumber();
                LoadInvoiceNumber();
                loadChallans(HIDDealerID.Value, ddlFindByDeliveryStatus.SelectedValue, ddlChallanNumber.SelectedValue);
                divoerderdetails.Visible = false;
            }
        }
        protected void LoadInvoiceNumber()
        {
          
            string qry;
            qry = "select t.InvID,t.InvNumber from InvoiceMaster t where t.IsApprove='Y' and t.IsCancel<>'Y' and t.IsChallan<>'Y'";

            DataTable dt = dba.GetDataTable(qry);
            if (dt != null)
            {
                forDdl.LoadDll(ddlInvoiceNumber, dt, "InvID", "InvNumber", "Select Invoice Number");
            }
            else
            {
                ddlInvoiceNumber.Items.Clear();
                ddlInvoiceNumber.Items.Add(new ListItem("Invoice Number not Available", "0"));

            }


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
        protected void LoadChallanNumber()
        {
            string status = "";
            if (ddlFindByDeliveryStatus.SelectedValue != "A")
            {
                status = ddlFindByDeliveryStatus.SelectedValue;
            }
            string qry;
            qry = "EXEC Get_Number_list 'ChallanMaster','" + HIDDealerID.Value + "','','','" + status + "'";

            DataTable dt = dba.GetDataTable(qry);
            if (dt != null)
            {
                forDdl.LoadDll(ddlChallanNumber, dt, "ChallanMasterID", "ChallanNumber", "Select Challan Number");
            }
            else
            {
                ddlChallanNumber.Items.Clear();
                ddlChallanNumber.Items.Add(new ListItem("Challan Number not Available", "0"));

            }


        }
      
        protected void grdChallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdChallan.PageIndex = e.NewPageIndex;
            loadInvoice();
        }
        protected void loadInvoice()
        {
            try
            {
                DataSet ds = new DataSet();

                String query = @"select t.InvID,t.InvNumber,t.ApprovedQty,t.ApprovedDate,t.PartyID,t.ProductGroupID,p.PartyName,t.InvDate from InvoiceMaster t,PartyMaster p 
                                 where t.PartyID=p.PartyID and t.IsApprove='Y' and t.IsCancel <>'Y' and t.isChallan<>'Y' order by t.InvID Desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                grdChallan.DataSource = ds;
                grdChallan.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
       
        private void LoadChallanDetails()
        {
            try
            {
                DataSet ds = new DataSet();

                String query = @"select t.InvDetailID,t.InvID,t.ProdID,t.ApprovedQty,t.ProdName
                 , (t.ApprovedQty-ISNULL(t1.ChallanQuantity,0)) as pending_quantity
                 from 
                (select t.*,p.ProdName from InvoiceDetails t left outer join Product_Master p on t.ProdID=p.ProdID where t.InvID='" + HIDInvID.Value + "') t left outer join (select (SUM(ISNULL(d.ChalanQty,0))-SUM(ISNULL(d.ReturnQty,0))) as ChallanQuantity,d.ProdID from ChallanDetail d,ChallanMaster  m where m.ChallanMasterID=d.ChallanMasterID and m.InvID='" + HIDInvID.Value + "' group by d.ProdID) t1 on  t.ProdID=t1.ProdID where  (t.ApprovedQty-ISNULL(t1.ChallanQuantity,0))<>0 order by t.InvDetailID desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                if (ds != null)
                {
                    gvChallanDetails.DataSource = ds;
                    gvChallanDetails.DataBind();
                 
                }

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void loadChallans(string partyid,string Status,string ChallanNo)
        {
            string DeliveryStatus="";
            if (Status != "A")
            {
                DeliveryStatus = Status;

            }
         
            try
            {
                DataSet ds = new DataSet();
                String query = @"EXEC Get_Challans '" + partyid + "','" + DeliveryStatus + "','" + ChallanNo + "'";
                ds = dba.GetDataSet(query, "ConnDB230");
                grdallChallan.DataSource = ds;
                grdallChallan.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void grdChallan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

            }
            for (int i = 0; i <= grdChallan.Rows.Count - 1; i++)
            {
                grdChallan.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
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
                                             Page.ClientScript.GetPostBackEventReference(grdChallan, "Select$" +
                                                                                         e.Row.RowIndex.ToString()));

                        break;
                }

            }
            catch (Exception ex) { }
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                //int rowindex = 0;
                //rowindex = currentRow.RowIndex;

                TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));


                Int32 PendingQuantiity = Convert.ToInt32(nullChecker(currentRow.Cells[5].Text));
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

                
            }
            catch
            {

            }

        }
        protected void grdChallan_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDInvID.Value = grdChallan.Rows[e.NewSelectedIndex].Cells[4].Text.Replace("&nbsp;", "");

            ddlInvoiceNumber.SelectedValue = HIDInvID.Value;
            LoadChallanDetails();
            divoerderdetails.Visible = true;
            HIDPartyID.Value = grdChallan.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");

            ProductGroupID = grdChallan.Rows[e.NewSelectedIndex].Cells[6].Text.Replace("&nbsp;", "");
            
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
        
        private void Clear()
        {

           
            gvChallanDetails.DataSource = null;
            gvChallanDetails.DataBind();
            divoerderdetails.Visible = false;
            HIDInvID.Value = string.Empty;
            HIDPartyID.Value = string.Empty;

        }
        protected void chkselect_Click(object sender, EventArgs e)
        {
            CheckBox thisCheckBox = (CheckBox)sender;
            GridViewRow currentRow = (GridViewRow)thisCheckBox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

          
            foreach (GridViewRow row in gvChallanDetails.Rows)
            {
              
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                    if (vchkselect.Checked)
                    {
                        TextBox txtQuantity = (TextBox)currentRow.FindControl("txtQuantity");
                        Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
                        Int32 PendingQuantiity = Convert.ToInt32(nullChecker(currentRow.Cells[5].Text));

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
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Invoice Quantity');", true);

                        }
                       
                    }
                }
            }
          
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int checkcount = 0;
            Int32 totalQTY = 0;
            Int32 PendingQuantiity = 0;
            foreach (GridViewRow row in gvChallanDetails.Rows)
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
                    Int32 pendingqty = Convert.ToInt32(row.Cells[5].Text.Trim());
                    PendingQuantiity += pendingqty;
                }
            }

           
            if (checkcount == 1)
            {
                btnSubmit.Visible = false;
                try
                {
                    string resOut = string.Empty;
                   
                    string ChallanDate = DateTime.ParseExact(txtChallanDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    string Remarks = txtRemarks.Text.Trim();

                 
                    string UserID = Session["UserID"].ToString();


                    string qryChallan = "insert into ChallanMaster (InvID,ChallanDate,TTLChallanQty,PartyID,Remarks,AddedBy,IsDeliver) values " +
                        "('" + HIDInvID.Value + "','" + ChallanDate + "','" + totalQTY + "','"+HIDPartyID.Value+"','"+Remarks+"','"+UserID+"','N')" + "SELECT CAST(scope_identity() AS varchar(36))";
                    string ChallanMasterID = dba.getObjectDataStr(qryChallan, "ConnDB230");

                    string RTChallanID = string.Empty;
                    if (HIDInvID.Value != string.Empty)
                    {
                        string dateYear = DateTime.ParseExact(DateTime.Today.ToString("yy"), "yy", CultureInfo.InvariantCulture).ToString("yy", CultureInfo.InvariantCulture);
                       
                        string datemonth = DateTime.ParseExact(DateTime.Today.ToString("mm"), "mm", CultureInfo.InvariantCulture).ToString("mm", CultureInfo.InvariantCulture);
                        string dateday = DateTime.ParseExact(DateTime.Today.ToString("dd"), "dd", CultureInfo.InvariantCulture).ToString("dd", CultureInfo.InvariantCulture);

                        string ChallanNumber = "CLN-" + dateYear + "-"+ Convert.ToInt64(ChallanMasterID).ToString("000000000");
                        string sqlInvno = "update ChallanMaster set ChallanNumber='" + ChallanNumber + "' where ChallanMasterID='" + ChallanMasterID + "'";
                        dba.ExecuteQuery(sqlInvno, "ConnDB230");
                    }
                    foreach (GridViewRow row in gvChallanDetails.Rows)
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
                                    string productid = row.Cells[2].Text.Trim();

                                    string QryChallanDetail = "insert into ChallanDetail (ChallanMasterID, ProdID, ChalanQty)" +
                                                           "values ('" + ChallanMasterID + "','" + productid + "','" + Quantity + "')";

                                    resOut = dba.ExecuteQuery(QryChallanDetail, "ConnDB230");

                                }

                                catch (Exception ex)
                                {
                                    throw new Exception();
                                }

                            }

                        }
                    }

                    Int32 currentpendingqty = (PendingQuantiity - totalQTY);
                    if (currentpendingqty == 0)
                    {
                        string qryChallanMaster =
                            @"update InvoiceMaster 
                      set IsChallan='Y' where InvID= '" + HIDInvID.Value + "'";
                        string finalOut = dba.ExecuteQuery(qryChallanMaster, "ConnDB230");
                    }
                    else
                    {
                        string qryChallanMaster =
                         @"update InvoiceMaster 
                      set IsChallan='P' where InvID= '" + HIDInvID.Value + "'";
                        string result = dba.ExecuteQuery(qryChallanMaster, "ConnDB230");

                    }


                    if (resOut == "1")
                    {

                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                        loadInvoice();
                        loadChallans(HIDDealerID.Value, ddlFindByDeliveryStatus.SelectedValue,ddlChallanNumber.SelectedValue);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Atleast One Then Try Again...');", true);
            }
            btnSubmit.Visible = true;
        }
        protected void btnChallan_Click(object sender, EventArgs e)
        {
            string NameValueParam = "Challan";
            if (ddlChallanNumber.SelectedValue == "" || ddlChallanNumber.SelectedIndex == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Challan Number ...');",
                    true);
            }
            else
            {
                Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + ddlChallanNumber.SelectedValue);
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
        //protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    loadChallans(ddlPartyName.SelectedValue,ddlFindByDeliveryStatus.SelectedValue,ddlChallanNumber.SelectedValue);
        //    LoadChallanNumber();
        //    divclndtl.Focus();
        //}
        protected void ddlChallanNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadChallans(HIDDealerID.Value, ddlFindByDeliveryStatus.SelectedValue, ddlChallanNumber.SelectedValue);
            divclndtl.Focus();
        }
        protected void btnDealerchange_Click(object sender, EventArgs e)
        {
            loadChallans(HIDDealerID.Value, ddlFindByDeliveryStatus.SelectedValue, ddlChallanNumber.SelectedValue);
            LoadChallanNumber();
            divclndtl.Focus();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
//            int checkcount = 0;
//            Int32 totalQTY = 0;
//            Int32 PendingQuantiity = 0;
//            Int32 CancelChallanQtyMaster = 0;
//            Int32 CancelChallanQtyDetail = 0;
//            foreach (GridViewRow row in gvChallanDetails.Rows)
//            {
//                if (row.RowType == DataControlRowType.DataRow)
//                {
//                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

//                    if (vchkselect.Checked)
//                    {
//                        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
//                        Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
//                        totalQTY += Quantity;

//                        checkcount = 1;
//                    }
//                    Int32 pendingqty = Convert.ToInt32(row.Cells[5].Text.Trim());
//                    CancelChallanQtyMaster = Convert.ToInt32(row.Cells[6].Text.Trim());
//                    PendingQuantiity += pendingqty;
//                }
//            }


//            if (checkcount == 1)
//            {
//                string resOut = string.Empty;
//                Int32 vCancelChallanQty = totalQTY + CancelChallanQtyMaster;
//                string QryInvMaster = @"update InvoiceMaster 
//                                                            set CancelChallanQty='" + vCancelChallanQty + "' where InvID='" + InvID + "'";
//                dba.ExecuteQuery(QryInvMaster, "ConnDB230");
//                foreach (GridViewRow row in gvChallanDetails.Rows)
//                {
//                    if (row.RowType == DataControlRowType.DataRow)
//                    {
//                        CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

//                        if (vchkselect.Checked)
//                        {
//                            try
//                            {

//                                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
//                                Int32 Quantity = Convert.ToInt32(nullChecker(txtQuantity.Text));
//                                string productid = row.Cells[2].Text.Trim();
//                                CancelChallanQtyDetail = Convert.ToInt32(row.Cells[6].Text.Trim());
//                                Int32 vCancelChallanQtyDetail = Quantity + CancelChallanQtyDetail;
//                                string QryInvDetails = @"update InvoiceDetails 
//                                                            set CancelChallanQty='" + vCancelChallanQtyDetail + "' where ProdID='" + productid + "' and InvID='" + InvID + "'";
//                                dba.ExecuteQuery(QryInvDetails, "ConnDB230");

//                                int UpdatedAvailableStockQty = 0;
//                                string AvailableStock = "select isnull(AvailableStock,0) as AvailableStock from IStock where ProdID='" + productid + "'";
//                                DataTable dt = dba.GetDataTable(AvailableStock);
//                                if (dt != null && dt.Rows.Count > 0)
//                                {
//                                    UpdatedAvailableStockQty = Convert.ToInt32(dt.Rows[0][0].ToString()) - Quantity;

//                                    string QryIStock = @"update IStock 
//                                                            set AvailableStock='" + UpdatedAvailableStockQty + "' where ProdID='" + productid + "'";
//                                    resOut = dba.ExecuteQuery(QryIStock, "ConnDB230");

//                                }

//                            }

//                            catch (Exception ex)
//                            {
//                                throw new Exception();
//                            }

//                        }

//                    }
//                }
//                Int32 currentpendingqty = (PendingQuantiity - totalQTY);
//                if (currentpendingqty == 0)
//                {
//                    string qryChallanMaster =
//                        @"update InvoiceMaster 
//                      set IsChallan='Y' where InvID= '" + InvID + "'";
//                     dba.ExecuteQuery(qryChallanMaster, "ConnDB230");
//                }
//                else
//                {
//                    string qryChallanMaster =
//                     @"update InvoiceMaster 
//                      set IsChallan='P' where InvID= '" + InvID + "'";
//                   dba.ExecuteQuery(qryChallanMaster, "ConnDB230");

//                }

//                if (resOut == "1")
//                {

//                    Clear();
//                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Cancelled...');", true);
//                    loadInvoice();
//                    loadChallans(ddlPartyName.SelectedValue, ddlFindByDeliveryStatus.SelectedValue, ddlChallanNumber.SelectedValue);
//                }
//                else
//                {
//                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Cancelled...');", true);
//                }


//            }
//            else
//            {
//                //lblmessage.Text = "Please Select One Then Try Again";
//                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Atleast One Then Try Again...');", true);
//            }
        }
    }
}