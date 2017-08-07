using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.RoleMgmt
{
    public partial class RolePermission : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        private helperAcc LoadDll = new helperAcc();

        private string roleId = string.Empty;
        private string userId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] != null && Session["UserID"] != null)
            {
                roleId = ddlRole.SelectedValue;
                userId = Session["UserID"].ToString();
            }
            if (!IsPostBack)
            {
                clear();
            }
        }

        protected void clear()
        {
            btnSubmit.Text = "Submit";
            HPID.Value = string.Empty;
            ddlLevel.ClearSelection();
            pnlChild.Visible = false;
            pnlParent.Visible = false;
        }

        protected void loadRole()
        {
            if (ddlLevel.SelectedIndex != 0)
            {
                string sql = "select RoleID, RoleName from UserRole where level = '" + ddlLevel.SelectedValue + "'";
                DataTable dt = dba.GetDataTable(sql);
                LoadDll.LoadDll(ddlRole, dt, "RoleID", "RoleName", "Select Role");
            }
            else
            {
                ddlRole.Items.Clear();
            }
        }

        protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadRole();
            pnlChild.Visible = false;
            pnlParent.Visible = false;
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            GvParentRefresh();
            pnlChild.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //first part
                string parentModuleId = HPID.Value;

                string sqlCheckparent = "select * from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + parentModuleId + "'";
                DataTable dtchkparent = dba.GetDataTable(sqlCheckparent);
                if (dtchkparent != null)
                {
                    SaveChildItem();
                }
                else
                {
                    string sqlinsertParent = "insert into RoleModuleMap (RoleID, ModuleID) values ('" + roleId + "', '" + parentModuleId + "')";
                    dba.GetDataTable(sqlinsertParent);
                    SaveChildItem();
                }

                //last part
                string sqlItmcheck = "select * from RoleModuleMap t1 " +
                                     "inner join UserModuleAccess t2 on t1.ModuleID=t2.ModuleID " +
                                     "where t2.ParentModuleID='" + HPPRIORITY.Value + "'";
                DataTable dtItmcheck = dba.GetDataTable(sqlItmcheck);
                if (dtItmcheck == null)
                {
                    string sqlItmcheckDel = "delete from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + parentModuleId + "'";
                    dba.ExecuteQuery(sqlItmcheckDel, "ConnDB230");
                }

                lblMsg.Text = "Successfully Submited";
                lblMsg.Attributes.Add("style", "color:Green; font-weight:bold;");
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

        protected void SaveChildItem()
        {
            foreach (GridViewRow row in gvChild.Rows)
            {
                string childModuleId = row.Cells[2].Text.ToString();
                CheckBox chkChild = row.FindControl("chkselect") as CheckBox;
                if (chkChild.Checked)
                {
                    string sqlCheckchild = "select * from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {

                    }
                    else
                    {
                        string sqlinsertchild = "insert into RoleModuleMap (RoleID, ModuleID) values ('" + roleId + "', '" + childModuleId + "')";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }

                }
                else
                {
                    string sqlCheckchild = "select * from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {
                        string sqlinsertchild = "delete from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }
                }
            }
        }

        protected void GvParentRefresh()
        {
            if (ddlRole.SelectedIndex != 0)
            {
                string sql =
                    "select t1.ModuleName, t1.ModuleID, t1.Priority from UserModuleAccess t1 " +
                    "cross join UserRole t2 where t2.level='" + ddlLevel.SelectedValue + "' " +
                    "and t2.RoleID='" + ddlRole.SelectedValue + "' and t2.ActiveStatus='Y' " +
                    "and t1.ParentModuleID = 0 order by t1.Priority asc ";


                DataTable dt = dba.GetDataTable(sql);
                if (dt != null)
                {
                    pnlParent.Visible = true;
                    gvParent.DataSource = dt;
                    gvParent.DataBind();
                }

            }
            else
            {

            }
        }
        protected void gvParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            HPID.Value = string.Empty;
            spItemName.Text = gvParent.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
            HPID.Value = gvParent.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
            string parentid = gvParent.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
            HPPRIORITY.Value = parentid;
            GvChildRefresh(parentid);
        }

        protected void GvChildRefresh(string parentid)
        {
            string sql = "select * from UserModuleAccess t1 " +
                         "cross join UserRole t2 " +
                         "where t2.RoleID = '" + ddlRole.SelectedValue + "' " +
                         "and t2.ActiveStatus='Y' and t1.ParentModuleID = '" + parentid + "' " +
                         "order by t1.Priority";
            DataTable dt = dba.GetDataTable(sql);
            if (dt != null)
            {
                pnlChild.Visible = true;
                gvChild.DataSource = dt;
                gvChild.DataBind();
            }
        }
        protected void gvChild_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
            for (int i = 0; i <= gvParent.Rows.Count - 1; i++)
            {
                gvParent.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
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
                                             Page.ClientScript.GetPostBackEventReference(gvParent, "Select$" +
                                                                                         e.Row.RowIndex.ToString()));

                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkChild = (e.Row.FindControl("chkselect") as CheckBox);
                    string childModuleId = nullChecker(e.Row.Cells[2].Text);

                    string sqlCheckchild = "select * from RoleModuleMap where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {
                        chkChild.Checked = true;
                    }

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected string nullChecker(string str)
        {
            string outut = string.Empty;
            if (str != "&nbsp;" && str != string.Empty)
            {
                outut = str;
            }
            else
            {
                outut = "0";
            }
            return outut;
        }


    }
}