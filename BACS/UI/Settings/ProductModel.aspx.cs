
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class ProductModel : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        helperAcc LoadDll = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
         if(!IsPostBack)
         {
             //LoadCategory();
             LoadProductGroup();
            LoadGrid();
            rdbtnIsActive.SelectedValue = "1";
        }
            
        }
        private void Clear()
        {
            txtModelName.Text = string.Empty;
            txtPetName.Text = string.Empty;
            txtGenericName.Text = "";
            rdbtnIsActive.SelectedValue = "1";
            ddlcategory.SelectedIndex = 0;
            txtModelName.ReadOnly = false;
            btnSubmit.Text = "Submit";
            lblmessage.Text = "";
            HIDModelID.Value = string.Empty;
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
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.*,pc.ProductGroupID from Product_Model t 
                                left join Product_Category pc on t.CategoryID=pc.CategoryID order by Model";
                ds = dba.GetDataSet(query, "ConnDB230");
                GRVProductModel.DataSource = ds;
                GRVProductModel.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void Save()
        {
            string IsActive = "";
            string ModelName = txtModelName.Text.ToString();
            string PetName = txtPetName.Text.ToString();
            string GenericName = txtGenericName.Text.ToString();
            string category = ddlcategory.SelectedValue.ToString();
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }
            string UserID = Session["UserID"].ToString();

            try
            {
               
                    if (btnSubmit.Text == "Submit")
                    {
                        string sqlchk = "select ModelID from Product_Model where Model='" + txtModelName.Text + "'";
                        DataTable dt = dba.GetDataTable(sqlchk);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                                "alert('The Model *" + txtModelName.Text + "* Already Exist...');", true);
                        }
                        else
                        {
                            string sqlCat = "insert into Product_Model(Model,Pet_Name,Generic,AddedBy,IsActive,CategoryID) values('" + ModelName + "','" + PetName + "','" + GenericName + "','" + UserID + "','" + IsActive + "','"+category+"' )";

                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        string sqlCat = "update Product_Model set Pet_Name='" + PetName + "', Generic='" + GenericName + "', IsActive='" + IsActive + "',ModifyBy='" + UserID + "',CategoryID='"+category+"' where ModelID='" + HIDModelID.Value + "'";

                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        btnSubmit.Text = "Submit";
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);
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

        protected void GRVProductModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GRVProductModel.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void GRVProductModel_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDModelID.Value = GRVProductModel.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtModelName.Text = GRVProductModel.Rows[e.NewSelectedIndex].Cells[2].Text.ToString();
            txtPetName.Text = GRVProductModel.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtGenericName.Text = GRVProductModel.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
          
            if (GRVProductModel.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
            string groupid = GRVProductModel.Rows[e.NewSelectedIndex].Cells[6].Text.ToString().Replace("&nbsp;","");
            if (!string.IsNullOrEmpty(groupid))
            {
                ddlProductGroup.SelectedValue = groupid;
            }
            else
            {
                ddlProductGroup.SelectedIndex = 0;
            }
            LoadCategory();
            string categoryid = GRVProductModel.Rows[e.NewSelectedIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(categoryid))
            {
                ddlcategory.SelectedValue = categoryid;
            }
            else
            {
                ddlcategory.SelectedIndex = 0;
            }
            
            lblmessage.Text = "";
            btnSubmit.Text = "Update";
            txtModelName.ReadOnly = true;
        }
        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }
    }
}