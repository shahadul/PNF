using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Purchase
{
    public partial class ProductConvert : System.Web.UI.Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        helperAcc LoadDll = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
            if (!IsPostBack)
            {
                Session["sgvPurchaseDetail)"] = null;
                Clear();
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
                    cmd.CommandText = @"select T.ProdID,t.ProdName from Product_Master t where t.ProdName LIKE '%'+@SearchText+'%' order by t.ProdName";
                    cmd.Parameters.AddWithValue("@SearchText", Party);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Dealers.Add(string.Format("{0}//{1}", dr["ProdName"], dr["ProdID"]));
                        }
                    }
                    conn.Close();
                }
            }
            return Dealers;

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
           // LoadDetailsGrid(HIDDealerID.Value);

        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "EXEC Get_Convert_Report";
                ds = dba.GetDataSet(query, "ConnDB230");
                WarehouseStock.DataSource = ds;
                WarehouseStock.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
             if (!string.IsNullOrEmpty(HIDFromProductID.Value))
                {
            if (Session["sgvPurchaseDetail)"] != null)
            {

                pnlPurchaseDetails.Visible = true;
                //pnlOrderShow.Visible = false;

                DataTable dt = (DataTable)Session["sgvPurchaseDetail)"];
                DataView dv = new DataView(dt);
                dv.RowFilter = "FromProductId=" + HIDFromProductID.Value;

                DataTable dtChk = dv.ToTable();

                if (dtChk.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                        "alert('The Product Allready Added...');", true);
                }
                else
                {
                    DataTableBuilder();
                    //pnlOrderShow.Visible = false;
                    //pnlOrderDetails.Visible = true;
                    //disableTop();
                }
            }
            else
            {
                DataTableBuilder();
              
                pnlPurchaseDetails.Visible = true;
            }


            //GrandTotalCal();
            ChildClear();
                }
             else
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                         "alert('Please select valid 'from Product'');", true);
             }
        }
        protected void DataTableBuilder()
        {
            // DataTable dtG = GetDataTable(gvPurchaseDetail);
            DataTable dtG = new DataTable();
            if (Session["sgvPurchaseDetail)"] != null)
            {
                dtG = (DataTable)Session["sgvPurchaseDetail)"];
            }
            else
            {
                dtG.Columns.Add("FromProduct");
                dtG.Columns.Add("ToProduct");
                dtG.Columns.Add("Qty");
                dtG.Columns.Add("FromProductId");
                dtG.Columns.Add("ToProductId");
            }


            if (true)
            {
                object[] o = { HIDFromProductValue.Value, HIDToProductValue.Value, txtQty.Text, HIDFromProductID.Value, HIDToProductID.Value };
               
                dtG.Rows.Add(o);

                if (dtG.Rows.Count > 0)
                {
                    gvPurchaseDetail.DataSource = null;
                    gvPurchaseDetail.DataBind();
                    gvPurchaseDetail.DataSource = dtG;
                    gvPurchaseDetail.DataBind();

                    pnlPurchaseDetails.Visible = true;
                    //  EnabledisableTop(false);
                }
                else
                {
                    pnlPurchaseDetails.Visible = false;
                }
            }
            Session["sgvPurchaseDetail)"] = dtG;
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
            txtFromProductName.Text = String.Empty;
            txtToProductName.Text = String.Empty;
            txtQty.Text = String.Empty;
            btnSubmit.Text = "Submit";
            txtRemarks.Text=String.Empty;
            LoadGrid();
            Session["sgvPurchaseDetail)"] = null;
            pnlPurchaseDetails.Visible = false;
        }
        protected void ChildClear()
        {

            txtFromProductName.Text = String.Empty;
            txtToProductName.Text = String.Empty;
            txtQty.Text = String.Empty;

        }
        protected void Save()
        {


            string UserID = Session["UserID"].ToString();
            string vRemarks = txtRemarks.Text;
            string result = "";
         
            if (Session["sgvPurchaseDetail)"] != null)
            {
                gvPurchaseDetail.AllowPaging = false;
                gvPurchaseDetail.DataSource = (DataTable)Session["sgvPurchaseDetail)"];
                gvPurchaseDetail.DataBind();
            }

            try
            {



                if (gvPurchaseDetail.Rows.Count > 0)
                {

                    double Totl = 0;
                    Int32 totalQTY = 0;
                    string resOut = string.Empty;
                    string vDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    foreach (GridViewRow row in gvPurchaseDetail.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Int32 qty = Convert.ToInt32(nullChecker(row.Cells[2].Text));
                            totalQTY += qty;
                        }
                    }

                    string qryPurchase =
                        "insert into Purchase (Date,Remarks,PurchaseType,CreatedBy,IsActive) values " +
                        "('" + vDate + "','" + vRemarks + "','Convert','" + UserID + "','Y') SELECT CAST(scope_identity() AS varchar(36))";
                    string PurchaseID = dba.GetObjectDataId(qryPurchase, "ConnDB230");

                    if (PurchaseID != string.Empty)
                    {
                        foreach (GridViewRow row in gvPurchaseDetail.Rows)
                        {
                            string g = Guid.NewGuid().ToString("N");
                          
                            double vDealerPrice = 0;
                            double vRetailPrice = 0;
                            int vReorderPoint = 0;

                            Int32 qty = Convert.ToInt32(nullChecker(row.Cells[2].Text));
                            double ImportPrice = 0;
                            double TotaL = 0;
                            string fromproductid = nullChecker(row.Cells[3].Text);
                            string toproductid = nullChecker(row.Cells[4].Text);

                            string query = "select top(1)t.PurchaseID,t.Price,p.DealerPrice,p.ReorderPoint from PurchaseDetail t,Product_Master p where t.IStockID=p.ProdID and t.IStockID='" + fromproductid + "' order by t.PurchaseID desc";
                            DataTable dt1 = dba.GetDataTable(query);
                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dt1.Rows[0][1].ToString()))
                                {
                                    ImportPrice = Convert.ToDouble(dt1.Rows[0][1].ToString());
                                }
                                if (!string.IsNullOrEmpty(dt1.Rows[0][2].ToString()))
                                {
                                    vDealerPrice = Convert.ToDouble(dt1.Rows[0][2].ToString());
                                }
                                if (!string.IsNullOrEmpty(dt1.Rows[0][3].ToString()))
                                {
                                    vReorderPoint = Convert.ToInt32(dt1.Rows[0][3].ToString());
                                }
                            }
                            TotaL = qty*ImportPrice;
                            Int32 fromqty = 0;
                            fromqty = qty * -1;
                            //double fromImportPrice = ImportPrice * -1;
                            double fromTotal = 0;
                            fromTotal = TotaL * -1;
                            string QryfromproductPurchaseDetail = "insert into PurchaseDetail (PurchaseID,IStockID,Qty,Price,Total,CreatedBy,ConvertDetailID) values " +
                                                  "('" + PurchaseID + "','" + fromproductid + "','" + fromqty + "','" + ImportPrice + "','" + fromTotal + "','" + UserID + "','" + g + "')";

                            resOut = dba.ExecuteQuery(QryfromproductPurchaseDetail, "ConnDB230");
                            

                            if (resOut == "1")
                            {
                                int UpdatedQty = 0;
                                string vStockQty = "select StockQty from IStock where ProdID='" + fromproductid + "'";
                                DataTable dt = dba.GetDataTable(vStockQty);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) - qty;
                                    string sqlCat = "update IStock  set StockQty='" + UpdatedQty + "' where ProdID='" + fromproductid + "'";
                                    result = dba.ExecuteQuery(sqlCat, "ConnDB230");

                                }
                                else
                                {
                                    string sqlCat = "insert into IStock(ProdID,StockQty,ImportPrice,DealerPrice,RetailPrice,Reorder,AdminUserID) values('" + fromproductid + "','" + qty + "','" + ImportPrice + "','" + vDealerPrice + "','" + vRetailPrice + "','" + vReorderPoint + "','" + UserID + "')";
                                    result = dba.ExecuteQuery(sqlCat, "ConnDB230");
                                }

                                string QrytoproductPurchaseDetail = "insert into PurchaseDetail (PurchaseID,IStockID,Qty,Price,Total,CreatedBy,ConvertDetailID) values " +
                                                "('" + PurchaseID + "','" + toproductid + "','" + qty + "','" + ImportPrice + "','" + TotaL + "','" + UserID + "','"+g+"')";

                                resOut = dba.ExecuteQuery(QrytoproductPurchaseDetail, "ConnDB230");

                                int UpdatedQty1 = 0;
                                string vStockQty1 = "select StockQty from IStock where ProdID='" + toproductid + "'";
                                dt = dba.GetDataTable(vStockQty1);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    UpdatedQty1 = Convert.ToInt32(dt.Rows[0][0].ToString()) + qty;
                                    string sqlCat = "update IStock  set StockQty='" + UpdatedQty1 + "' where ProdID='" + toproductid + "'";
                                    result = dba.ExecuteQuery(sqlCat, "ConnDB230");

                                }
                                else
                                {
                                    string sqlCat = "insert into IStock(ProdID,StockQty,ImportPrice,DealerPrice,RetailPrice,Reorder,AdminUserID) values('" + toproductid + "','" + qty + "','" + ImportPrice + "','" + vDealerPrice + "','" + vRetailPrice + "','" + vReorderPoint + "','" + UserID + "')";
                                    result = dba.ExecuteQuery(sqlCat, "ConnDB230");
                                }
                            }

                            

                        }

                    }


                }

                if (result == "1")
                {
                    Clear();
                    ChildClear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    LoadGrid();
                }



            }

            catch { }


        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            Stockcheck();
        }

        protected void Stockcheck()
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                if (!string.IsNullOrEmpty(HIDFromProductID.Value))
                {
                    string sqlq1 = "select isnull(t.StockQty,0) from IStock t where t.ProdID='" + HIDFromProductID.Value +
                                   "'";
                    DataTable dt1 = dba.GetDataTable(sqlq1);
                    Int32 vstockqty = 0;
                    if (dt1 != null)
                    {
                        vstockqty = Convert.ToInt32(dt1.Rows[0][0].ToString());
                    }
                    if ((Convert.ToInt32(txtQty.Text)) >= vstockqty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                            "alert('Invalid Quantity!! You have " + vstockqty + " Quantity in stock');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                            "alert('Please select valid 'from Product'');", true); 
                }
            }
        }

        protected void gvPurchaseDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = new DataTable();
            if (Session["sgvPurchaseDetail)"] != null)
            {
                dt = (DataTable)Session["sgvPurchaseDetail)"];
            }

            dt.Rows[index].Delete();
            //ViewState["dt"] = dt;
            gvPurchaseDetail.DataSource = dt;
            gvPurchaseDetail.DataBind();
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                //  EnabledisableTop(true);
                pnlPurchaseDetails.Visible = false;
            }
          //  GrandTotalCal();
        }

        protected void gvPurchaseDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchaseDetail.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)Session["sgvPurchaseDetail)"];
            gvPurchaseDetail.DataSource = null;
            gvPurchaseDetail.DataSource = dt;
            gvPurchaseDetail.DataBind();

        }
        protected void WarehouseStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            WarehouseStock.PageIndex = e.NewPageIndex;
            LoadGrid();

        }
    }
}