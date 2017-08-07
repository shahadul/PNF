
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class Division : Page
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
            txtDivision.Text = string.Empty;
            HIDDivisionID.Value = string.Empty;
            rdbtnIsActive.SelectedValue = "1";
            btnSubmit.Text = "Submit";

        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "Select * from DivisionMaster order by DivisionName";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvDivision.DataSource = ds;
                gvDivision.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserID = Session["UserID"].ToString();
            string Division = txtDivision.Text.ToString();
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
                        string sqlchk = "select * from DivisionMaster where DivisionName='" + txtDivision.Text + "'";
                        DataTable dt = dba.GetDataTable(sqlchk);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                                "alert('The Division *" + txtDivision.Text + "* Already Exist...');", true);
                        }
                        else
                        {
                            string sqlCat = "insert into DivisionMaster (DivisionName,IsActive,CreatedBy) values ('" + Division + "','" + IsActive + "', '" +
                                            UserID +
                                            "')";
                            dba.ExecuteQuery(sqlCat, "ConnDB230");
                            txtDivision.Text = string.Empty;
                            LoadGrid();
                            Clear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        string sqlCat = "update DivisionMaster set DivisionName='" + Division + "', IsActive='" + IsActive + "' " +
                                        "where DivisionID='" + HIDDivisionID.Value + "'";
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
            string sqlCat = "Select * from DivisionMaster order by DivisionName";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvDivision.DataSource = ds;
            gvDivision.DataBind();
        }

        protected void gvDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDivision.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvDivision_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDDivisionID.Value = string.Empty;
            HIDDivisionID.Value = gvDivision.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtDivision.Text = gvDivision.Rows[e.NewSelectedIndex].Cells[2].Text.Trim();
            if (gvDivision.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "") == "Y")
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