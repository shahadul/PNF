using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class PassWordReset : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        private string uid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"]!=null)
            {
                uid = Session["UserID"].ToString();
            }
            if (!IsPostBack)
            {
                LoadddlUserName();

            }
            else
            {
                if (!(String.IsNullOrEmpty(txtNewPassword.Text.Trim())))
                {
                    txtNewPassword.Attributes["value"] = txtNewPassword.Text;
                }
                if (!(String.IsNullOrEmpty(txtOldPassword.Text.Trim())))
                {
                    txtOldPassword.Attributes["value"] = txtOldPassword.Text;
                }
                if (!(String.IsNullOrEmpty(txtConfirmPassword.Text.Trim())))
                {
                    txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;
                }
            }
            lblmessage.Visible = false;
        }

        protected void LoadddlUserName()
        {
           
            DataTable dt;
            string query = String.Empty;
            if (Session["RoleName"].ToString() != "Admin")
            {
                query =
                    @"select distinct t.LogUserID as loginMasterId,t.UserName LoginID from Login_Users t where t.IsActive='Y' and t.UserName='" +
                    uid + "'";
                
                ddlUserName.Enabled = false;
                ddlUserName.SelectedValue = uid;
            }
            else
            {
                query = @"select distinct t.LogUserID as loginMasterId,t.UserName LoginID from Login_Users t where t.IsActive='Y'";
                ddlUserName.Enabled = true;
            }
               
                DataSet ds = dba.GetDataSet(query, "ConnDB230");
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    forDdl.LoadDll(ddlUserName, dt, "loginMasterId", "LoginID", "Select User");
                    ddlUserName.SelectedIndex = 1;
                }
                else
                {
                    ddlUserName.Items.Clear();
                    ddlUserName.Items.Add(new ListItem("No User Available", "0"));
                }
                
            }
        

        private void Clear()
        {
            ddlUserName.SelectedIndex = 0;
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtNewPassword.Attributes["value"] = "";
            txtOldPassword.Attributes["value"] = "";
            txtConfirmPassword.Attributes["value"] = "";
        }
        protected void ResetPassward_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            string vUserName = ddlUserName.SelectedItem.Text.ToString();
            string vUserID = ddlUserName.SelectedValue.Trim().ToString();
            string vOldPassword = txtOldPassword.Text.Trim().ToString();
            if (Session["UserName"] != null)
            {
                //if (Session["UserName"].ToString() != vUserName)
                //{

                //}
                //else
                //{

                if (objvalidator.CheckCredentials(vUserName, vOldPassword))
                {
                    if (!objvalidator.CheckCredentials(txtNewPassword.Text.ToString()))
                    {
                        lblmessage.Text = "Password must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.";
                        lblmessage.Visible = true;
                        lblmessage.ForeColor = Color.Red;
                        txtNewPassword.Focus();

                    }
                    else
                    {
                        try
                        {
                            String query = "EXEC Reset_Password '" + ddlUserName.SelectedValue.ToString() + "','" + txtOldPassword.Text.Trim().ToString() + "','" + txtConfirmPassword.Text.Trim().ToString() + "','" + Session["TransDate"].ToString() + "','" + Session["username"].ToString() + "'";
                            dba.ExecuteQuery(query, "ConnDB230");

                            //lblmessage.Text = "Successfully Submitted";
                            //lblmessage.ForeColor = Color.Green;
                            //lblmessage.Visible = true;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Password of user " + ddlUserName.SelectedItem.Text.ToString() + " Successfully Changed');", true);
                            Clear();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception();
                        }
                        finally
                        {

                        }

                    }

                }
                else
                {
                    lblmessage.Text = "Old Password Not Correct";
                    lblmessage.Visible = true;
                    lblmessage.ForeColor = Color.Red;
                    txtOldPassword.Focus();
                }
                //}
            }
        }
        protected void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (!objvalidator.CheckCredentials(txtNewPassword.Text.ToString()))
            {
                lblmessage.Text = "Password must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.";
                lblmessage.Visible = true;
                lblmessage.ForeColor = Color.Red;
                txtNewPassword.Focus();

            }
            else { txtConfirmPassword.Focus(); }
        }
    }
}