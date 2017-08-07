
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

namespace PNF.UI.Reports
{
    public partial class ReportForm : Page
    {
       // ReportDocument rdoc = new ReportDocument();

        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        public  static string vfile="";
        public static DataSet vds = null;
        public static string vFromDate = "";
        public static string vToDate = "";
        public static string vStockFromDate = "";
        public static string vStockToDate = "";
        public static string vDate = "";   
        public static String VAreaID="";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
               
                LoadArea();
                LoadVendor();
                LoadPurchaseType();
                LoadProductGroup();
              
                LoadProduct();
               
                divsalesreport.Visible = false;
                divStock.Visible = false;

              
            }
          
           // ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnSubmit);
          
        }
        protected void _reportViewer_DataBinding(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            else
            {
               
                    ReportDocument doc = (ReportDocument)Session["RPT"];
                    CrystalReportViewer1.ReportSource = doc;
                    CrystalReportViewer1.DataBind();
               
               
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["RPT"];
                CrystalReportViewer1.ReportSource = doc;
                CrystalReportViewer1.DataBind();
            }

          
        }
        protected void LoadArea()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select AreaID,AreaName,AreaCode from AreaMaster order by AreaName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlArea, dt, "AreaID", "AreaName", "Select Area");
            }
            else
            {

                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("No Area Available", "0"));

            }
        }
        protected void LoadVendor()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("Select * from Vendor where IsActive='Y' order by CompanyName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlVendor, dt, "VendorID", "CompanyName", "Select Vendor");
            }
            else
            {

                ddlVendor.Items.Clear();
                ddlVendor.Items.Add(new ListItem("No Vendor Available", "0"));

            }
        }
        protected void LoadPurchaseType()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select PurchaseTypeId,PurchaseType from PurchaseType", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlPurchaseType, dt, "PurchaseTypeId", "PurchaseType", "Select Purchase Type");
            }
            else
            {

                ddlPurchaseType.Items.Clear();
                ddlPurchaseType.Items.Add(new ListItem("No Purchase Type Available", "0"));

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
                    if (!string.IsNullOrEmpty(VAreaID))
                    {
                        cmd.CommandText = @"select t.PartyID,lu.UserName,t.PartyName,lu.UserName + '  =  '  + t.PartyName  as Party,AreaID from PartyMaster t,Login_Users lu 
                           where t.PartyID=lu.UserID and t.IsActive='Y' and lu.UserName + '  =  '  + t.PartyName LIKE '%'+@SearchText+'%' and AreaID='" + VAreaID + "' order by lu.UserName";
                        
                    }
                    else
                    {
                        cmd.CommandText = @"select t.PartyID,lu.UserName,t.PartyName,lu.UserName + '  =  '  + t.PartyName  as Party,AreaID from PartyMaster t,Login_Users lu 
                           where t.PartyID=lu.UserID and t.IsActive='Y' and lu.UserName + '  =  '  + t.PartyName LIKE '%'+@SearchText+'%' order by lu.UserName";
                    }
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
        protected void Button2_Click(object sender, EventArgs e)
        {
           SetControlProperty();
        }
        protected void LoadProductGroup()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select ProductGroupID,GroupName from ProductGroup where IsActive='Y' order by GroupName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlProductGroup, dt, "ProductGroupID", "GroupName", "Select Product Group");
                forDdl.LoadDll(ddlProductGroupStock, dt, "ProductGroupID", "GroupName", "Select Product Group");
            }
            else
            {

                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Add(new ListItem("No Product Group Available", "0"));
                ddlProductGroupStock.Items.Clear();
                ddlProductGroupStock.Items.Add(new ListItem("No Product Group Available", "0"));
            }
        }
        protected void LoadCategory()
        {
            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.CategoryID,t.Category from Product_Category t where t.ProductGroupID='"+ddlProductGroup.SelectedValue+"' order by t.Category", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadCheckbx(chkCategory, dt, "CategoryID", "Category", "Select Category");
            }
            else
            {

                chkCategory.Items.Clear();
                chkCategory.Items.Add(new ListItem("No Category Available", "0"));

            }
        }
        protected void LoadProduct()
        {
            string CategoryID = "";
            if (!string.IsNullOrEmpty(chkCategory.SelectedValue.ToString()))
            {
                CategoryID = SelectedString(chkCategory);
            }
            DataTable dt;
            DataSet ds = dba.GetDataSet("EXEC GetCategoryWiseProduct '" + CategoryID + "'", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlProductName, dt, "ProdID", "ProdName", "Select Product");
            }
            else
            {

                ddlProductName.Items.Clear();
                ddlProductName.Items.Add(new ListItem("No Product Available", "0"));

            }
        }
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue == "SalesProcessReport")
            {
                ShowReportViewer(null, "");
                divsalesreport.Visible = true;
                divStock.Visible = false;
                SetControlProperty();
               // LoadReports();
                pnlRpt.Visible = false;
            }
            else if (ddlReportType.SelectedValue == "StockReport")
            {
                ShowReportViewer(null, "");
                divsalesreport.Visible = false;
                divStock.Visible = true;
                SetControlProperty();
                //LoadReports();
            }
            else
            {
                Clear();
            }
           
        }

        //protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetControlProperty();
        //    LoadReports();
        //}

        protected void rbtnlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetControlProperty();
            if (ddlreportcategorystock.SelectedValue == "PurchaseDetailsReport" || ddlreportcategorystock.SelectedValue == "ProductWisePurchaseSummary")
            {
                divStock_Purchase.Visible = true;
                divStock_Stock.Visible = false;
            }
            else
            {
                divStock_Purchase.Visible = false;
                divStock_Stock.Visible = true;
            }
            pnlRpt.Visible = false;
            //LoadReports();
        }
        //protected void ddlNumber_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadReports();
        //}

        protected void Clear()
        { 
            divsalesreport.Visible = false;
            divStock.Visible = false;
            pnlRpt.Visible = false;
        }
        protected void SetControlProperty()
        {
          
            //vDate = txtDate.Text.ToString();

            //if (!string.IsNullOrEmpty(vDate))
            //{
            //    vDate = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
            //vFromDate = txtFromDate.Text.ToString();

            //if (!string.IsNullOrEmpty(vFromDate))
            //{
            //    vFromDate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
            //vToDate = txtToDate.Text.ToString();

            //if (!string.IsNullOrEmpty(vToDate))
            //{
            //    vToDate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}

            //vStockFromDate = txtStockFromDate.Text.ToString();

            //if (!string.IsNullOrEmpty(vStockFromDate))
            //{
            //    vStockFromDate = DateTime.ParseExact(txtStockFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
            //vStockToDate = txtStockToDate.Text.ToString();

            //if (!string.IsNullOrEmpty(vStockToDate))
            //{
            //    vStockToDate = DateTime.ParseExact(txtStockToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            //}
          
            SetDates();
            if (String.IsNullOrEmpty(txtDealer.Text.ToString()))
            {
                HIDDealerID.Value = "";
                HIDDealerValue.Value = "";
            }
            if (rbtnlist.SelectedValue == "Order")
            {
                lblWhat.Text = "Order Number";
                LoadNumber("Local_Order_Master", "OrderID", "OrderNo", "Select Order Number", "N");

            }
            else if (rbtnlist.SelectedValue == "ApprovedOrder")
            {
                lblWhat.Text = "Order Number";
                LoadNumber("Local_Order_Master", "OrderID", "OrderNo", "Select Order Number", "Y");
            }
             if (rbtnlist.SelectedValue == "Invoice")
            {
                lblWhat.Text = "Invoice Number";
                LoadNumber("InvoiceMaster", "InvID", "InvNumber", "Select Invoice Number", "N");
            }
             else if (rbtnlist.SelectedValue == "ApprovedInvoice" || rbtnlist.SelectedValue == "SalesSummary")
            {
                lblWhat.Text = "Invoice Number";
                LoadNumber("InvoiceMaster", "InvID", "InvNumber", "Select Invoice Number", "Y");
            }
           
             else if (rbtnlist.SelectedValue == "MoneyReceipt")
             {
                 lblWhat.Text = "Money Receipt Number";
                 LoadNumber("Payment_Master", "PMID", "PMNumber", "Select Money Receipt Number", "N");
             }
        }

        protected void SetDates()
        {
            vDate = txtDate.Text.ToString();

            if (!string.IsNullOrEmpty(vDate))
            {
                vDate = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            vFromDate = txtFromDate.Text.ToString();

            if (!string.IsNullOrEmpty(vFromDate))
            {
                vFromDate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            vToDate = txtToDate.Text.ToString();

            if (!string.IsNullOrEmpty(vToDate))
            {
                vToDate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            vStockFromDate = txtStockFromDate.Text.ToString();

            if (!string.IsNullOrEmpty(vStockFromDate))
            {
                vStockFromDate = DateTime.ParseExact(txtStockFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            vStockToDate = txtStockToDate.Text.ToString();

            if (!string.IsNullOrEmpty(vStockToDate))
            {
                vStockToDate = DateTime.ParseExact(txtStockToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }
        protected void SetLimittedControlProperty()
        {
            SetDates();
           
            if (String.IsNullOrEmpty(txtDealer.Text.ToString()))
            {
                HIDDealerID.Value = "";
                HIDDealerValue.Value = "";
            }
        }
        protected void LoadNumber(string table, string id, string text, string msg,string ApprovalState)
        {
            string qry = "";
          
            string partyid = HIDDealerID.Value;

            qry = "EXEC Get_Number_list '" + table + "','" + partyid + "','" + vFromDate + "','" + vToDate + "','" + ApprovalState + "'";
          
            DataTable dt = dba.GetDataTable(qry);
            if (dt != null)
            {
                forDdl.LoadDll(ddlNumber, dt, id, text, msg);
            }
            else
            {
                ddlNumber.Items.Clear();
                ddlNumber.Items.Add(new ListItem("Not Available", "0"));

            }
        }
      
        protected void ShowReportViewer(DataTable dt,string rptfile)
        {
            vfile = rptfile;

            // vds = ds;
            if (dt != null)
            {
                pnlRpt.Visible = true;
                ReportDocument rdoc = new ReportDocument();
                rdoc.Load(Server.MapPath(rptfile));
                rdoc.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = rdoc;
                Session["RPT"] = rdoc;
                CrystalReportViewer1.HasToggleGroupTreeButton = false;
                CrystalReportViewer1.HasToggleParameterPanelButton = false;
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                CrystalReportViewer1.HasDrilldownTabs = true;
                CrystalReportViewer1.HasPrintButton = true;
                // CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                CrystalReportViewer1.DataBind();
            }
            else
            {
                pnlRpt.Visible = false;

            }
           
        }
        protected string SelectedString(CheckBoxList lstbx)
        {
            string message = "";
            foreach (ListItem item in lstbx.Items)
            {
                if (item.Selected)
                {
                    //message += item.Text + " " + item.Value + "\\n";

                    message = message + "," + item.Value;
                    //  dt.Rows.Add(item.Text);
                }
            }

            if (message != string.Empty)
            {
                message = message.Substring(1);
            }

            return message;
        }
        protected void LoadSalesProcessReports()
        {
            string approvalstatus = "N";
            string vID = "";
            string vpartyID = "";
            string vProductGroupID = "";
            string vproduct = "";
            string vareaID = "";
            string vCategoryID = "";
            string rdbtnvalue = rbtnlist.SelectedValue;
            string rptfile = "";

            if ((ddlNumber.SelectedValue != "0") || (ddlNumber.SelectedIndex != 0))
            {
                vID = ddlNumber.SelectedValue;

            }
            if (!string.IsNullOrEmpty(ddlArea.SelectedValue) || (ddlArea.SelectedIndex != 0))
            {
                vareaID = ddlArea.SelectedValue;

            }
            if (!string.IsNullOrEmpty(HIDDealerID.Value))
            {
                vpartyID = HIDDealerID.Value;

            }
            if (!string.IsNullOrEmpty(ddlProductGroup.SelectedValue) || (ddlProductGroup.SelectedIndex != 0))
            {
                vProductGroupID = ddlProductGroup.SelectedValue;

            }
            if (!string.IsNullOrEmpty(chkCategory.SelectedValue) || (chkCategory.SelectedIndex != 0))
            {
                vCategoryID = SelectedString(chkCategory);

            }
            if (!string.IsNullOrEmpty(ddlProductName.SelectedValue) || (ddlProductName.SelectedIndex != 0))
            {
                vproduct = ddlProductName.SelectedValue;

            }
            if (rdbtnvalue == "ApprovedInvoice" || rdbtnvalue == "ApprovedOrder")
            {
                approvalstatus = "Y";
            }

            //DataSet ds = new DataSet();
            if (rdbtnvalue == "Order" || rdbtnvalue == "ApprovedOrder")
            {
               
                if (vID == "" && vpartyID == "")
                {
                    rptfile = "AllOrderDetailsReport.rpt";
                }
                else
                {
                    rptfile = "OrderDetailsReport.rpt";
                }
                try
                {

                    String query = "EXEC Get_Order_details_Report '" + vID + "','" + vareaID + "','" + vpartyID + "','" + vCategoryID + "','" + approvalstatus + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                       ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }

                }
                catch (Exception ex)
                { throw ex; }

            }
            else if (rdbtnvalue == "Invoice" || rdbtnvalue == "ApprovedInvoice")
            {
                if (vID == "" && vpartyID == "")
                {
                    rptfile = "AllInvoiceDetailsReport.rpt";
                }
                else
                {
                    rptfile = "InvoiceDetailsReport.rpt";
                }
                try
                {

                    String query = "EXEC Get_Invoice_details_Report '" + vID + "','" + vareaID + "','" + vpartyID + "','" + vCategoryID + "','" + approvalstatus + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
            else if (rdbtnvalue == "AllInvoice")
            {

                rptfile = "AllInvoiceDetailsReportAll.rpt";
               
                try
                {

                    String query = "EXEC Get_Invoice_details_Report_all";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
        
            else if (rdbtnvalue == "DeliverdNew")
            {
                if (vpartyID == "")
                {
                    rptfile = "DeliveryDetailsReport_New.rpt";
                }
                else
                {
                    rptfile = "DealerWiseDeliveryReport_New.rpt";
                }


                try
                {

                    String query = "EXEC Get_Delivery_details_Report_New '" + vpartyID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
            else if (rdbtnvalue == "SellReturn")
            {
                rptfile = "SellReturn.rpt";
                try
                {

                    String query = "EXEC getReturn_report '" + vpartyID + "','" + vProductGroupID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
                
          
            else if (rdbtnvalue == "SalesSummaryNew")
            {

                rptfile = "SalesSummaryReport_New.rpt";

                try
                {
                    String query = "EXEC Get_Sales_details_Report_New '" + vID + "','" + vareaID + "','" + vpartyID + "','" + vCategoryID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
            //else if (rdbtnvalue == "ProductWiseSalesSummary")
            //{

            //    rptfile = "ProductWiseSalesSummaryReport.rpt";
               
            //    try
            //    {
            //        String query = "EXEC Get_Sales_Summary_Report '" + vareaID + "','" + vpartyID + "','" + vCategoryID + "','" + vproduct + "','" + vFromDate + "','" + vToDate + "'";
            //        DataTable dt = new DataTable();
            //        dt = dba.GetDataTable(query);
            //        if (dt != null)
            //        {
            //            ShowReportViewer(dt, rptfile);
                       
            //        }
            //        else
            //        {
            //            pnlRpt.Visible = false;
            //        }
            //    }
            //    catch (Exception ex)
            //    { throw ex; }
            //}
            else if (rdbtnvalue == "ProductWiseSalesSummaryNew")
            {

                rptfile = "ProductWiseSalesSummaryReport.rpt";

                try
                {
                    String query = "EXEC Get_Sales_Summary_Report_New '" + vareaID + "','" + vpartyID + "','" + vCategoryID + "','" + vproduct + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
            //else if (rdbtnvalue == "DealerWiseSalesSummary")
            //{

            //    rptfile = "DealerWiseSalesSummaryReport.rpt";

            //    try
            //    {
            //        String query = "EXEC Get_Dealer_Sales_Summary_Report '" + vareaID + "','" + vpartyID + "'";
            //        String queryInvoice = "EXEC Get_Invoice_details_Report '','','','','Y','',''";
            //        String queryPayment = "EXEC Get_MoneyReceipt_details_Report '','','','',''";
            //        DataTable dt = new DataTable();
            //        dt = dba.GetDataTable(query);
            //        DataTable dt1 = new DataTable();
            //        dt1 = dba.GetDataTable(queryInvoice);
            //        DataTable dt2 = new DataTable();
            //        dt2 = dba.GetDataTable(queryPayment);
            //        if (dt != null)
            //        {
            //            pnlRpt.Visible = true;
            //            ReportDocument rdoc = new ReportDocument();
            //            rdoc.Load(Server.MapPath(rptfile));
            //           // rdoc.Subreports[0].SetDataSource(dt1);
            //            rdoc.Subreports[0].SetDataSource(dt1);
            //          //  rdoc.Subreports[1].SetDataSource(dt2);
            //            rdoc.SetDataSource(dt);
            //            CrystalReportViewer1.ReportSource = rdoc;
            //            Session["RPT"] = rdoc;
            //            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            //            CrystalReportViewer1.HasToggleParameterPanelButton = false;
            //            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            //            CrystalReportViewer1.HasDrilldownTabs = true;
            //            CrystalReportViewer1.HasPrintButton = true;
            //            // CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            //            CrystalReportViewer1.DataBind();
            //        }
            //        else
            //        {
            //            pnlRpt.Visible = false;

            //        }
            //    }
            //    catch (Exception ex)
            //    { throw ex; }
            //}

            else if (rdbtnvalue == "DealerWiseSalesSummary")
            {

                rptfile = "DealerWiseSalesSummaryReport.rpt";

                try
                {
                    String query = "EXEC Get_Dealer_Sales_Summary_Report '" + vareaID + "','" + vpartyID + "'";
                    String queryInvoice = "EXEC Get_Invoice_details_Report '','','','','Y','',''";
                    String queryPayment = "EXEC Get_MoneyReceipt_details_Report '','','','',''";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    DataTable dt1 = new DataTable();
                    dt1 = dba.GetDataTable(queryInvoice);
                    DataTable dt2 = new DataTable();
                    dt2 = dba.GetDataTable(queryPayment);
                    if (dt != null)
                    {
                        pnlRpt.Visible = true;
                        ReportDocument rdoc = new ReportDocument();
                        rdoc.Load(Server.MapPath(rptfile));
                        // rdoc.Subreports[0].SetDataSource(dt1);
                        rdoc.Subreports[0].SetDataSource(dt1);
                          rdoc.Subreports[1].SetDataSource(dt2);
                        rdoc.SetDataSource(dt);
                        CrystalReportViewer1.ReportSource = rdoc;
                        Session["RPT"] = rdoc;
                        CrystalReportViewer1.HasToggleGroupTreeButton = false;
                        CrystalReportViewer1.HasToggleParameterPanelButton = false;
                        CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                        CrystalReportViewer1.HasDrilldownTabs = true;
                        CrystalReportViewer1.HasPrintButton = true;
                        // CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
                        CrystalReportViewer1.DataBind();
                    }
                    else
                    {
                        pnlRpt.Visible = false;

                    }
                }
                catch (Exception ex)
                { throw ex; }
            }

            else if (rdbtnvalue == "MoneyReceipt")
            {
               // rptfile = "AllMoneyReceiptDetailsReport.rpt";
                if (vID == "" && vpartyID == "")
                {
                    rptfile = "Reports/AllMoneyReceiptDetailsReport.rpt";
                }
                else
                {
                    rptfile = "Reports/DealerWiseMoneyReceipt.rpt";
                }
                try
                {
                    String query = "EXEC Get_MoneyReceipt_details_Report '" + vID + "','" + vareaID + "','" + vpartyID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);
                     
                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
            }
                
           else if (rdbtnvalue == "DealerWiseAccountStatement")
            {
                  if (!string.IsNullOrEmpty(vpartyID))

                {
                rptfile = "AccountStatementsByDealer.rpt";
               
                try
                {
                    String query = "EXEC Get_AccountSatatment_Report_ByDealer '" + vpartyID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);
                        
                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
                }
                  else
                  {
                      ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please select Dealer and then try again...');", true);
                  }
            }
            else if (rdbtnvalue == "AreaWiseAccountStatement")
            {

                if (!string.IsNullOrEmpty(vareaID))
                {
                    rptfile = "AccountStatementsByArea.rpt";
                    
                }
                else
                {
                    rptfile = "AccountStatementsByAreaAll.rpt";
                }
                    
               
                try
                {
                    String query = "EXEC Get_AccountSatatment_Report_ByArea '" + vareaID + "','" + vFromDate + "','" + vToDate + "'";
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);
                        
                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }
                }
                catch (Exception ex)
                { throw ex; }
           
                 
            }
            else if (rdbtnvalue == "DealerWiseProductGroupAccountStatement")
            {
                if (!string.IsNullOrEmpty(vpartyID) && !string.IsNullOrEmpty(vProductGroupID))

                {
                    rptfile = "AccountStatementsByProductGroup.rpt";
                    try
                    {
                        String query = "EXEC Get_AccountSatatment_Report_ByProductGroupID '" + vpartyID + "','" + vProductGroupID + "','" + vFromDate + "','" + vToDate + "'";
                        DataTable dt = new DataTable();
                        dt = dba.GetDataTable(query);
                        if (dt != null)
                        {
                            ShowReportViewer(dt, rptfile);

                        }
                        else
                        {
                            pnlRpt.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    { throw ex; }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please select Dealer and Product Group then try again...');", true);
                }
                
            }
        }
        protected void LoadStockReports()
        {
           string productgroup=String.Empty;
           string vendor = String.Empty;
           string PurchaseType=String.Empty;
            if (ddlProductGroupStock.SelectedIndex != 0)
            {
                productgroup = ddlProductGroupStock.SelectedValue;
            }
            if (ddlVendor.SelectedIndex != 0)
            {
                vendor = ddlVendor.SelectedValue;
            }
            if (ddlPurchaseType.SelectedIndex != 0)
            {
                PurchaseType = ddlPurchaseType.SelectedItem.Text;
            }
            string rptfile = "";
            String query = "";
            if (ddlreportcategorystock.SelectedValue == "StockReport")
            {
                rptfile = "ImportStockReport.rpt";
                query = "EXEC Get_Stock_Report '','" + productgroup + "','" + vDate + "'";
            }
            else if (ddlreportcategorystock.SelectedValue == "StockLedgerReport")
            {
                rptfile = "StockLedgerReport.rpt";
                query = "EXEC Get_Stock_ledger_Report '" + vDate + "'";
                
            }
            else if (ddlreportcategorystock.SelectedValue == "StockEdgingReport")
            {
                rptfile = "ProductEdgingReport.rpt";
                query = "EXEC Get_Product_Edging_Report '" + vDate + "'";
                
            }

            else if (ddlreportcategorystock.SelectedValue == "PurchaseDetailsReport")
            {
                rptfile = "PurchaseReport.rpt";
                query = "EXEC Get_Purchase_Report '" + vendor + "','" + PurchaseType + "','" + vStockFromDate + "','" + vStockToDate + "'";

            }
            else if (ddlreportcategorystock.SelectedValue == "ProductWisePurchaseSummary")
            {
                rptfile = "ProductWisePurchaseSummaryReport.rpt";
                query = "EXEC Get_ProductWisePurchaseSummary '" + vendor + "','" + PurchaseType + "','" + vStockFromDate + "','" + vStockToDate + "'";

            }
                try
                {
                    
                    DataTable dt = new DataTable();
                    dt = dba.GetDataTable(query);                  
                    if (dt != null)
                    {
                        ShowReportViewer(dt, rptfile);

                    }
                    else
                    {
                        pnlRpt.Visible = false;
                    }

                }
                catch (Exception ex)
                { throw ex; }

            }
        protected void LoadReports()
        {
           
            if (ddlReportType.SelectedValue == "SalesProcessReport")
            {
                divsalesreport.Visible = true;
                divStock.Visible = false;
                LoadSalesProcessReports();
            }
            else if (ddlReportType.SelectedValue == "StockReport")
            {
                divsalesreport.Visible = false;
                divStock.Visible = true;
                LoadStockReports();
            }
            else 
            {
                Clear();
            }
           
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {

            SetControlProperty();
            LoadReports();

        }
        //protected void txtFromDate_TextChanged(object sender, EventArgs e)
        //{

        //    SetControlProperty();
        //    LoadReports();

        //}
        //protected void txtToDate_TextChanged(object sender, EventArgs e)
        //{

        //    SetControlProperty();
        //    LoadReports();
        //}

        //protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetControlProperty();
        //    LoadReports();

        //}

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  LoadParty();
            pnlRpt.Visible = false;
            VAreaID = ddlArea.SelectedValue;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rbtnlist.SelectedValue != "Default")
            {
                SetLimittedControlProperty();
                LoadReports();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Report Category and Then Try Again...');", true);
            }
           
        }
        protected void btnSubmitStock_Click(object sender, EventArgs e)
        {
            if (ddlreportcategorystock.SelectedValue != "Default")
            {
                if ((ddlreportcategorystock.SelectedValue == "StockLedgerReport" || ddlreportcategorystock.SelectedValue == "StockEdgingReport") && string.IsNullOrEmpty(txtDate.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Date and Try again...');", true);

                }
                else
                {
                    SetLimittedControlProperty();
                    LoadReports();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please Select Report Category and Then Try Again...');", true);
            }
           
            
        }
        protected void chkCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct();
            pnlRpt.Visible = false;
            //SetControlProperty();
            //LoadReports();
        }

        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory();
            pnlRpt.Visible = false;
        }
        protected void ddlProductGroupStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReports();
        }
       
       
    }
}