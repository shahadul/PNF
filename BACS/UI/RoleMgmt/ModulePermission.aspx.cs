
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.RoleMgmt
{
    public partial class ModulePermission : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        private helperAcc LoadDll = new helperAcc();

        private static string roleId = string.Empty;
        private string userId = string.Empty;
        private static bool vchkboxparent=false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["RoleID"] != null && Session["UserID"] != null)
            //{
            //    roleId = ddlRole.SelectedValue;
            //    userId = Session["UserID"].ToString();
            //}
            if (!IsPostBack)
            {
                LoadRole();
                clear();
            }
        }

        protected void clear()
        {
            btnSubmit.Text = "Submit";
            HIDModuleID.Value = string.Empty;
          
            pnlChild.Visible = false;
            pnlParent.Visible = false;
        }
          
        protected void LoadRole()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select * from UserRole where IsActive='Y'", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                LoadDll.LoadDll(ddlRole, dt, "RoleID", "RoleName", "Select Role");
            }
            else
            {
                ddlRole.Items.Clear();
                ddlRole.Items.Add(new ListItem("No Role Available", "0"));
            }
        }
       
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            roleId = ddlRole.SelectedValue;
            GvParentRefresh();
            pnlChild.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string parentModuleId = HIDModuleID.Value;
                SaveParentModule();
                if (vchkboxparent)
                {
                    SaveChildModule();
                    GvChildRefresh(HPPRIORITY.Value);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Updated...');", true);

                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Pease Checked Module Group first then try again');", true);

                }
               
               
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }
        protected void SaveParentModule()
        {
            foreach (GridViewRow row in gvParent.Rows)
            {
                string ParentModuleId = row.Cells[1].Text.ToString();
                CheckBox chkParent = row.FindControl("chkselect") as CheckBox;
                if (chkParent.Checked)
                {
                    string sqlCheckchild = "select * from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + ParentModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {

                    }
                    else
                    {
                        string sqlinsertchild = "insert into RoleDetail (RoleID, ModuleID) values ('" + roleId + "', '" + ParentModuleId + "')";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }

                }
                else
                {
                    string sqlCheckchild = "select * from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + ParentModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {
                        string sqlinsertchild = "delete from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + ParentModuleId + "'";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }
                }
            }
        }

        protected void SaveChildModule()
        {
            foreach (GridViewRow row in gvChild.Rows)
            {
                string childModuleId = row.Cells[2].Text.ToString();
                CheckBox chkChild = row.FindControl("chkselect") as CheckBox;
                if (chkChild.Checked)
                {
                    string sqlCheckchild = "select * from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {

                    }
                    else
                    {
                        string sqlinsertchild = "insert into RoleDetail (RoleID, ModuleID) values ('" + roleId + "', '" + childModuleId + "')";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }

                }
                else
                {
                    string sqlCheckchild = "select * from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                    DataTable dtchkchild = dba.GetDataTable(sqlCheckchild);
                    if (dtchkchild != null)
                    {
                        string sqlinsertchild = "delete from RoleDetail where RoleID='" + roleId + "'and ModuleID='" + childModuleId + "'";
                        dba.ExecuteQuery(sqlinsertchild, "ConnDB230");
                    }
                }
            }
        }
       
        protected void GvParentRefresh()
        {
            if (ddlRole.SelectedIndex != 0)
            {
                string sql = @"
                     select distinct t1.ModuleID,t1.ModuleName,t1.ParentModuleID,t1.Priority,case  when t2.status is not NULL then 'Y' else 'N' end RoleWiseModuleStatus  from 
                    (select t.ModuleID,t.ModuleName, t.Priority,t.IsActive,t.ParentModuleID,rd.RoleID
                    from ModuleMaster t                    
                    left join RoleDetail rd on t.ModuleID=rd.ModuleID
                    where t.IsActive='Y' and t.ParentModuleID=0) t1
                    left join  
                    (select t.ModuleID,t.ModuleName, t.Priority,t.IsActive,t.ParentModuleID,rd.RoleID,
                   'Y' as status   
                    from ModuleMaster t                    
                    left join RoleDetail rd on t.ModuleID=rd.ModuleID
                    where t.IsActive='Y' and t.ParentModuleID=0 and rd.RoleID='"+roleId+"') t2 on t1.ModuleID=t2.ModuleID order by t1.Priority";
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
            HIDModuleID.Value = string.Empty;
            spItemName.Text = gvParent.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
            HIDModuleID.Value = gvParent.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
            string parentid = gvParent.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
            CheckBox chkboxParent = gvParent.SelectedRow.FindControl("chkselect") as CheckBox;
            if (chkboxParent.Checked)
            { vchkboxparent = true; }
                
            else
            { vchkboxparent = false; }

            HPPRIORITY.Value = parentid;
            GvChildRefresh(parentid);
        }

        protected void GvChildRefresh(string parentid)
        {
            string sql = @"
                    select * from ModuleMaster t 
                    where t.IsActive='Y' and t.ParentModuleID = '" + parentid + "' order by t.Priority";
            DataTable dt = dba.GetDataTable(sql);
            if (dt != null)
            {
                pnlChild.Visible = true;
                gvChild.DataSource = dt;
                gvChild.DataBind();
            }
            else
            {
                gvChild.DataSource = null;
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

                CheckBox chkParent = (e.Row.FindControl("chkselect") as CheckBox);
                string status = (e.Row.Cells[5].Text);

                if (status == "Y")
                {
                    chkParent.Checked = true;
                }
                else
                {
                    chkParent.Checked = false;
                }
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

                    string sqlCheckchild = @"select * from ModuleMaster t 
                    left join RoleDetail rd on t.ModuleID=rd.ModuleID                  
                    where t.IsActive='Y' and rd.RoleID='"+roleId+"' and t.ModuleID = '" + childModuleId + "'";
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