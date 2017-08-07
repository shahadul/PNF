using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.RoleMgmt
{
    public partial class RoleSetup : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clear();
                GvRefresh();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string roleName = txtRole.Text;
                string IsActive = "";
                if (btnRdStatus.SelectedValue == "0")
                {
                    IsActive = "N";
                }
                else
                {
                    IsActive = "Y";
                }
                string remarks = txtRemarks.Text;
              
                string result = string.Empty;

                if (btnSubmit.Text == "Submit")
                {
                    string sql = "insert into UserRole(RoleName,IsActive,CreatedBy,Remarks) values('" + roleName + "', '" + IsActive + "','" + Session["UserID"].ToString() + "','" + remarks + "')";
                    result = dba.ExecuteQuery(sql, "ConnDB230");
                    if (result != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    }
                }
                if (btnSubmit.Text == "Update" && HIDRoleId.Value != string.Empty)
                {
                    string sql = "update UserRole set RoleName='" + roleName + "', IsActive='" + IsActive + "',Remarks='" + remarks + "' where RoleId='" + HIDRoleId.Value + "'";
                    result = dba.ExecuteQuery(sql, "ConnDB230");
                    if (result != "1")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);
                    }
                }
                clear();
                GvRefresh();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void clear()
        {
            btnSubmit.Text = "Submit";
            HIDRoleId.Value = string.Empty;
            txtRemarks.Text = string.Empty;
            txtRole.Text = string.Empty;
            btnRdStatus.SelectedIndex = 0;
        }
        protected void GvRefresh()
        {
            string sqlCat = "select * from UserRole";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvRole.DataSource = ds;
            gvRole.DataBind();
        }

        protected void gvRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            HIDRoleId.Value = string.Empty;
            HIDRoleId.Value = gvRole.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
            txtRole.Text = gvRole.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtRemarks.Text = gvRole.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string status = gvRole.SelectedRow.Cells[3].Text.Trim().Replace("&nbsp;", "");
            if (status == "N")
            {
                btnRdStatus.SelectedValue = "0";
            }
            else
            {
                btnRdStatus.SelectedValue = "1";
            }          
            btnSubmit.Text = "Update";
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
            for (int i = 0; i <= gvRole.Rows.Count - 1; i++)
            {
                gvRole.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
            }
            try
            {
                switch (e.Row.RowType)
                {
                    case DataControlRowType.Header:
                        //...
                        break;
                    case DataControlRowType.DataRow:

                        e.Row.Attributes.Add("onclick",
                                             Page.ClientScript.GetPostBackEventReference(gvRole, "Select$" +
                                                                                         e.Row.RowIndex.ToString()));

                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void gvRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRole.PageIndex = e.NewPageIndex;
            GvRefresh();
        }

    }
}