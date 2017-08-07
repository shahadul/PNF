using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class ProductGroup : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rdbtnIsActive.SelectedValue = "1";
                LoadGrid();
            }

        }
       
        private void Clear()
        {
            txtProductGroup.Text = string.Empty;
            HIDProductGroupID.Value = string.Empty;
            btnSubmit.Text = "Submit";
            rdbtnIsActive.SelectedValue = "1";
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "Select * from ProductGroup order by GroupName";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvProductGroup.DataSource = ds;
                gvProductGroup.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserID = Session["UserID"].ToString();
            string ProductGroup = txtProductGroup.Text.ToString();
            string IsActive = "";
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }
         
                try
                {
                    if (btnSubmit.Text == "Submit")
                    {
                        string sqlchk = "select * from ProductGroup where GroupName='" + txtProductGroup.Text + "'";
                        DataTable dt = dba.GetDataTable(sqlchk);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                                "alert('The Product Group *" + txtProductGroup.Text + "* Already Exist...');", true);
                        }
                        else
                        {

                            string sqlCat = "insert into ProductGroup (GroupName,IsActive,CreatedBy) values ('" +
                                            ProductGroup + "','" + IsActive + "', '" +
                                            UserID +
                                            "')";
                            dba.ExecuteQuery(sqlCat, "ConnDB230");
                            txtProductGroup.Text = string.Empty;
                            LoadGrid();
                            Clear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        string sqlCat = "update ProductGroup set GroupName='" + ProductGroup + "',IsActive='" + IsActive + "', ModifiedBy='" + UserID + "' " +
                                        "where ProductGroupID='" + HIDProductGroupID.Value + "'";
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

        protected void GvRefresh()
        {
            string sqlCat = "Select * from ProductGroup order by GroupName";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvProductGroup.DataSource = ds;
            gvProductGroup.DataBind();
        }

        protected void gvProductGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductGroup.PageIndex = e.NewPageIndex;
            GvRefresh();
        }

        protected void gvProductGroup_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDProductGroupID.Value = string.Empty;
            HIDProductGroupID.Value = gvProductGroup.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtProductGroup.Text = gvProductGroup.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().Replace("&quot;", "\"");
            if (gvProductGroup.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
            btnSubmit.Text = "Update";
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }


    }
}