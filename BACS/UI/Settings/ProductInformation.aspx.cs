
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class ProductInformation : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                LoadProductGroup();
                LoadGrid();
                setstatus();
            }
        }
        protected void LoadProductGroup()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select ProductGroupID,GroupName from ProductGroup where IsActive='Y' order by GroupName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlProductGroup, dt, "ProductGroupID", "GroupName", "Select Product Group");
            }
            else
            {

                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Add(new ListItem("No Product Group Available", "0"));

            }
        }
        protected void setstatus()
        {
            pnlDetails.Visible = false;
            lblEOLDate.Visible = false;
            txtEOLDate.Visible = false;
            chkIsDetails.SelectedValue = "0";
            chkIsEOL.SelectedValue = "0";
            rdbtnIsActive.SelectedValue = "1";
        }

        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.ProdID,t.ProdName,t.Barcode,t.BarcodeTemp,t.DateAdded,t.DateModified,t.DealerPrice,t.Discount,t.EOLDate,t.ICode,t.ImportCost,
                t.IsActive,t.IsDetail,t.IsEOL,t.ReorderPoint,t.RetailPrice,t.Short1,t.Short2,
                d.Atrbt1,d.Atrbt2,d.Atrbt3,d.Atrbt4,d.Atrbt5,d.Atrbt6,d.Atrbt7,d.Atrbt8,d.Atrbt9,d.Atrbt10,u.UserName as AddedBy 
                ,pc.Category,pm.Model,d.ProdDetailID,t.ModelID,t.CategoryID,pc.ProductGroupID
                from Product_Master t
                left outer join Product_Details d on t.ProdID=d.ProdID
                left outer join Login_Users u on t.AddedBy=u.LogUserID
                left outer join Product_Category pc on t.CategoryID=pc.CategoryID
                left outer join Product_Model pm on t.ModelID=pm.ModelID
                where t.IsActive='Y' order by t.ProdID desc";
                ds = dba.GetDataSet(query, "ConnDB230");
                GRVProductInfo.DataSource = ds;
                GRVProductInfo.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void LoadCategory()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select CategoryID,Category from Product_Category where ProductGroupID='" + ddlProductGroup.SelectedValue + "' order by Category", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlcategory, dt, "CategoryID", "Category", "Select Category");
            }
            else
            {

                ddlcategory.Items.Clear();
                ddlcategory.Items.Add(new ListItem("No Category Available", "0"));

            }
        }
        protected void LoadProductModel()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.ModelID,t.Model from Product_Model t where t.IsActive='Y' and t.CategoryID='"+ddlcategory.SelectedValue+"' order by t.Model", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlproductmodel, dt, "ModelID", "Model", "Select Product Model");
            }
            else
            {

                ddlproductmodel.Items.Clear();
                ddlproductmodel.Items.Add(new ListItem("No Product Model Available", "0"));

            }
        }
        private void Clear()
        {
         
            txtBarCode.Text = string.Empty;
            txtBarCodeTemp.Text = string.Empty;
            ddlcategory.SelectedIndex = 0;
            ddlproductmodel.SelectedIndex = 0;
          
            txtDealerPrice.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtEOLDate.Text= "";

            txtImpcost.Text = string.Empty;
            txtImportCode.Text = string.Empty;
       
            txtProductName.Text =string.Empty;
            txtReorderPoint.Text =string.Empty;
            txtRetailPrice.Text =string.Empty;
            txtShortName1.Text = string.Empty;
            txtShortName2.Text = string.Empty;
            txtAtrbt1.Text = string.Empty;
            txtAtrbt2.Text = string.Empty;
            txtAtrbt3.Text = string.Empty;
            txtAtrbt4.Text = string.Empty;
            txtAtrbt5.Text = string.Empty;
            txtAtrbt6.Text = string.Empty;
            txtAtrbt7.Text = string.Empty;
            txtAtrbt8.Text = string.Empty;
            txtAtrbt9.Text = string.Empty;
            txtAtrbt10.Text = string.Empty;
            txtProductName.Focus();
            setstatus();
        }
        protected void Save()
        {
            string Isdetails = "N";
            string IsEOL = "N";
            string IsActive = "N";

            string BarCode = txtBarCode.Text.ToString();
            string BarCodeTemp = txtBarCodeTemp.Text.ToString();
            string category = ddlcategory.SelectedValue;
            string productmodel = ddlproductmodel.SelectedValue;

            string DealerPrice = txtDealerPrice.Text.ToString();
            string Discount = txtDiscount.Text.ToString();
            string vEOLDate = txtEOLDate.Text.ToString();
            string EOLDate=string.Empty;
            if (!string.IsNullOrEmpty(vEOLDate))
            {
                EOLDate = DateTime.ParseExact(txtEOLDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else 
            {
                EOLDate = DateTime.ParseExact(DateTime.Today.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            string Impcost = txtImpcost.Text.ToString();
            string ImportCode = txtImportCode.Text.ToString();

            string ProductName = txtProductName.Text.ToString();
            string ReorderPoint = txtReorderPoint.Text.ToString();
            string RetailPrice = txtRetailPrice.Text.ToString();

            string ShortName1 = txtShortName1.Text.ToString();
            string ShortName2 = txtShortName2.Text.ToString();

            string Atrbt1 = txtAtrbt1.Text.ToString();
            string Atrbt2 = txtAtrbt2.Text.ToString();
            string Atrbt3 = txtAtrbt3.Text.ToString();
            string Atrbt4 = txtAtrbt4.Text.ToString();
            string Atrbt5 = txtAtrbt5.Text.ToString();
            string Atrbt6 = txtAtrbt6.Text.ToString();
            string Atrbt7 = txtAtrbt7.Text.ToString();
            string Atrbt8 = txtAtrbt8.Text.ToString();
            string Atrbt9 = txtAtrbt9.Text.ToString();
            string Atrbt10 = txtAtrbt10.Text.ToString();
            
            string UserID = Session["UserID"].ToString();
            if (chkIsDetails.SelectedValue == "0")
            {
                Isdetails = "N";
            }
            else
            {
                Isdetails = "Y";
            }
            if (chkIsEOL.SelectedValue == "0")
            {
                IsEOL = "N";
            }
            else
            {
                IsEOL = "Y";
            }
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }

            if (btnSubmit.Text != "Update")
            {
                string insertresult = "";
                if (chkIsDetails.SelectedValue == "0")
                {
                    try
                    {
                        string sqlproduct = @"insert into Product_Master(AddedBy,Barcode,BarcodeTemp,CategoryID,DealerPrice,Discount,EOLDate,ICode,
                            ImportCost,IsActive,IsDetail,IsEOL,ModelID,ProdName,ReorderPoint,RetailPrice,Short1,Short2)
                            values ('" + UserID + "','" + BarCode + "','" + BarCodeTemp + "','" + category + "','" + DealerPrice + "','" + Discount + "','" + EOLDate + "','" + ImportCode + "','" + Impcost + "','" + IsActive + "','" + Isdetails + "','" + IsEOL + "','" + productmodel + "','" + ProductName + "','" + ReorderPoint + "','" + RetailPrice + "','" + ShortName1 + "','" + ShortName2 + "' )";
                        insertresult = dba.ExecuteQuery(sqlproduct, "ConnDB230");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    try
                    {
                        
                        string sqlproduct = @"insert into Product_Master(AddedBy,Barcode,BarcodeTemp,CategoryID,DealerPrice,Discount,EOLDate,ICode,
                            ImportCost,IsActive,IsDetail,IsEOL,ModelID,ProdName,ReorderPoint,RetailPrice,Short1,Short2)
                            values ('" + UserID + "','" + BarCode + "','" + BarCodeTemp + "','" + category + "','" + DealerPrice + "','" + Discount + "','" + EOLDate + "','" + ImportCode + "','" + Impcost + "','" + IsActive + "','" + Isdetails + "','" + IsEOL + "','" + productmodel + "','" + ProductName + "','" + ReorderPoint + "','" + RetailPrice + "','" + ShortName1 + "','" + ShortName2 + "' )" + "SELECT CAST(scope_identity() AS varchar(36))";
                        string resultproduct = dba.getObjectDataStr(sqlproduct, "ConnDB230");

                        if (!string.IsNullOrEmpty(resultproduct))
                        {
                            string sqlproductdetails = @"insert into Product_Details(ProdID,Atrbt1,Atrbt2,Atrbt3,Atrbt4,Atrbt5,Atrbt6,Atrbt7,Atrbt8,Atrbt9,Atrbt10)
                            values ('" + resultproduct + "','" + Atrbt1 + "','" + Atrbt2 + "','" + Atrbt3 + "','" + Atrbt4 + "','" + Atrbt5 + "','" + Atrbt6 + "','" + Atrbt7 + "','" + Atrbt8 + "','" + Atrbt9 + "','" + Atrbt10 + "' )";
                            insertresult = dba.ExecuteQuery(sqlproductdetails, "ConnDB230");
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception();
                    }

                }
                if (insertresult == "1")
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    LoadGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Not Saved...');", true);

                }
            }
            else
            {
                string updateresult = "";
                if (chkIsDetails.SelectedValue == "0")
                {
                    try
                    {
                        string sqlproduct = @"update Product_Master set ModifiedBy='" + UserID + "',Barcode='" + BarCode + "',BarcodeTemp='" + BarCodeTemp + "',CategoryID='" + category + "',DealerPrice='" + DealerPrice + "',Discount='" + Discount + "',EOLDate='" + EOLDate + "',ICode='" + ImportCode + "',ImportCost='" + Impcost + "',IsActive='" + IsActive + "',IsDetail='" + Isdetails + "',IsEOL='" + IsEOL + "',ModelID='" + productmodel + "',ProdName='" + ProductName + "',ReorderPoint='" + ReorderPoint + "',RetailPrice='" + RetailPrice + "',Short1='" + ShortName1 + "',Short2='" + ShortName2 + "' where ProdID='" + HIDProdID.Value + "'";

                        updateresult = dba.ExecuteQuery(sqlproduct, "ConnDB230");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    try
                    {

                       
                        string sqlproduct = @"update Product_Master set ModifiedBy='" + UserID + "',Barcode='" + BarCode + "',BarcodeTemp='" + BarCodeTemp + "',CategoryID='" + category + "',DealerPrice='" + DealerPrice + "',Discount='" + Discount + "',EOLDate='" + EOLDate + "',ICode='" + ImportCode + "',ImportCost='" + Impcost + "',IsActive='" + IsActive + "',IsDetail='" + Isdetails + "',IsEOL='" + IsEOL + "',ModelID='" + productmodel + "',ProdName='" + ProductName + "',ReorderPoint='" + ReorderPoint + "',RetailPrice='" + RetailPrice + "',Short1='" + ShortName1 + "',Short2='" + ShortName2 + "' where ProdID='" + HIDProdID.Value + "'";
                        updateresult = dba.ExecuteQuery(sqlproduct, "ConnDB230");

                        if(updateresult=="1")
                        {
                            if (!string.IsNullOrEmpty(HIDProdDetailID.Value))
                            {
                                string sqlproductdetails = @"update Product_Details set Atrbt1='" + Atrbt1 + "',Atrbt2='" + Atrbt2 + "',Atrbt3='" + Atrbt3 + "',Atrbt4='" + Atrbt4 + "',Atrbt5='" + Atrbt5 + "',Atrbt6='" + Atrbt6 + "',Atrbt7='" + Atrbt7 + "',Atrbt8='" + Atrbt8 + "',Atrbt9='" + Atrbt9 + "',Atrbt10='" + Atrbt10 + "' where ProdDetailID='" + HIDProdDetailID.Value + "' and ProdID='" + HIDProdID.Value + "'";
                                updateresult = dba.ExecuteQuery(sqlproductdetails, "ConnDB230");
                            }
                            else 
                            {
                                string sqlproductdetails = @"insert into Product_Details(ProdID,Atrbt1,Atrbt2,Atrbt3,Atrbt4,Atrbt5,Atrbt6,Atrbt7,Atrbt8,Atrbt9,Atrbt10)
                            values ('" + HIDProdID.Value + "','" + Atrbt1 + "','" + Atrbt2 + "','" + Atrbt3 + "','" + Atrbt4 + "','" + Atrbt5 + "','" + Atrbt6 + "','" + Atrbt7 + "','" + Atrbt8 + "','" + Atrbt9 + "','" + Atrbt10 + "' )";
                                updateresult = dba.ExecuteQuery(sqlproductdetails, "ConnDB230");
                            }


                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception();
                    }

                }

                if (updateresult == "1")
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    LoadGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Not Saved...');", true);

                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            
        }

        protected void chkIsDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkIsDetails.SelectedValue != "1")
            {
                pnlDetails.Visible = false;
            }
            else 
            {
                pnlDetails.Visible = true;
                txtAtrbt1.Focus();
            }
        }

        protected void chkIsEOL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkIsEOL.SelectedValue != "1")
            {
                txtEOLDate.Visible = false;
                lblEOLDate.Visible = false;
            }
            else
            {
                txtEOLDate.Visible = true;
                lblEOLDate.Visible = true;
            }
        }

        protected void GRVProductInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GRVProductInfo.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void GRVProductInfo_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDProdID.Value = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[1].Text.ToString().Replace("&nbsp;", "");
            HIDProdDetailID.Value = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[31].Text.ToString().Replace("&nbsp;", "");
            txtProductName.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtBarCode.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtBarCodeTemp.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
            txtDealerPrice.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[5].Text.Replace("&nbsp;", "");
            txtDiscount.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[6].Text.Replace("&nbsp;", "");
            txtEOLDate.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");

            txtImportCode.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
            txtImpcost.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[9].Text.ToString().Replace("&nbsp;", "");
           if(GRVProductInfo.Rows[e.NewSelectedIndex].Cells[10].Text.ToString().Replace("&nbsp;", "")=="Y")
            {
            rdbtnIsActive.SelectedValue="1";
            }
            else
            { rdbtnIsActive.SelectedValue="0";
            }
           if(GRVProductInfo.Rows[e.NewSelectedIndex].Cells[11].Text.ToString().Replace("&nbsp;", "")=="Y")
            {
            chkIsDetails.SelectedValue="1";
            }
            else
            { chkIsDetails.SelectedValue="0";
            }
           if(GRVProductInfo.Rows[e.NewSelectedIndex].Cells[12].Text.ToString().Replace("&nbsp;", "")=="Y")
            {
            chkIsEOL.SelectedValue="1";
            }
            else
            { chkIsEOL.SelectedValue="0";
            }
            txtReorderPoint.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[13].Text.ToString().Replace("&nbsp;", "");
            txtRetailPrice.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[14].Text.ToString().Replace("&nbsp;", "");
            txtShortName1.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[15].Text.ToString().Replace("&nbsp;", "");
            txtShortName2.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[16].Text.Replace("&nbsp;", "");
            txtAtrbt1.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[19].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt2.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[20].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt3.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[21].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt4.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[22].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt5.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[23].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt6.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[24].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt7.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[25].Text.ToString().Replace("&quot;", "\"");           
            txtAtrbt8.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[26].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt9.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[27].Text.ToString().Replace("&nbsp;", "");
            txtAtrbt10.Text = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[28].Text.ToString().Replace("&nbsp;", "");
            ddlproductmodel.SelectedValue = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[32].Text.ToString().Replace("&nbsp;", "");
           // ddlcategory.SelectedValue = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[33].Text.ToString().Replace("&nbsp;", "");
            string groupid = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[33].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(groupid))
            {
                ddlProductGroup.SelectedValue = groupid;
            }
            else
            {
                ddlProductGroup.SelectedIndex = 0;
            }
            LoadCategory();
            string categoryid = GRVProductInfo.Rows[e.NewSelectedIndex].Cells[34].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(categoryid))
            {
                ddlcategory.SelectedValue = categoryid;
            }
            else
            {
                ddlcategory.SelectedIndex = 0;
            }
            btnSubmit.Text = "Update";
            this.chkIsEOL_SelectedIndexChanged(this, null);
            this.chkIsDetails_SelectedIndexChanged(this, null);
            ddlcategory.Focus();
        }
        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductModel();
        }
         //SqlConnection myConnection = dba.getconnection();
         //                   myConnection.Open();
         //                   SqlTransaction transaction = myConnection.BeginTransaction();
         //                   string ProductID = "";
         //                   try
         //                   {
         //                       string sqlproduct = @"update Product_Master set ModifiedBy='" + UserID + "',Barcode='" + BarCode + "',BarcodeTemp='" + BarCodeTemp + "',CategoryID='" + category + "',DealerPrice='" + DealerPrice + "',Discount='" + Discount + "',EOLDate='" + EOLDate + "',ICode='" + ImportCode + "',ImportCost='" + Impcost + "',IsActive='" + IsActive + "',IsDetail='" + Isdetails + "',IsEOL='" + IsEOL + "',ModelID='" + productmodel + "',ProdName='" + ProductName + "',ReorderPoint='" + ReorderPoint + "',RetailPrice='" + RetailPrice + "',Short1='" + ShortName1 + "',Short2='" + ShortName2 + "' where ProdID='" + HIDProdID.Value + "'" + "SELECT CAST(scope_identity() AS varchar(36))"; 

         //                        ProductID = dba.ExecuteTransactionQuery(sqlproduct, myConnection, transaction);

         //                       string resultproductdetails = "";
         //                       if (!string.IsNullOrEmpty(ProductID))
         //                       {

         //                           string sqlproductdetails = @"update Product_Details set Atrbt1='" + Atrbt1 + "',Atrbt2='" + Atrbt2 + "',Atrbt3='" + Atrbt3 + "',Atrbt4='" + Atrbt4 + "',Atrbt5='" + Atrbt5 + "',Atrbt6='" + Atrbt6 + "',Atrbt7='" + Atrbt7 + "',Atrbt8='" + Atrbt8 + "',Atrbt9='" + Atrbt9 + "',Atrbt10='" + Atrbt10 + "' where ProdDetailID='" + HIDProdDetailID.Value + "' and ProdID='" + HIDProdID.Value + "'";
         //                           resultproductdetails = dba.ExecuteTransactionQuery(sqlproductdetails, myConnection, transaction);

         //                       }
         //                       if (resultproductdetails == "1")
         //                       {
         //                           transaction.Commit();
         //                           Clear();
         //                           ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
         //                           LoadGrid();
         //                       }
         //                       else
         //                       {
         //                           transaction.Rollback();
         //                       }


         //                   }
         //                   catch (SqlException sqlError)
         //                   {
         //                       transaction.Rollback();
         //                   }
         //                   myConnection.Close();

        

    }
}