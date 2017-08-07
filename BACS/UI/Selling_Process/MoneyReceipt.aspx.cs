
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
    public partial class MoneyReceipt : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;       
        static string InvoiceID = "";
        static double MRAmount = 0;
        static double InvoiceAmount = 0;
        static string MRID = "";
        static string PartyCode = "";
        static string PartyName = "";
        static string PartyMobile = "";
        static string asmMobile = "";
        static string dsmMobile = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["Message"].ToString()))
            {
                string msg = Session["Message"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('" + msg + "');", true);
                Session["Message"] = string.Empty;
            }
            if (!IsPostBack)
            {
                lblmessage.Text=String.Empty;
                txtTotalInvoice.Text = "0";
                txtTotalMRAmount.Text = "0";
                divCheque_PO_NO.Visible = false;
                divbankname.Visible = false;
                divdetails.Visible = false;
                LoadBankList();
                LoadCollectionType();

            }
        }
        protected void LoadCollectionType()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select 1 as CollectionTypeID,'Payment' as CollectionType union select 2 as CollectionTypeID,'Claim Adjustment' as CollectionType ", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlCollectionType, dt, "CollectionTypeID", "CollectionType", "Select Collection Type");
            }
            else
            {

                ddlCollectionType.Items.Clear();
                ddlCollectionType.Items.Add(new ListItem("No Collection Type Available", "0"));

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

      public void LoadDetailsGrid(string party)
        {
          //string  vMRDate = DateTime.ParseExact(txtMRDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            DataSet ds = new DataSet();
            try
            {
                
                String query = @"select t1.InvoiceAmount,isnull(t2.MrAmount,0) as PayAmount,(t1.InvoiceAmount-isnull(t2.MrAmount,0))as OutstandingAmount
                  ,0 as PresentPayment,t1.PartyID,t1.ProductGroupID,pg.GroupName,pm.PartyCode,pm.PartyName,pm.OwnerContactPhone1 as PartyMobile,asm.PhonePersonal as asmMobile,dsm.PhonePersonal as dsmMobile from  
                  (select isnull(sum(t.ApprovedAmount),0) as InvoiceAmount
                  ,t.PartyID,t.ProductGroupID from InvoiceMaster t where t.PartyID='" + party + "' and t.IsApprove='Y' " +
                  "and t.IsCancel<>'Y' group by t.ProductGroupID,t.PartyID) t1 left join (select isnull(sum(d.GroupPMAmount),0) as MrAmount,t.PartyID,d.ProductGroupID " +
                  "from Payment_Master t,Payment_Master_Detail d where t.PartyID='" + party + "' and t.CancelStatus='N' and t.PMID=d.PMID group by d.ProductGroupID,t.PartyID) t2 " +
                   "on t1.PartyID=t2.PartyID and t1.ProductGroupID=t2.ProductGroupID left join ProductGroup pg on t1.ProductGroupID=pg.ProductGroupID "+
                   "left join PartyMaster pm on t1.PartyID=pm.PartyID "+
                   "left join AreaSalesManMaster asm on pm.AreaSalesManID=asm.AreaSalesManID "+
                   "left join DivisionalSalesManMaster dsm on asm.DivisionalSalesManID=dsm.DivisionalSalesManID";
                ds = dba.GetDataSet(query, "ConnDB230");
                if (ds != null)
                {
                   
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                    divdetails.Visible = true;
                    double voutstanding = 0;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        voutstanding += Convert.ToDouble(row["OutstandingAmount"]);
                        PartyCode = row["PartyCode"].ToString();
                        PartyName = row["PartyName"].ToString();
                        PartyMobile = row["PartyMobile"].ToString();
                        asmMobile = row["asmMobile"].ToString();
                        dsmMobile = row["dsmMobile"].ToString();
                    }

                    if (!string.IsNullOrEmpty(voutstanding.ToString()))
                    {
                        txtTotalInvoice.Text = voutstanding.ToString();

                    }
                    else
                    {
                        txtTotalInvoice.Text = "0";

                    }

                }
                else
                {
                    divdetails.Visible = false;
                }

            }
            catch (Exception ex)
            { throw ex; }
         
        }
       
    
        protected void LoadBankList()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.BID,t.BankName from BankList t order by BankName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlBankName, dt, "BID", "BankName", "Select Bank Name");
            }
            else
            {

                ddlBankName.Items.Clear();
                ddlBankName.Items.Add(new ListItem("No Bank Name Available", "0"));

            }
        }
       
        private void Clear()
        {
            txtTotalMRAmount.Text = "0";
            txtTotalInvoice.Text = "0";
            divCheque_PO_NO.Visible = false;
            divbankname.Visible = false;
            InvoiceID = string.Empty;
            HIDDealerID.Value=String.Empty;
            txtDealer.Text=String.Empty;
            txtSerial.Text = "";
            txtRemarks.Text = "";
            txtMRDate.Text = string.Empty;
            txtChequeDate.Text = string.Empty;
            txtCheque_PO_NO.Text = string.Empty;
            divdetails.Visible = false;
            lblmessage.Text=String.Empty;
        }
        public void SendSMS(double TTLAmnt, string Payment)
        {
            
            //PartyMobile = String.Empty;
            //PartyMobile = "8801755654247";
            //asmMobile = String.Empty;
            //dsmMobile = String.Empty;
            string date = txtMRDate.Text;
            string smsdtl = @"Dear Valued Customer. Thank You for Payment on Power+" + Environment.NewLine + "--Your Details--" + Environment.NewLine + PartyCode + "," + Environment.NewLine + PartyName + "," + Environment.NewLine + "MR No:" + MRID + "," + Environment.NewLine + "TTL AMN:" + TTLAmnt + "," + Environment.NewLine + "Payment:" + Payment + "," + Environment.NewLine + "Date:" + date;

            String sid = "FDLElectronics"; 
            String user = "fdlelectronics";
            String pass = "74X878m>";
            string URI = "http://sms.sslwireless.com/pushapi/dynamic/server.php";
            string myParameters = String.Empty;

            if (!string.IsNullOrEmpty(PartyMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + PartyMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    string s = myParameters;
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters);
                    Response.Write(HtmlResult);
                }
               
            }
            if (!string.IsNullOrEmpty(asmMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + asmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters);
                    Response.Write(HtmlResult);
                }
            }
            if (!string.IsNullOrEmpty(dsmMobile))
            {
                myParameters = "user=" + user + "&pass=" + pass + "&sms[0][0]=" + dsmMobile + "&sms[0][1]=" + HttpUtility.UrlEncode(smsdtl) + "&sms[0][2]=987654321&" + "sid=" + sid;
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(URI, myParameters);
                    Response.Write(HtmlResult);
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            int checkcount = 0;

            foreach (GridViewRow row in gvDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                    if (vchkselect.Checked)
                    {
                        checkcount = 1;
                        break;
                    }

                }
            }
            double vMRAmount = Convert.ToDouble(txtTotalMRAmount.Text);

            if (checkcount == 1 && vMRAmount!=0)
            {
                btnSubmit.Visible = false;
                string resOut = "";
                
                InvoiceAmount = Convert.ToDouble(txtTotalInvoice.Text);
                int vMRID = 0;
                string vMRDate = DateTime.ParseExact(txtMRDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string ReceivedType = ddlCheckedBy.SelectedItem.Text.Trim();
                string Payment = String.Empty;
                //if (vMRAmount > InvoiceAmount)
                //{
                //    txtTotalMRAmount.Text = Convert.ToString(MRAmount);
                //    txtTotalMRAmount.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Not Accept greater than Invoice Amount');", true);
                //}
                //else
                //{
                    string vRTMRID = string.Empty;
                    try
                    {
                        string collectionstatus = "";
                        if (rdbtnIsCollect.SelectedValue == "0")
                        {
                            collectionstatus = "N";
                        }
                        else
                        {
                            collectionstatus = "Y";
                        }
                        string vCreatedBy = Session["UserID"].ToString();
                        //string vMRDate = DateTime.ParseExact(txtMRDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        //string ReceivedType = ddlCheckedBy.SelectedItem.Text.Trim();
                        string vChequeDate = txtChequeDate.Text.ToString();
                        string vCheckedDate = "";
                        if (!string.IsNullOrEmpty(vChequeDate))
                        {
                            vCheckedDate = DateTime.ParseExact(txtChequeDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }

                        string vCheque_PO_NO = txtCheque_PO_NO.Text.Trim();
                        string vBankName = ddlBankName.SelectedItem.Text.Trim();
                        string partyid = HIDDealerID.Value;
                        string vRemarks = txtRemarks.Text.Trim();
                        string vserial = txtSerial.Text.Trim();
                        string query = "insert into Payment_Master(PMDate,InvoiceAmount,PMAmount,ReceivedType,BankName,ChequeDate,ChequeNo,CreatedBy,Remarks,PartyID,Serial,CancelStatus,CollectionStatus,CollectionType)values('" + vMRDate + "','" + InvoiceAmount + "','" + vMRAmount + "','" + ReceivedType + "','" + vBankName + "','" + vCheckedDate + "','" + vCheque_PO_NO + "','" + vCreatedBy + "','" + vRemarks + "','" + partyid + "','" + vserial + "','N','" + collectionstatus + "','" + ddlCollectionType.SelectedItem.Text.Trim()+ "')" + "SELECT CAST(scope_identity() AS varchar(36))";
                        string result = dba.getObjectDataStr(query, "ConnDB230");
                        MRID = result;
                        if (result != string.Empty)
                        {
                            vMRID = Convert.ToInt32(result);
                            string dateYear = DateTime.ParseExact(DateTime.Today.ToString("yy"), "yy", CultureInfo.InvariantCulture).ToString("yy", CultureInfo.InvariantCulture);

                            string vMRNumber = "MR-" + dateYear + "-" + Convert.ToInt64(result).ToString("000000000");

                            string sqlq1 = "SELECT  PMID, PMNumber FROM Payment_Master WHERE (PMID = '" + result + "')";
                            DataTable dt1 = dba.GetDataTable(sqlq1);
                            if (dt1 != null)
                            {
                                vRTMRID = dt1.Rows[0][0].ToString();
                            }
                            string updatequery = "update Payment_Master  set PMNumber='" + vMRNumber + "' WHERE (PMID = '" + result + "')";
                            resOut = dba.ExecuteQuery(updatequery, "ConnDB230");
                        }
                        string resultdetail = "";
                        foreach (GridViewRow row in gvDetails.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                                if (vchkselect.Checked)
                                {
                                    Label lblDueAmount = (Label)row.FindControl("lblDueAmount");
                                    decimal GroupDueAmount = Convert.ToDecimal(nullChecker(lblDueAmount.Text));

                                    TextBox txtPresentPayment = (TextBox)row.FindControl("txtPresentPayment");
                                    decimal GroupPresentPayment = Convert.ToDecimal(nullChecker(txtPresentPayment.Text));
                                    string ProductGroupID = row.Cells[3].Text.Trim();
                                    string querydetail = "insert into Payment_Master_Detail(PMID,ProductGroupID,GroupInvoiceAmount,GroupPMAmount)values('" + result + "','" + ProductGroupID + "','" + GroupDueAmount + "','" + GroupPresentPayment + "')";

                                    resultdetail = dba.getObjectDataStr(querydetail, "ConnDB230");

                                    String queryupdate = "EXEC updatemoneyreceiptdtl '" + partyid + "','" + ProductGroupID + "'";
                                    dba.ExecuteQuery(queryupdate, "ConnDB230");



                                }

                            }
                        }

                        if (resOut == "1")
                        {
                            String query1 = "EXEC updatemoneyreceipt '" + HIDDealerID.Value + "'";
                            dba.ExecuteQuery(query1, "ConnDB230");
                            String query2 = "EXEC updatemoneyreceipt '" + HIDDealerID.Value + "'";
                            dba.ExecuteQuery(query2, "ConnDB230");

                            if (ReceivedType == "CASH")
                            {
                                Payment = "CASH";
                            }
                            else
                            {
                                Payment = ddlBankName.SelectedItem.Text.Trim();
                            }
                            SendSMS(vMRAmount, Payment);

                            Clear();
                           
                        
                            Session["Message"] = "Data Saved...";
                           
                        }
                        else
                        {
                            
                            Session["Message"] = "Not Saved...";
                        }
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                        btnSubmit.Visible = true;
                    }


                //}
            }
            else
            {
               
             
                Session["Message"] = "Total MR Amount Should not be Zero So Please Select One...";
            }
            Response.Redirect("MoneyReceipt.aspx");
            btnSubmit.Visible = true;
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
        protected void chkselect_Click(object sender, EventArgs e)
        {
            decimal GrandTotal = 0;
            foreach (GridViewRow row in gvDetails.Rows)
            {
                //Label lblInvoiceAmount = (Label)row.FindControl("lblInvoiceAmount");
                //decimal InvoiceAmount = Convert.ToDecimal(nullChecker(lblInvoiceAmount.Text));
                //decimal vtotal = 0;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox vchkselect = (CheckBox)row.FindControl("chkselect");

                    if (vchkselect.Checked)
                    {
                        TextBox txtPresentPayment = (TextBox)row.FindControl("txtPresentPayment");
                        decimal PresentPayment = Convert.ToDecimal(nullChecker(txtPresentPayment.Text));
                        GrandTotal = GrandTotal + PresentPayment;

                    }
                }
            }
            txtTotalMRAmount.Text = GrandTotal.ToString();
        }

        protected void ddlCheckedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCheckedBy.SelectedValue == "1")
            {
                divCheque_PO_NO.Visible = false;
                divbankname.Visible = false;
            }
            else
            {
                divCheque_PO_NO.Visible = true;
                divbankname.Visible = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           string ds = ddlBankName.SelectedItem.Text;
           Clear();
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
           LoadDetailsGrid(HIDDealerID.Value);
        
        }

    }
}