using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace PNF.UI.MP
{
    public partial class MpAdmin : MasterPage
    {
        DatabaseAccess dbc = new DatabaseAccess();

        private string roleId = string.Empty;
        private string RoleName = string.Empty;
        private string User = string.Empty;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlNow = string.Empty;  
            try
            {
           
            if (Session["UserName"] != null)
            {
                StringBuilder sb = new StringBuilder();
                User = Session["UserName"].ToString();
                sb.Append(User);
                sb.Append("(");
                RoleName = Session["RoleName"].ToString();
                sb.Append(RoleName);
                sb.Append(")");
                lblUserName.Text = sb.ToString();
                 
                roleId = Session["RoleID"].ToString();
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
               // urlNow = Regex.Replace(HttpContext.Current.Request.Url.AbsolutePath, "/pnf/UI", "..", RegexOptions.IgnoreCase); //live
               urlNow = Regex.Replace(HttpContext.Current.Request.Url.AbsolutePath, "/uI", "..", RegexOptions.IgnoreCase);
                
                int home = urlNow.IndexOf("Default.aspx");
                if (home <1)
                {
                    string qryNow = @"select rm.RoleName,mm.ModuleName,mm.URL,mm.ParentModuleID from UserRole rm 
                    left join RoleDetail rd on rm.RoleID=rd.RoleID left join ModuleMaster mm on rd.ModuleID=mm.ModuleID
                    where rm.RoleID = '" + roleId + "' and mm.URL='"+ urlNow +"' order by mm.Priority asc";

                    DataTable dtNow = dbc.GetDataTable(qryNow);
                    if (dtNow == null)
                    {
                        Response.Redirect("~/login.aspx");
                       
                    }
                }
                ForGenerateUrl();
            }
            }
            catch (Exception ex)
            {
                Session["er"] = ex.Message+" "+urlNow;
            }
        }
       
        protected void btnlnkLogout_OnClick(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/login.aspx");
        }

        // Dynamic Menu

        protected void ForGenerateUrl()
        {
            string qry = @"select rm.RoleName,mm.Priority,mm.ModuleName,mm.URL,mm.ParentModuleID from UserRole rm 
                    left join RoleDetail rd on rm.RoleID=rd.RoleID left join ModuleMaster mm on rd.ModuleID=mm.ModuleID
                    where rm.RoleID = '" + roleId + "' and rm.IsActive='Y' and mm.IsActive='Y' order by mm.Priority asc";
                //"SELECT tur.RoleName, tuma.Priority, tuma.ModuleName, tuma.URL, " +
                //"tuma.ParentModuleID FROM UserRole tur " +
                //"left join RoleModuleMap trm on tur.RoleID=trm.RoleID " +
                //"left join UserModuleAccess tuma on trm.ModuleID=tuma.ModuleID " +
                //"where tur.RoleID = '" + roleId + "' and tuma.ActiveStatus='Y' and tur.ActiveStatus='Y' " +
                //"order by tuma.Priority asc";
            DataSet ds = new DataSet();
            ds = dbc.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                DataTable table = ds.Tables[0];
                DataRow[] parentMenus = table.Select("ParentModuleID = 0");

                var sb = new StringBuilder();
                string unorderedList = GenerateUL(parentMenus, table, sb);
                menu.InnerHtml = unorderedList;
            }
        }

        protected string MenuIconConditional(string menuHeading)
        {
           // string menuicon = "";
            string menuicon = "fa fa-bars";
            if (menuHeading != string.Empty)
            {
                if (menuHeading == "Role Management")
                {
                    menuicon = "fa fa-check";
                }
                if (menuHeading == "User Management")
                {
                    menuicon = "fa fa-users";
                }
                if (menuHeading == "Distributor Registration")
                {
                    menuicon = "fa fa-random";
                }
                if (menuHeading == "Sales Officer")
                {
                    menuicon = "fa fa-road";
                }
                if (menuHeading == "Stock Management")
                {
                    menuicon = "fa fa-bar-chart-o";
                }
                if (menuHeading == "Promotion")
                {
                    menuicon = "fa fa-plus-circle";
                }
                if (menuHeading == "Sales Procedure")
                {
                    menuicon = "fa fa-bolt";
                }
                if (menuHeading == "Reports")
                {
                    menuicon = "fa fa-building";
                }
            }

            return menuicon;
        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            try
            {
                //top
                
                string tp1 = "page-container";
                string top1 = String.Format(@"<div class=""{0}"">", tp1);

                string u1 = "page-sidebar-menu";
                string uu1 = String.Format(@"<ul class=""{0}"">", u1);
                sb.Append(uu1);

                string u2 = "sidebar-toggler-wrapper";
                string uu2 = String.Format(@"<li class=""{0}"">", u2);
                sb.Append(uu2);

               // string u3 = "";
                string u3 = "sidebar-toggler";
                string uu3 = String.Format(@"<div class=""{0}""></div>", u3);
                sb.Append(uu3);

                string u4 = "clearfix";
                string uu4 = String.Format(@"<div class=""{0}""></div>", u4);
                sb.Append(uu4);
                sb.Append("</li>");

                string u5 = "";
                string uu5 = String.Format(@"<li class=""{0}"">", u5);
                sb.Append(uu5);

                string u6 = "../MP/Default.aspx";
                string uu6 = String.Format(@"<a href=""{0}"">", u6);
                sb.Append(uu6);

                string u7 = "fa fa-home";
                string uu7 = String.Format(@"<i class=""{0}""></i>", u7);
                sb.Append(uu7);

                string u8 = "title";
                string uu8 = String.Format(@"<span class=""{0}"">Home</span>", u8);
                sb.Append(uu8);
                sb.Append("</a></li>");
                //before 

                //common
                string liClass = "";
                string liClassapned = String.Format(@"<li class=""{0}"">", liClass);

                //sb.AppendLine(liClassapned);

                if (menu.Length > 0)
                {
                    foreach (DataRow dr in menu)
                    {
                        string handler = dr["URL"].ToString();
                        string menuText = dr["ModuleName"].ToString();
                        string pid = dr["Priority"].ToString();
                        string parentId = dr["ParentModuleID"].ToString();


                        //for menu title start

                        string menhref = "javascript:;";
                        string liClassmenhref = String.Format(@"<a href=""{0}"">", menhref);

                        // MENU LABEL ICON CONDITIONALLY
                        //string menhreficon = "fa fa-bars";
                        string menhreficon = MenuIconConditional(menuText);

                        string liClassmenhreficon = String.Format(@"<i class=""{0}""></i>", menhreficon);

                      //  string menhreficonspn2 = "menu_img";
                        string menhreficonspn2 = "arrow";
                        string liClassmenhreficonspn2 = String.Format(@"<span class=""{0}""></span>", menhreficonspn2);

                        //for menu title end

                        DataRow[] subMenu = table.Select(String.Format("ParentModuleID = '" + pid + "'"));
                        //foreach (DataRow subMenu in table.Select("ParentModuleID='" + Convert.ToInt64(pid) + "'"))
                        //{

                        //}
                        if (subMenu.Length > 0 && !pid.Equals(parentId))
                        {
                            sb.Append(liClassapned);

                            //string line = String.Format(@"<a data-toggle=""{2}"" href=""{0}"">{1}{3}</a>", handler, menuText, liClass, bClassapned);
                            //sb.Append(line);

                            sb.Append(liClassmenhref);
                            sb.Append(liClassmenhreficon);
                            string liClassmenhreficonspn1 = String.Format(@"<span class=""{0}"">{1}</span>", "title", menuText);
                            sb.Append(liClassmenhreficonspn1);
                            sb.Append(liClassmenhreficonspn2);
                            sb.Append("</a>");
                            var subMenuBuilder = new StringBuilder();
                            sb.Append(GenerateULsubmenu(subMenu, table, subMenuBuilder));

                            sb.Append("</li>");
                        }
                        else
                        {
                            sb.Append(liClassmenhref);
                            sb.Append(liClassmenhreficon);
                            string liClassmenhreficonspn1 = String.Format(@"<span class=""{0}"">{1}</span>", handler, menuText);
                            sb.Append(liClassmenhreficonspn1);
                            sb.Append(liClassmenhreficonspn2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('" + ex + "');", true);
                Session["er"] = ex.Message;
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        private string GenerateULsubmenu(DataRow[] submenu, DataTable table, StringBuilder ssb)
        {
            // submenu start
            string ultp = "sub-menu";
            string ultpstyle = "display: none;";
            string sbultp = String.Format(@"<ul class=""{0}"" style=""{1}"">", ultp, ultpstyle);

            string lisbicon = "fa fa-chevron-circle-right";
            string sblisbicon = String.Format(@"<i class=""{0}""></i>", lisbicon);

            // submenu start end

            ssb.AppendLine(sbultp);

            foreach (DataRow dr in submenu)
            {
                string handler = dr["URL"].ToString();
                string menuText = dr["ModuleName"].ToString();
                if (menuText != "Hide")
                {
                    ssb.Append("<li>");
                    string line = String.Format(@"<a href=""{0}"">{1}{2}</a>", handler, sblisbicon, menuText);
                    ssb.Append(line);
                    ssb.Append("</li>");
                }
              
            }

            ssb.Append("</ul>");
            return ssb.ToString();
        }
    }
}