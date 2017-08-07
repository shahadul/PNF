using System;
using System.Web.UI;

namespace PNF
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"]!=null)
            {
                Response.Redirect("~/UI/MP/Default.aspx");
                Response.End();
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~/login.aspx");
                Response.End();
            }
        }
    }
}