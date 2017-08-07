using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using Model;

namespace PNF.UI.MP
{
    public partial class Default : Page
    {
      static DatabaseAccess dba=new DatabaseAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
         
            try
            {
              
                if (Session["UserName"] != null)
                {

                }
                else
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                    Response.End();
                }
            }

            catch (Exception ex)
            {
                Session["er"] = ex.Message;
            }
        }

       

        [WebMethod]
        [ScriptMethod]
        public static List<summary> StockSummaryList()
        {
            List<summary> lstSales = new List<summary>();
            DataTable dt =new DataTable();
           dt= dba.GetDataTable("EXEC Get_GroupWise_Stock_Summary ''");
          
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    summary objsummary = new summary();
                    objsummary.ProductGroupName = dr["GroupName"].ToString();
                    objsummary.TotalQuantity = Convert.ToInt32(dr["TotalQuantity"].ToString());
                    lstSales.Add(objsummary);
                }
            }
            return lstSales;
        }
        [WebMethod]
        [ScriptMethod]
        public static List<summary> PumpSellSummaryList()
        {
            List<summary> lstSales = new List<summary>();
            DataTable dt = new DataTable();
            dt = dba.GetDataTable("EXEC Get_GroupWise_Sales_Summary_New '1'");

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    summary objsummary = new summary();
                    objsummary.ProductGroupName = dr["GroupName"].ToString();
                    objsummary.TotalQuantity = Convert.ToInt32(dr["TotalQuantity"].ToString());
                    objsummary.Month = dr["Months"].ToString();
                    lstSales.Add(objsummary);
                }
            }
            return lstSales;
        }

        [WebMethod]
        [ScriptMethod]
        public static List<summary> KitchenWarSellSummaryList()
        {
            List<summary> lstSales = new List<summary>();
            DataTable dt = new DataTable();
            dt = dba.GetDataTable("EXEC Get_GroupWise_Sales_Summary_New '2'");

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    summary objsummary = new summary();
                    objsummary.ProductGroupName = dr["GroupName"].ToString();
                    objsummary.TotalQuantity = Convert.ToInt32(dr["TotalQuantity"].ToString());
                    objsummary.Month = dr["Months"].ToString();
                    lstSales.Add(objsummary);
                }
            }
            return lstSales;
        }
        [WebMethod]
        [ScriptMethod]
        public static List<GroupWiseSalesSummary> GroupWiseSellSummaryList()
        {
            List<GroupWiseSalesSummary> lstSales = new List<GroupWiseSalesSummary>();
            DataTable dt = new DataTable();
            dt = dba.GetDataTable("EXEC Get_GroupWise_Sales_Summary_New ''");

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GroupWiseSalesSummary objsummary = new GroupWiseSalesSummary();
                    objsummary.Month = dr["Months"].ToString();
                    objsummary.PumpQuantity = Convert.ToInt32(dr["PumpQuantity"].ToString());
                    objsummary.KitchenQuantity = Convert.ToInt32(dr["KitchenQuantity"].ToString());
                    lstSales.Add(objsummary);
                }
            }
            return lstSales;
        }
    }
}