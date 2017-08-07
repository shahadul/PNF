using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Selling_Process
{
    public partial class Deliver : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;

        static string ChallanMasterID = "";
        static string partyid = "0";
        static string ProductGroupID = String.Empty;

        static string PartyCode = "";
        static string PartyName = "";
        static string PartyMobile = "";
        static string asmMobile = "";
        static string dsmMobile = "";
        static string Invoiceid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDeliverDate.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(Session["Message"].ToString()))
            {
                string msg = Session["Message"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('" + msg + "');", true);
                Session["Message"] = string.Empty;
            }
            if (!IsPostBack)
            {
                
                loadChallans();
              
                loadDeliver();
                LoadChallanNumber();
                divoerderdetails.Visible = false;
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
            string qry;
            qry = "EXEC Get_Number_list 'ChallanMaster','','','','N'";

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
      
        protected void loadChallans()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.*,pm.PartyCode,pm.PartyName
                                ,pm.OwnerContactPhone1 as Partymobile,asm.PhonePersonal as asmMobile,dsm.PhonePersonal as dsmMobile from ChallanMaster t
                                 left join PartyMaster pm on t.PartyID=pm.PartyID
                                 left join AreaSalesManMaster asm on pm.AreaSalesManID=asm.AreaSalesManID
                                 left join DivisionalSalesManMaster dsm on asm.DivisionalSalesManID=dsm.DivisionalSalesManID
                                 where 
                                 (t.IsDeliver is null or t.IsDeliver<>'Y') order by t.ChallanDate Desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                grdChallan.DataSource = ds;
                grdChallan.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void loadDeliver()
        {
            String query = "";
            string party = String.Empty;
            party = HIDDealerID.Value;
            if (!string.IsNullOrEmpty(party))
            {
               
                query = @"select t.*,pm.PartyName,cm.InvID from Delivery t,PartyMaster pm,ChallanMaster cm where t.PartyID=pm.PartyID and t.ChallanMasterID=cm.ChallanMasterID and t.PartyID='" + party + "' order by t.DeliveryDate Desc";
            }
            else
            {
                query = @"select t.*,pm.PartyName,cm.InvID from Delivery t,PartyMaster pm,ChallanMaster cm where t.PartyID=pm.PartyID and t.ChallanMasterID=cm.ChallanMasterID order by t.DeliveryDate Desc";
            }
            try
            {
                DataSet ds = new DataSet();
                ds = dba.GetDataSet(query, "ConnDB230");
                grdDelivery.DataSource = ds;
                grdDelivery.DataBind();
            }
            catch (Exception ex)
            { throw ex; }
        }
        protected void grdChallan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdChallan.PageIndex = e.NewPageIndex;
            loadChallans();
        }
        private void LoadChallanDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.*,pm.ProdName,((invd.ApprovedTotal/invd.ApprovedQty)*t.ChalanQty) as ChalanTotal from ChallanMaster cm,InvoiceMaster invm,ChallanDetail t,InvoiceDetails invd,Product_Master pm 
                                where
                                cm.InvID=invm.InvID
                                and t.ProdID=pm.ProdID 
                                 and cm.InvID=invd.InvID
                                 and cm.ChallanMasterID=t.ChallanMasterID
                                 and t.ProdID=invd.ProdID
                                 and t.ChallanMasterID='"+ChallanMasterID+"'order by t.ChallanDetailID ";
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


        protected void grdChallan_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            divoerderdetails.Visible = true;
            partyid = grdChallan.Rows[e.NewSelectedIndex].Cells[9].Text.Replace("&nbsp;", "");
            ChallanMasterID = grdChallan.Rows[e.NewSelectedIndex].Cells[1].Text.Replace("&nbsp;", "");
            ddlChallanNumber.SelectedValue = ChallanMasterID;
            HidTotalQty.Value = grdChallan.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");
            PartyCode = grdChallan.Rows[e.NewSelectedIndex].Cells[10].Text.Replace("&nbsp;", "");
            PartyName = grdChallan.Rows[e.NewSelectedIndex].Cells[11].Text.Replace("&nbsp;", "");
            PartyMobile = grdChallan.Rows[e.NewSelectedIndex].Cells[12].Text.Replace("&nbsp;", "");
            asmMobile = grdChallan.Rows[e.NewSelectedIndex].Cells[13].Text.Replace("&nbsp;", "");
            dsmMobile = grdChallan.Rows[e.NewSelectedIndex].Cells[14].Text.Replace("&nbsp;", "");
            Invoiceid = grdChallan.Rows[e.NewSelectedIndex].Cells[4].Text.Replace("&nbsp;", "");
            LoadChallanDetails();

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
            HidTotalQty.Value = "0";
            gvChallanDetails.DataSource = null;
            gvChallanDetails.DataBind();
            divoerderdetails.Visible = false;
            ChallanMasterID = string.Empty;
            loadChallans();
            loadDeliver();
        }
        //public void SendSMS(double TTLAmnt)
        //{
        //   // PartyMobile = "8801755654247";
        //    //asmMobile = String.Empty;
        //    //dsmMobile = String.Empty;
        //    string date = DateTime.ParseExact(txtDeliverDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
           
        //    string smsdtl = @"Dear Valued Customer. Thank You for Purchasing from Power+" + Environment.NewLine + "--Your Details--" + Environment.NewLine + PartyCode + "," + Environment.NewLine + PartyName + "," + Environment.NewLine + "Inv No:" + Invoiceid + "," + Environment.NewLine + "TTL AMN:" + TTLAmnt + "," + Environment.NewLine + "Date:" + date;

        //    String sid = "FDLElectronics"; String user = "fdlelectronics"; String pass = "74X878m>";
        //    string URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";

        //    string myParameters = String.Empty;

        //    if (!string.IsNullOrEmpty(PartyMobile))
        //    {
        //        myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + PartyMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
        //        using (WebClient wc = new WebClient())
        //        {
        //            string s = myParameters;
        //            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(asmMobile))
        //    {
        //        myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + asmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
        //        using (WebClient wc = new WebClient())
        //        {
        //            string s = myParameters;
        //            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(dsmMobile))
        //    {
        //        myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + dsmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
        //        using (WebClient wc = new WebClient())
        //        {
        //            string s = myParameters;
        //            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //            string HtmlResult = wc.UploadString(URI, myParameters); Response.Write(HtmlResult);
        //        }
        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string DeliveryDate = DateTime.ParseExact(txtDeliverDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            string resOut = string.Empty;
            double TTLAmnt = 0;
            string addedby = Session["UserID"].ToString();
            try
            {
                btnSubmit.Visible = false;
                string qryDeliver = "insert into Delivery (ChallanMasterID,DeliveryDate,DeliveryQty,PartyID,DeliveryStatus,AddedBy) values " +
                        "('" + ChallanMasterID + "','" + DeliveryDate + "','" + HidTotalQty.Value + "','" + partyid + "','Y','" + addedby + "')";
                string result = dba.ExecuteQuery(qryDeliver, "ConnDB230");
                string QryUpdateChallanMaster = @"update ChallanMaster set IsDeliver='Y' where ChallanMasterID='" + ChallanMasterID + "'";
                resOut = dba.ExecuteQuery(QryUpdateChallanMaster, "ConnDB230");
                if (result == "1")
                {

                    foreach (GridViewRow row in gvChallanDetails.Rows)
                    {
                        int Qty = Convert.ToInt32(nullChecker(row.Cells[1].Text));
                        int vprodId = Convert.ToInt32(nullChecker(row.Cells[2].Text));
                        TTLAmnt += Convert.ToDouble(nullChecker(row.Cells[4].Text));
                        int UpdatedStockQty = 0;
                        int UpdatedAvailableStockQty = 0;
                        string StockQty = "select StockQty,isnull(AvailableStock,0) as AvailableStock from IStock where ProdID='" + vprodId + "'";
                        DataTable dt = dba.GetDataTable(StockQty);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            UpdatedStockQty = Convert.ToInt32(dt.Rows[0][0].ToString()) - Qty;
                            UpdatedAvailableStockQty = Convert.ToInt32(dt.Rows[0][1].ToString()) - Qty;
                            string QryIStock = @"update IStock 
                                                            set StockQty='" + UpdatedStockQty + "',AvailableStock='" + UpdatedAvailableStockQty + "' where ProdID='" + vprodId + "'";
                            dba.ExecuteQuery(QryIStock, "ConnDB230");

                        }
                    }

                }


            }
            catch
            {

            }

            if (resOut == "1")
            {
              //  SendSMS(TTLAmnt);
                Clear();
              
                Session["Message"] = "Data Saved...";
            }
            else
            {
                Session["Message"] = "Not Saved...";
               
            }
            Response.Redirect("Deliver.aspx");
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
        protected void btnChallan1_Click(object sender, EventArgs e)
        {
            string NameValueParam = "Challan";
            if (string.IsNullOrEmpty(hidChallanMasterID.Value))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Row...');",
                    true);
            }
            else
            {
                Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + hidChallanMasterID.Value);
            }

        }
        protected void btnDelivery_Click(object sender, EventArgs e)
        {
            string NameValueParam = "Delivery";
            if (string.IsNullOrEmpty(hidChallanMasterID.Value))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Row...');",
                    true);
            }
            else
            {
                Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + hidChallanMasterID.Value);
            }

        }
        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            string NameValueParam = "Invoice";
            if (string.IsNullOrEmpty(hidInvoiceID.Value))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select One Row...');",
                    true);
            }
            else
            {
                Response.Redirect("../Reports/ReportFormCommon.aspx?NameValueParam=" + NameValueParam + "&ID=" + hidInvoiceID.Value);
            }

        }
        protected void grdDelivery_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {

            hidChallanMasterID.Value = grdDelivery.Rows[e.NewSelectedIndex].Cells[4].Text.Replace("&nbsp;", "");
            hidInvoiceID.Value = grdDelivery.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");

        }
        protected void grdDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDelivery.PageIndex = e.NewPageIndex;
            loadDeliver();
        }
        protected void btnDealerchange_Click(object sender, EventArgs e)
        {
            loadDeliver();

        }
    }
}