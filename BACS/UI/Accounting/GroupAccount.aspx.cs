using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Accounting
{
    public partial class GroupAccount : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        helperAcc forDdl = new helperAcc();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadCompany();
            }

        }
        protected void LoadCompany()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.ComID,t.Company from CompanyMaster t where t.IsActive='Y' order by t.Company", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlCompanyID, dt, "ComID", "Company", "Select Company");
            }
            else
            {

                ddlCompanyID.Items.Clear();
                ddlCompanyID.Items.Add(new ListItem("No Company Available", "0"));

            }
        }
        private void Clear()
        {
            txtGroupAccCode.Text = string.Empty;
            HIDGroupAccID.Value = string.Empty;
            txtGroupAccName.Text=String.Empty;
            ddlAccBalanceType.SelectedIndex = 0;
            ddlCompanyID.SelectedIndex = 0;
            btnSubmit.Text = "Submit";
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.GroupAccID,t.GroupAccCode,t.GroupAccName,t.AccBalanceType,t.CompanyID,c.Company from GroupAccount t
                                 left join CompanyMaster c on t.CompanyID=c.ComID
                                 order by t.GroupAccName";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvGroupAccount.DataSource = ds;
                gvGroupAccount.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlAccBalanceType.SelectedValue != "SelectType")
            {
                string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string UserID = Session["loginMasterId"].ToString();
                string GroupAccCode = string.Format("{0:00}", txtGroupAccCode.Text);
                string GroupAccName = txtGroupAccName.Text;
                string AccBalanceType = ddlAccBalanceType.SelectedValue;
                string CompanyID = ddlCompanyID.SelectedValue;

              

                    try
                    {
                        if (btnSubmit.Text == "Submit")
                        {
                              string sqlchk = "select t.* from GroupAccount t where t.GroupAccCode='" + GroupAccCode + "'";
                             DataTable dt = dba.GetDataTable(sqlchk);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                                    "alert('The Group Acc Code *" + GroupAccCode + "* Already Exist...');", true);
                                txtGroupAccCode.Focus();
                            }
                            else
                            {
                                string sqlCat =
                                    "insert into GroupAccount (GroupAccCode,GroupAccName,AccBalanceType,CompanyID,IsActive,AddedDate,AddedBy) values ('" +
                                    GroupAccCode + "','" + GroupAccName + "','" + AccBalanceType + "','" + CompanyID +
                                    "','Y','" + CurrentDate + "', '" +
                                    UserID +
                                    "')";
                                dba.ExecuteQuery(sqlCat, "ConnDB230");
                                LoadGrid();
                                Clear();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');",
                                    true);
                            }
                        }
                        if (btnSubmit.Text == "Update")
                        {
                            string sqlCat = "update GroupAccount set GroupAccName='" + GroupAccName + "',AccBalanceType='" + AccBalanceType + "',CompanyID='" + CompanyID + "', ModifiedBy='" + UserID + "',ModifiedDate='" + CurrentDate + "' " +
                                            "where GroupAccID='" + HIDGroupAccID.Value + "'";
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
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Please select Acc Balance Type...');", true);
            }
           
        }

        //protected void GvRefresh()
        //{
        //    string sqlCat = "Select * from Product_Category order by Category";
        //    DataSet ds = dba.GetDataSet(sqlCat, "ConnDB230");
        //    gvGroupAccount.DataSource = ds;
        //    gvGroupAccount.DataBind();
        //}

        protected void gvGroupAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroupAccount.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvGroupAccount_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDGroupAccID.Value = string.Empty;
            HIDGroupAccID.Value = gvGroupAccount.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtGroupAccCode.Text = gvGroupAccount.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().Replace("&nbsp;", "");
            txtGroupAccName.Text = gvGroupAccount.Rows[e.NewSelectedIndex].Cells[3].Text.Trim().Replace("&nbsp;", "");
            string AccBalanceType = gvGroupAccount.Rows[e.NewSelectedIndex].Cells[6].Text.Trim().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(AccBalanceType))
            {
                ddlAccBalanceType.SelectedValue = AccBalanceType;
            }
            else
            {
                ddlAccBalanceType.SelectedIndex = 0;
            }
            string CompanyID = gvGroupAccount.Rows[e.NewSelectedIndex].Cells[7].Text.Trim().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(CompanyID))
            {
                ddlCompanyID.SelectedValue = CompanyID;
            }
            else
            {
                ddlCompanyID.SelectedIndex = 0;
            }
            btnSubmit.Text = "Update";
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }


    }
}