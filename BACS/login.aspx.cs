using System;
using System.Data;
using System.Globalization;
using System.Web.UI;

namespace PNF
{
    public partial class login : Page
    {
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
       
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Session["er"]!=null)
            {
                lblerror.Text = Session["er"].ToString();
            }
            if (!IsPostBack) 
            {
                Session.Abandon();
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string Lat = "";
            string Long = "";
             Lat = HIDLT.Value;
             Long = HIDLG.Value;
             string username = txtUserName.Text.ToString();
             string password = txtPassword.Text.ToString();

             if (objvalidator.CheckCredentials(username, password))
             {
                 try
                 {
                     Session["UserName"] = txtUserName.Text.ToString();
                     Session["password"] = txtPassword.Text.ToString();
                     string sqlUid = "select t.LogUserID,t.UserName,t.roleid,ur.RoleName from Login_Users t left join UserRole ur on t.RoleID=ur.RoleID where t.UserName='" + username + "'";
                     DataSet ds = dba.GetDataSet(sqlUid, "ConnDB230");
                     DataTable dt = ds.Tables[0];

                     if (dt != null)
                     {
                         Session["loginMasterId"] = dt.Rows[0][0].ToString();
                         Session["UserID"] = dt.Rows[0][1].ToString();
                         Session["RoleID"] = dt.Rows[0][2].ToString();
                         Session["RoleName"] = dt.Rows[0][3].ToString();
                         Session["TransDate"] = DateTime.ParseExact(DateTime.Today.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                         Session["Message"] = string.Empty;
                         //string sqlCat = "insert into UserLogDetail (Latitude,Longitute,RDID,IsActive,Date) values ('" + HIDLT.Value + "','" + HIDLG.Value + "','" + Session["loginMasterId"] + "','Y','" + Session["TransDate"] + "')";
                         //dba.ExecuteQuery(sqlCat, "ConnDB230");
                     }
                     Response.Redirect("~/UI/MP/Default.aspx");
                     Response.End();
                 }
                 catch (Exception ex)
                 {
                     lblerror.Text = ex.Message.ToString();
                     throw;
                 }
             }

            }

    }
}