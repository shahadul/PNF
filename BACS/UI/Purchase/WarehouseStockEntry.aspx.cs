
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Purchase
{
    public partial class WarehouseStockEntry : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        helperAcc LoadDll = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            //LoadCategory();
                LoadProductGroup();
                LoadVendor();
                LoadGrid();
                LoadPurchaseType();
                Session["sgvPurchaseDetail)"] = null;
            }
           
            
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModel();
        }
        protected void LoadPurchaseType()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select PurchaseTypeId,PurchaseType from PurchaseType", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                LoadDll.LoadDll(ddlPurchaseType, dt, "PurchaseTypeId", "PurchaseType", "Select Purchase Type");
            }
            else
            {

                ddlPurchaseType.Items.Clear();
                ddlPurchaseType.Items.Add(new ListItem("No Purchase Type Available", "0"));

            }
        }
        protected void LoadVendor()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("Select * from Vendor where IsActive='Y' order by CompanyName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                LoadDll.LoadDll(ddlVendor, dt, "VendorID", "CompanyName", "Select Vendor");
            }
            else
            {

                ddlVendor.Items.Clear();
                ddlVendor.Items.Add(new ListItem("No Vendor Available", "0"));

            }
        }
        protected void LoadProductGroup()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select ProductGroupID,GroupName from ProductGroup where IsActive='Y' order by GroupName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                LoadDll.LoadDll(ddlProductGroup, dt, "ProductGroupID", "GroupName", "Select Product Group");
            }
            else
            {

                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Add(new ListItem("No Product Group Available", "0"));

            }
        }
        protected void LoadCategory()
        {
            string qry = "select CategoryID,Category from Product_Category where ProductGroupID='" + ddlProductGroup.SelectedValue + "' order by Category";
            
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                
                LoadDll.LoadDll(ddlcategory, dt, "CategoryID", "Category", "Select Category");
            }
            else
            {

                ddlcategory.Items.Clear();
                ddlcategory.Items.Add(new ListItem("No Category Available", "0"));

            }
        }
        protected void LoadModel()
        {
            string qry = "select t.ModelID,t.Model from Product_Model t where t.CategoryID='" + ddlcategory.SelectedValue + "' and t.IsActive='Y' order by t.Model";

            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                LoadDll.LoadDll(ddlModel, dt, "ModelID", "Model", "Select Model");
            }
            else
            {

                ddlModel.Items.Clear();
                ddlModel.Items.Add(new ListItem("No Model Available", "0"));

            }
        }
        protected void LoadProduct()
        {
            string qry = "select ProdID,ProdName from Product_Master where ModelID='" + ddlModel.SelectedValue + "' and IsActive='Y' order by ProdName ";
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                LoadDll.LoadDll(ddlProductName, dt, "ProdID", "ProdName", "Select Product");
            }
            else
            {

                ddlProductName.Items.Clear();
                ddlProductName.Items.Add(new ListItem("No Product Available", "0"));

            }
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "EXEC Get_Stock_Report '',''";
                ds = dba.GetDataSet(query, "ConnDB230");
                WarehouseStock.DataSource = ds;
                WarehouseStock.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void Clear()
        {
            ddlProductGroup.SelectedIndex = -1;
            ddlProductName.SelectedIndex = -1;
            ddlModel.SelectedIndex = -1;
            ddlcategory.SelectedIndex = -1;
            ddlPurchaseType.SelectedIndex = -1;
            txtQty.Text = "";
            btnSubmit.Text = "Submit";
            txtGrossTotal.Text = "0";
            LoadGrid();
            Session["sgvPurchaseDetail)"] = null;
            pnlPurchaseDetails.Visible = false;
        }
        protected void ChildClear()
        {
            txtImportPrice.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            txtSL.Text = string.Empty;
            txtWarranty.Text=String.Empty;
           
            ddlModel.SelectedIndex = 0;
            ddlProductName.SelectedIndex = 0;
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
           
                if (Session["sgvPurchaseDetail)"] != null)
                {
                    pnlPurchaseDetails.Visible = true;
                    //pnlOrderShow.Visible = false;

                    DataTable dt = (DataTable)Session["sgvPurchaseDetail)"];
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "ProductId=" + ddlProductName.SelectedValue;

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
                   // pnlOrderShow.Visible = false;
                    pnlPurchaseDetails.Visible = true;
                }


                GrandTotalCal();
                ChildClear();
               
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
                dtG.Columns.Add("ProductName");
                dtG.Columns.Add("Qty");
                dtG.Columns.Add("ImportPrice");
                dtG.Columns.Add("Total");
                dtG.Columns.Add("S/N");
                dtG.Columns.Add("Warranty");
                dtG.Columns.Add("ProductId");
            }


            if (true)
            {
                object[] o =
                {ddlProductName.SelectedItem.Text.Trim(),txtQty.Text.Trim(),Convert.ToDouble(txtImportPrice.Text.Trim()),Convert.ToDouble(txtTotalPrice.Text.Trim())
                    ,txtSL.Text.Trim(),txtWarranty.Text.Trim(),ddlProductName.SelectedValue.Trim()};
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
      
       
        protected void Save()
        {

            string vInvoiceDate = DateTime.ParseExact(txtInvoiceDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                           
            string vVendor = ddlVendor.SelectedValue;
            string vInvoiceNo = txtInvoiceNo.Text;
            string UserID = Session["UserID"].ToString();
            string vRemarks = txtRemarks.Text;
            string result = "";
            string PurchaseType = ddlPurchaseType.SelectedItem.Text;

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
                           
                            foreach (GridViewRow row in gvPurchaseDetail.Rows)
                            {
                                if (row.RowType == DataControlRowType.DataRow)
                                {
                                    Int32 qty = Convert.ToInt32(nullChecker(row.Cells[1].Text));
                                    totalQTY += qty;
                                    Totl += Convert.ToDouble(nullChecker(row.Cells[3].Text));
                                }
                            }
                           
                            string qryPurchase =
                                "insert into Purchase (InvoiceNo,Date,VendorID,PurchaseType,Remarks,Gross,CreatedBy,IsActive) values " +
                                "('" + vInvoiceNo + "','" + vInvoiceDate + "','" + vVendor + "','" + PurchaseType + "','" + vRemarks + "','" + Totl + "','" + UserID + "','Y') SELECT CAST(scope_identity() AS varchar(36))";
                            string PurchaseID = dba.GetObjectDataId(qryPurchase, "ConnDB230");
                        
                            if (PurchaseID != string.Empty)
                            {
                                    foreach (GridViewRow row in gvPurchaseDetail.Rows)
                                    {
                                        double vDealerPrice = 0;
                                        double vRetailPrice = 0;
                                        int vReorderPoint = 0;
                                        
                                        Int32 qty = Convert.ToInt32(nullChecker(row.Cells[1].Text));
                                        double ImportPrice = Convert.ToDouble(nullChecker(row.Cells[2].Text));
                                        double TotaL = Convert.ToDouble(nullChecker(row.Cells[3].Text));
                                        string vSN = row.Cells[4].Text;
                                        int vWarranty = Convert.ToInt32(nullChecker(row.Cells[5].Text));
                                       // vRemarks = row.Cells[6].Text;
                                        string productid = nullChecker(row.Cells[6].Text);

                                        string query = "select t.DealerPrice,t.ImportCost,t.RetailPrice,t.ReorderPoint from Product_Master t  where t.ProdID='" + productid + "' and t.IsActive='Y'";
                                        DataTable dt1 = dba.GetDataTable(query);
                                        if (dt1 != null && dt1.Rows.Count > 0)
                                        {
                                            if (!string.IsNullOrEmpty(dt1.Rows[0][0].ToString()))
                                            {
                                                vDealerPrice = Convert.ToDouble(dt1.Rows[0][0].ToString());
                                            }

                                            if (!string.IsNullOrEmpty(dt1.Rows[0][2].ToString()))
                                            {
                                                vRetailPrice = Convert.ToDouble(dt1.Rows[0][2].ToString());
                                            }
                                            if (!string.IsNullOrEmpty(dt1.Rows[0][3].ToString()))
                                            {
                                                vReorderPoint = Convert.ToInt32(dt1.Rows[0][3].ToString());
                                            }
                                        }
                                        string QryPurchaseDetail = "insert into PurchaseDetail (PurchaseID,IStockID,SL_N,Qty,Price,Total,CreatedBy,Warranty) values " +
                                                              "('" + PurchaseID + "','" + productid + "','" + vSN + "','" + qty + "','" + ImportPrice + "','" + TotaL + "','" + UserID + "','" + vWarranty + "')";

                                        resOut = dba.ExecuteQuery(QryPurchaseDetail, "ConnDB230");

                                        int UpdatedQty = 0;
                                        string vStockQty = "select StockQty from IStock where ProdID='" + productid + "'";
                                        DataTable dt = dba.GetDataTable(vStockQty);
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            UpdatedQty = Convert.ToInt32(dt.Rows[0][0].ToString()) + qty;
                                            string sqlCat = "update IStock  set StockQty='" + UpdatedQty + "',ImportPrice='" + ImportPrice + "',DealerPrice='" + vDealerPrice + "',RetailPrice='" + vRetailPrice + "',Reorder='" + vReorderPoint + "',AdminUserID='" + UserID + "' where ProdID='" + productid + "'";
                                            result = dba.ExecuteQuery(sqlCat, "ConnDB230");

                                        }
                                        else
                                        {
                                            string sqlCat = "insert into IStock(ProdID,StockQty,ImportPrice,DealerPrice,RetailPrice,Reorder,AdminUserID) values('" + productid + "','" + qty + "','" + ImportPrice + "','" + vDealerPrice + "','" + vRetailPrice + "','" + vReorderPoint + "','" + UserID + "')";
                                            result = dba.ExecuteQuery(sqlCat, "ConnDB230");
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
        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }
        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        protected void WarehouseStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            WarehouseStock.PageIndex = e.NewPageIndex;
            LoadGrid();

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
            GrandTotalCal();
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

        protected void gvPurchaseDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select t.DealerPrice,t.ImportCost,t.RetailPrice,t.ReorderPoint from Product_Master t  where t.ProdID='" + ddlProductName.SelectedValue + "'";
            DataTable dt1 = dba.GetDataTable(query);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
               
                if (!string.IsNullOrEmpty(dt1.Rows[0][1].ToString()))
                {
                    txtImportPrice.Text = dt1.Rows[0][1].ToString();
                }
              
            }
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
        protected void GrandTotalCal()
        {
            if (Session["sgvPurchaseDetail)"] != null)
            {
                gvPurchaseDetail.AllowPaging = false;
                gvPurchaseDetail.DataSource = (DataTable)Session["sgvPurchaseDetail)"];
                gvPurchaseDetail.DataBind();
            }
            double GrandTotl = 0;
            if (gvPurchaseDetail.Rows.Count > 0)
            {
                foreach (GridViewRow Row in gvPurchaseDetail.Rows)
                {

                    GrandTotl += Convert.ToDouble(nullChecker(Row.Cells[3].Text));
                }

            }
            txtGrossTotal.Text = Convert.ToString(GrandTotl);
            if (Session["sgvPurchaseDetail)"] != null)
            {
                gvPurchaseDetail.AllowPaging = true;
                gvPurchaseDetail.DataSource = (DataTable)Session["sgvPurchaseDetail)"];
                gvPurchaseDetail.DataBind();
            }
        }
        protected void TotalCal()
        {
           
            if (txtQty.Text == string.Empty || txtImportPrice.Text == string.Empty)
            {
                txtTotalPrice.Text = string.Empty;
            }
            else
            {
                double TotalPr = (Convert.ToDouble(txtImportPrice.Text) * Convert.ToDouble(txtQty.Text));
                txtTotalPrice.Text = Convert.ToString(TotalPr);
            }
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            TotalCal();
        }

        protected void txtImportPrice_TextChanged(object sender, EventArgs e)
        {
            TotalCal();
        }
    }
}