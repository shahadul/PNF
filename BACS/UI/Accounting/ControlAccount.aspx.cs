using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Accounting
{
    public partial class ControlAccount : Page
    {
        private DatabaseAccess dba = new DatabaseAccess();
        helperAcc forDdl = new helperAcc();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadGroupAccount();
                LoadAccountItems();
                LoadBalanceType();
                txtControlAccCode.Text = "[Auto Generated]";
            }

        }
        protected void LoadGroupAccount()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select GroupAccCode,GroupAccName from GroupAccount where IsActive='Y' order by GroupAccName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlGroupAcc, dt, "GroupAccCode", "GroupAccName", "Select Accounts");
            }
            else
            {

                ddlGroupAcc.Items.Clear();
                ddlGroupAcc.Items.Add(new ListItem("No Accounts Available", "0"));

            }
        }
        protected void LoadAccountItems()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select ItemCode,ItemName from AccountItem where IsActive='Y' order by ItemName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlAccountItem, dt, "ItemCode", "ItemName", "Select Item");
            }
            else
            {

                ddlAccountItem.Items.Clear();
                ddlAccountItem.Items.Add(new ListItem("No Item Available", "0"));

            }
        }
      
        protected void LoadBalanceType()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select AccBalanceID,AccBalance from AccBalanceType", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlAccBalanceType, dt, "AccBalance", "AccBalance", "Select Type");
            }
            else
            {

                ddlAccBalanceType.Items.Clear();
                ddlAccBalanceType.Items.Add(new ListItem("No Type Available", "0"));

            }
        }
        private void Clear()
        {
            ddlGroupAcc.SelectedIndex = 0;
            txtGroupAccCode.Text = string.Empty;
            txtControlAccount.Text=String.Empty;
            txtControlAccCode.Text=String.Empty;
            ddlAccountItem.SelectedIndex = 0;
            txtItemCode.Text=String.Empty;
            txtAccBalance.Text = "0";
            ddlAccBalanceType.SelectedIndex = 0;
            HIDControlAccID.Value = String.Empty;
            txtControlAccCode.Text = "[Auto Generated]";
            btnSubmit.Text = "Submit";
        }
        private void LoadGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = @"select t.ControlAccID,t.ControlAccCode,t.ControlAccount,t.GroupAccCode,ga.GroupAccName,t.AccountItem,t.AccItemCode,t.BalanceType,t.Amount from ControlAccount t
                                 left join GroupAccount ga on t.GroupAccCode=ga.GroupAccCode where t.IsActive='Y'
                                 order by t.ControlAccount";
                ds = dba.GetDataSet(query, "ConnDB230");
                gvControlAccount.DataSource = ds;
                gvControlAccount.DataBind();

            }
            catch (Exception ex)
            { throw ex; }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
          
                string CurrentDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string UserID = Session["loginMasterId"].ToString();
                string GroupAccCode = string.Format("{0:00}", txtGroupAccCode.Text);
                Int32 NextSerial = 0; 
                string AccItemCode = txtItemCode.Text;
                string AccountItem = ddlAccountItem.SelectedItem.Text;
                double Amount = Convert.ToDouble(txtAccBalance.Text);
                string BalanceType = ddlAccBalanceType.SelectedValue;
                string ControlAccount = txtControlAccount.Text;
                DataTable dt1 = dba.GetDataTable("EXEC GenerateControlAccCode '"+GroupAccCode+"'");
                string ControlAccCode = dt1.Rows[0][0].ToString();


                try
                {
                    if (btnSubmit.Text == "Submit")
                    {
                        string sqlchk = "select t.ControlAccCode from ControlAccount t where t.ControlAccount='" + ControlAccount + "' and t.IsActive='Y'";
                        DataTable dt = dba.GetDataTable(sqlchk);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save",
                                "alert('The Control Account *" + ControlAccount + "* Already Exist...');", true);
                            txtGroupAccCode.Focus();
                        }
                        else
                        {
                            string sqlCat =
                                "insert into ControlAccount (GroupAccCode,ControlAccCode,ControlAccount,AccItemCode,AccountItem,Amount,BalanceType,IsActive,AddedDate,AddedBy) values ('" +
                                GroupAccCode + "','" + ControlAccCode + "','" + ControlAccount + "','" + AccItemCode + "','" + AccountItem + "','" + Amount + "','" + BalanceType + "','Y','" + CurrentDate + "', '" + UserID + "')";
                            dba.ExecuteQuery(sqlCat, "ConnDB230");
                            NextSerial = Convert.ToInt32(ControlAccCode.Substring(2, 3)) + 1;
                            string sqlupdate = "update NextSerial set ControlAccount='" + NextSerial + "'";
                            dba.ExecuteQuery(sqlupdate, "ConnDB230");
                            LoadGrid();
                            Clear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');",
                                true);
                        }
                    }
                    if (btnSubmit.Text == "Update")
                    {
                        string sqlCat = "update ControlAccount set GroupAccCode='" + GroupAccCode + "',ControlAccount='" + ControlAccount + "',AccItemCode='" + AccItemCode + "',AccountItem='" + AccountItem + "',Amount='" + Amount + "',BalanceType='" + BalanceType + "', ModifiedBy='" + UserID + "',ModifiedDate='" + CurrentDate + "' " +
                                        "where ControlAccID='" + HIDControlAccID.Value + "'";
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


        protected void gvControlAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvControlAccount.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void gvControlAccount_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDControlAccID.Value = string.Empty;
            HIDControlAccID.Value = gvControlAccount.Rows[e.NewSelectedIndex].Cells[1].Text.Trim().Replace("&nbsp;", "");
            txtGroupAccCode.Text = gvControlAccount.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().Replace("&nbsp;", "");
            string GroupAcc = gvControlAccount.Rows[e.NewSelectedIndex].Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(GroupAcc))
            {
                ddlGroupAcc.SelectedValue = GroupAcc;
            }
            else
            {
                ddlGroupAcc.SelectedIndex = 0;
            }
            txtControlAccCode.Text = gvControlAccount.Rows[e.NewSelectedIndex].Cells[4].Text.Trim().Replace("&nbsp;", "");
            txtControlAccount.Text = gvControlAccount.Rows[e.NewSelectedIndex].Cells[5].Text.Trim().Replace("&nbsp;", "");
            txtItemCode.Text = gvControlAccount.Rows[e.NewSelectedIndex].Cells[6].Text.Trim().Replace("&nbsp;", "");
            string AccountItem = gvControlAccount.Rows[e.NewSelectedIndex].Cells[6].Text.Trim().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(AccountItem))
            {
                ddlAccountItem.SelectedValue = AccountItem;
            }
            else
            {
                ddlAccountItem.SelectedIndex = 0;
            }
            txtAccBalance.Text = gvControlAccount.Rows[e.NewSelectedIndex].Cells[8].Text.Trim().Replace("&nbsp;", "");
            string BalanceType = gvControlAccount.Rows[e.NewSelectedIndex].Cells[9].Text;
            if (!string.IsNullOrEmpty(BalanceType))
            {
                ddlAccBalanceType.SelectedValue = BalanceType;
            }
            else
            {
                ddlAccBalanceType.SelectedIndex = 0;
            }
            btnSubmit.Text = "Update";
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

        }

        protected void ddlGroupAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGroupAccCode.Text = ddlGroupAcc.SelectedValue;
        }

        protected void ddlAccountItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = ddlAccountItem.SelectedValue;
        }


    }
}