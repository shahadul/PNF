
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using PNF;

namespace main.UI.Settings
{
    public partial class Category : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        helperAcc forDdl = new helperAcc();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadProductGroup();
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
        private void Clear()
        {
           txtCategory.Text = string.Empty;
           HIDCategoryID.Value = string.Empty;
           btnSubmit.Text = "Submit";
           ddlProductGroup.SelectedIndex = 0;
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"Select t.*,pg.GroupName from Product_Category t
                                left join ProductGroup pg on t.ProductGroupID=pg.ProductGroupID
                                 order by Category";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvCategory.DataSource = ds;
                gvCategory.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserID = Session["UserID"].ToString();
            string category = txtCategory.Text.ToString();
            string ProductGroupID = ddlProductGroup.SelectedValue;
            string sqlchk = "select * from Category where Category='" + txtCategory.Text + "'";
            DataTable dt = dba.GetDataTable(sqlchk);
            if (dt != null && dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                    "alert('The Category *" + txtCategory.Text + "* Already Exist...');", true);
            }
            else
            {
               
                try
                {
                    if (btnSubmit.Text == "Submit")
                    {
                        string sqlCat = "insert into Product_Category (ProductGroupID,Category,AddedByFK) values ('"+ProductGroupID+"','" + category + "', '" +
                                        UserID +
                                        "')";
                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        txtCategory.Text = string.Empty;
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        string sqlCat = "update Product_Category set ProductGroupID='"+ProductGroupID+"',Category='" + category + "', LastUserFK='" + UserID + "' " +
                                        "where CategoryID='" + HIDCategoryID.Value + "'";
                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                      
                        btnSubmit.Text = "Submit";
                        LoadGrid();
                        Clear();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void GvRefresh()
        {
            string sqlCat = "Select * from Product_Category order by Category";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvCategory.DataSource = ds;
            gvCategory.DataBind();
        }
       
        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;
            GvRefresh();
        }

        protected void gvCategory_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDCategoryID.Value = string.Empty;
            HIDCategoryID.Value = gvCategory.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");

            string groupid = gvCategory.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(groupid))
            {
                ddlProductGroup.SelectedValue = groupid;
            }
            else
            {
                ddlProductGroup.SelectedIndex = 0;
            }
               
            txtCategory.Text = gvCategory.Rows[e.NewSelectedIndex].Cells[4].Text.Trim().Replace("&quot;", "\""); 
            btnSubmit.Text = "Update";
        }

      
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }

       
    }
}