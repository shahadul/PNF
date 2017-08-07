using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class SECSetup : Page
    {
        helperAcc forDdl = new helperAcc();
        private DatabaseAccess dba = new DatabaseAccess();
        private string uid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loginMasterId"] != null)
            {
                uid = Session["loginMasterId"].ToString();
            }
            if (!IsPostBack)
            {
                GvRefresh();
                loadAccessPerson();

                chkstatus.SelectedValue = "1";
            }
            if (IsPostBack)
            {

            }
        }
        protected void loadAccessPerson()
        {
            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.* from loginMaster t,RoleMaster rm where t.RoleID=rm.RoleID and rm.RoleName='RDO' and t.IsActive='Y' order by LoginID", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlManagedByID, dt, "loginMasterId", "LoginID", "Select RDO");
                forDdl.LoadDll(ddlFindByRDO, dt, "loginMasterId", "LoginID", "Select RDO");
            }
            else
            {
                ddlManagedByID.Items.Clear();
                ddlManagedByID.Items.Add(new ListItem("No RDO Available", "0"));
                ddlFindByRDO.Items.Clear();
                ddlFindByRDO.Items.Add(new ListItem("No RDO Available", "0"));
            }
        }
        protected void clear()
        {
            txtSECEmployeeID.Text=String.Empty;
            txtSECName.Text=String.Empty;
            ddlManagedByID.SelectedIndex = 0;
            HID.Value = string.Empty;
            btnSubmit.Text = "Submit";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string SECEmployeeID = txtSECEmployeeID.Text;
            string SECName = txtSECName.Text;
            string ManagedByID = ddlManagedByID.SelectedValue;
           
            string IsActive = "";
            if (chkstatus.SelectedValue == "0")
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
                    string sqlchk = "select * from SECMaster where SECEmployeeID='" + txtSECEmployeeID.Text + "'";
                    DataTable dt = dba.GetDataTable(sqlchk);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                       "alert('The SEC EmployeeID *" + txtSECEmployeeID.Text + "* Already Exist...');", true);
                    }
                    else
                    {
                        string sqlCat = "insert into SECMaster (SECEmployeeID,SECName,ManagedByID,IsActive,AddedBy) values ('" + SECEmployeeID + "','" + SECName + "','" + ManagedByID + "', '" +
                                   IsActive +
                                   "','" + uid + "')";
                        dba.ExecuteQuery(sqlCat, "ConnDB230");
                        clear();
                        GvRefresh();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                    }

                }
                if (btnSubmit.Text == "Update")
                {
                    string sqlCat = "update SECMaster set SECName='" + SECName + "',ManagedByID='"+ManagedByID+"', IsActive='" + IsActive + "' " +
                                    "where SECMASTERID='" + HID.Value + "'";
                    dba.ExecuteQuery(sqlCat, "ConnDB230");
                    clear();
                    GvRefresh();
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
            string sqlCat = "select t.*,lm.LoginID as ManagedBy from SECMaster t,LoginMaster lm where t.ManagedByID=lm.loginMasterId order by t.SECEmployeeID";
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvSEC.DataSource = ds;
            gvSEC.DataBind();

        }

        protected void gvSEC_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSEC.PageIndex = e.NewPageIndex;
            GvRefresh();
        }

        protected void gvSEC_SelectedIndexChanged(object sender, EventArgs e)
        {
            HID.Value = string.Empty;
           
            HID.Value = gvSEC.SelectedRow.Cells[0].Text.Trim().Replace("&nbsp;", "");
            txtSECEmployeeID.Text = gvSEC.SelectedRow.Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtSECName.Text = gvSEC.SelectedRow.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string status = gvSEC.SelectedRow.Cells[4].Text.Trim().Replace("&nbsp;", "");
            if (status == "N")
            {
                chkstatus.SelectedValue = "0";
            }
            else
            {
                chkstatus.SelectedValue = "1";
            }
            string managedby = gvSEC.SelectedRow.Cells[5].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(managedby))
            {
                ddlManagedByID.SelectedValue = managedby;
            }
           
            btnSubmit.Text = "Update";
        }

        protected void gvSEC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
            }
            for (int i = 0; i <= gvSEC.Rows.Count - 1; i++)
            {
                gvSEC.Rows[i].BackColor = i % 2 != 0 ? Color.Gainsboro : Color.LightSkyBlue;
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
                                             Page.ClientScript.GetPostBackEventReference(gvSEC, "Select$" +
                                                                                         e.Row.RowIndex.ToString()));

                        break;
                }

            }
            catch (Exception ex) { }
        }

        protected void ddlFindByRDO_SelectedIndexChanged(object sender, EventArgs e)
        {   string sqlCat=String.Empty;
            if (ddlFindByRDO.SelectedIndex != 0)
            {
                sqlCat = "select t.*,lm.LoginID as ManagedBy from SECMaster t,LoginMaster lm where t.ManagedByID=lm.loginMasterId and t.ManagedByID='" + ddlFindByRDO.SelectedValue + "' order by t.SECEmployeeID"; 
            }
            else
            {
                sqlCat = "select t.*,lm.LoginID as ManagedBy from SECMaster t,LoginMaster lm where t.ManagedByID=lm.loginMasterId order by t.SECEmployeeID"; 
            }
            DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
            gvSEC.DataSource = ds;
            gvSEC.DataBind();
        }
    }
}