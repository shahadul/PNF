using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

namespace PNF.UI.Reports
{
    public partial class ReportFormCommon : Page
    {
        DatabaseAccess dba = new DatabaseAccess();       
        helperAcc forDdl = new helperAcc();
        public static string vNameValueParam = "";
        public static string vID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
               
              
                LoadReport();
            }
           
        }

        protected void LoadReport()
        {

            vNameValueParam = Request.QueryString["NameValueParam"].ToString();
             vID = Request.QueryString["ID"].ToString();
            if (vNameValueParam == "Invoice")
            {
                LoadInvoiceReport();
            }
            else if (vNameValueParam == "Challan")
            {
                LoadChallanReport();
            }
            else if (vNameValueParam == "Delivery")
            {
                LoadDeliveryReport();
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                CrystalReportViewer1.ReportSource = Session["RPT"];
            }
        }
        protected void LoadInvoiceReport()
        {

            string rptfile = "";
            rptfile = "../Reports/InvoiceReport.rpt";

            try
            {
                String query = "EXEC Get_Challan_By_InvoiceID '" + vID + "'";
                DataTable dt = new DataTable();
                dt = dba.GetDataTable(query);
                if (dt != null)
                {
                    ShowReportViewer(dt, rptfile);

                }
                else
                {
                    dvReport.Visible = false;
                }

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void LoadChallanReport()
        {
           
            string rptfile = "";
            rptfile = "../Reports/ChallanReport.rpt";

            try
            {
                String query = "EXEC Get_Challan_By_ChallanID '" + vID + "'";
                DataTable dt = new DataTable();
                dt = dba.GetDataTable(query);
                if (dt != null)
                {
                    ShowReportViewer(dt, rptfile);

                }
                else
                {
                    dvReport.Visible = false;
                }

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void LoadDeliveryReport()
        {
           
            string rptfile = "";
            rptfile = "../Reports/DeliveryReportSingle.rpt";

            try
            {
                String query = "EXEC Get_Challan_By_ChallanID '" + vID + "'";
                DataTable dt = new DataTable();
                dt = dba.GetDataTable(query);
                if (dt != null)
                {
                    ShowReportViewer(dt, rptfile);

                }
                else
                {
                    dvReport.Visible = false;
                }

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void ShowReportViewer(DataTable dt, string rptfile)
        {
            if (dt != null)
            {
                dvReport.Visible = true;
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
                dvReport.Visible = false;

            }

        }
       
    }
}