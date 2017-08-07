
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PNF.UI.Settings
{
    public partial class UserRegistrationUI : Page
    {
       
        DatabaseAccess dba = new DatabaseAccess();
        Validator objvalidator = new Validator();
        helperAcc forDdl = new helperAcc();
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btnExport);
            if (!IsPostBack)
            {
                Loadddl();               
                rdbtnIsActive.SelectedValue = "1";
                divpartywiseuser.Visible = false;
                divManager.Visible = false;
                divRSMDSM.Visible = false;
                divSE.Visible = false;
                divASM.Visible = false;
            }
            else
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                    
                }
                if (!(String.IsNullOrEmpty(txtConfirmPassword.Text.Trim())))
                {
                    txtConfirmPassword.Attributes["value"] = txtConfirmPassword.Text;
                }
               
            }
            divmsg.Visible = false;
            lblmessage.Visible = false;
            txtUserName.Enabled = true;
        }
        protected  void Loadddl()
        {
            LoadRole();
            LoadArea();
            LoadParty();
            LoadManager();
            LoadASM();
            LoadSE();
            LoadUserName();

        }
        protected void LoadArea()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select AreaID,AreaName,AreaCode from AreaMaster order by AreaName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlArea, dt, "AreaID", "AreaName", "Select Area");
            }
            else
            {

                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem("No Area Available", "0"));

            }
        }
        protected void LoadManager()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.ManagerID,t.FirstName + ' ' + t.MiddleName + ' ' + t.LastName as ManagerName from ManagerMaster t", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlManager, dt, "ManagerID", "ManagerName", "Select Manager");
            }
            else
            {

                ddlManager.Items.Clear();
                ddlManager.Items.Add(new ListItem("Not Available", "0"));

            }
        }
        protected void LoadRSM()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.RegionalSalesManID,t.FirstName + ' ' + t.MiddleName + ' ' + t.LastName as ManagerName from RegionalSalesManMaster t", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlRSM, dt, "RegionalSalesManID", "ManagerName", "Select RSM");
            }
            else
            {

                ddlRSM.Items.Clear();
                ddlRSM.Items.Add(new ListItem("Not Available", "0"));

            }
        }
        protected void LoadDSM()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.DivisionalSalesManID,t.FirstName + ' ' + t.MiddleName + ' ' + t.LastName as ManagerName from DivisionalSalesManMaster t", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlDSM, dt, "DivisionalSalesManID", "ManagerName", "Select DSM");
            }
            else
            {

                ddlDSM.Items.Clear();
                ddlDSM.Items.Add(new ListItem("Not Available", "0"));

            }
        }
        protected void LoadASM()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.AreaSalesManID,t.FirstName + ' ' + t.MiddleName + ' ' + t.LastName as ASMName from AreaSalesManMaster t", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlASM, dt, "AreaSalesManID", "ASMName", "Select ASM");
            }
            else
            {

                ddlASM.Items.Clear();
                ddlASM.Items.Add(new ListItem("Not Available", "0"));

            }
        }
        protected void LoadSE()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("select t.SalesExecutiveID,t.FirstName + ' ' + t.MiddleName + ' ' + t.LastName as SEName from SalesExecutiveMaster t", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlSE, dt, "SalesExecutiveID", "SEName", "Select SE");
            }
            else
            {

                ddlSE.Items.Clear();
                ddlSE.Items.Add(new ListItem("Not Available", "0"));

            }
        }
        protected void LoadUserName()
        {
            string qry=String.Empty;
            if (ddlRole.SelectedIndex != 0)
            {
                qry = @"select t.LogUserID,t.UserName from Login_Users t where t.IsActive='Y' and RoleID='"+ddlRole.SelectedValue+"' order by t.UserName";
            }
            else
            {
                qry = @"select t.LogUserID,t.UserName from Login_Users t where t.IsActive='Y' order by t.UserName";
                
            }
                
            DataTable dt;
            DataSet ds = dba.GetDataSet(qry, "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlSearchByUserName, dt, "LogUserID", "UserName", "Select User Name");
            }
            else
            {

                ddlSearchByUserName.Items.Clear();
                ddlSearchByUserName.Items.Add(new ListItem("No User Name Available", "0"));

            }
        }
        protected void LoadParty()
        {

            DataTable dt;
            DataSet ds = dba.GetDataSet("EXEC get_dealer ''", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlPartyName, dt, "PartyID", "Party", "Select Dealer");
            }
            else
            {

                ddlPartyName.Items.Clear();
                ddlPartyName.Items.Add(new ListItem("No Dealer Available", "0"));

            }
        }
        protected void LoadRole()
        {
            
            DataTable dt;
            DataSet ds = dba.GetDataSet("select RoleID,case when RoleName='Party' then 'Dealer' else RoleName end RoleName,CreatedBy,ModifiedBy,ModifiedDate,IsActive,CreatedDate,Regarks from UserRole where IsActive='Y' order by RoleName", "ConnDB230");
            if (ds != null)
            {
                dt = ds.Tables[0];
                forDdl.LoadDll(ddlRole, dt, "RoleID", "RoleName", "Select Role");
            }
            else
            {

            ddlRole.Items.Clear();
            ddlRole.Items.Add(new ListItem("No Role Available", "0"));

            }
        }
       
        private void LoadPartyGrid()
        {
            string UserName = "";
             UserName = ddlSearchByUserName.SelectedValue;
            String query = "";
            try
            {
                DataSet ds = new DataSet();
                if (!String.IsNullOrEmpty(UserName))
                {
                    query = @"select pm.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as makeby,am.AreaName,am.AreaCode,(SE.FirstName + ' ' +SE.MiddleName +' '+ SE.LastName) as SE,(asm.FirstName + ' ' +asm.MiddleName +' '+ asm.LastName) as ASM from PartyMaster pm
                        left outer join Login_Users lu on pm.PartyID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on pm.CreatedBy=logusr.LogUserID
                        LEFT OUTER JOIN AreaMaster am on pm.AreaID=am.AreaID
                        left outer join SalesExecutiveMaster SE on pm.SalesExecutiveID=SE.SalesExecutiveID
                        left outer join AreaSalesManMaster asm on pm.AreaSalesManID=asm.AreaSalesManID
                        where lu.LogUserID='" + UserName + "' order by pm.Serial";
                }
                else
                {
                    query = @"select pm.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as makeby,am.AreaName,am.AreaCode,(SE.FirstName + ' ' +SE.MiddleName +' '+ SE.LastName) as SE,(asm.FirstName + ' ' +asm.MiddleName +' '+ asm.LastName) as ASM from PartyMaster pm
                        left outer join Login_Users lu on pm.PartyID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on pm.CreatedBy=logusr.LogUserID
                        LEFT OUTER JOIN AreaMaster am on pm.AreaID=am.AreaID
                        left outer join SalesExecutiveMaster SE on pm.SalesExecutiveID=SE.SalesExecutiveID
                        left outer join AreaSalesManMaster asm on pm.AreaSalesManID=asm.AreaSalesManID
                        order by pm.Serial";
                    
                }
               
                ds = dba.GetDataSet(query, "ConnDB230");
                GridView2.DataSource = ds;
                GridView2.DataBind();
                divASMGrid.Visible = false;
                divSEGrid.Visible = false;
                divPartyGrid.Visible = true;
                divUserGrid.Visible = false;
            }
            catch (Exception ex)
            { throw ex; }

        }
        private void LoadUserGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                String query = "";
                string UserName = "";
                UserName = ddlSearchByUserName.SelectedValue;
                if (!String.IsNullOrEmpty(UserName))
                {
                    if (ddlRole.SelectedItem.Text == "Admin")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.AdminUserID as PartyUserID,'' as PartyName,'' as PartyID from AdminUsers t
                        left outer join Login_Users lu on t.AdminUserID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID                        
                        where ur.RoleName='Admin' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divUserGrid.Visible = true;
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "Manager")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.ManagerID as PartyUserID,'' as PartyName,'' as PartyID from ManagerMaster t
                        left outer join Login_Users lu on t.ManagerID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID                        
                        where ur.RoleName='Manager' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divUserGrid.Visible = true;
                    }
                    else if (ddlRole.SelectedItem.Text == "ASM")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.AreaSalesManID as PartyUserID,'' as PartyName,'' as PartyID,RSM.FirstName + ' ' + RSM.MiddleName + ' ' + RSM.LastName as RSMName,DSM.FirstName + ' ' + DSM.MiddleName + ' ' + DSM.LastName as DSMName from AreaSalesManMaster t
                        left outer join Login_Users lu on t.AreaSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID  
                        left outer join RegionalSalesManMaster RSM on t.RegionalSalesManID=RSM.RegionalSalesManID     
                        left outer join DivisionalSalesManMaster DSM on t.DivisionalSalesManID=DSM.DivisionalSalesManID                    
                        where ur.RoleName='ASM' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridASM.DataSource = ds;
                        GridASM.DataBind();
                        divASMGrid.Visible = true;
                        divSEGrid.Visible = false;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "SE")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.SalesExecutiveID as PartyUserID,'' as PartyName,'' as PartyID,asm.FirstName + ' ' + asm.MiddleName + ' ' + asm.LastName as ASMName,'' as Manager,'' as ManagerID from SalesExecutiveMaster t
                        left outer join Login_Users lu on t.SalesExecutiveID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join AreaSalesManMaster asm on t.AreaSalesManID=asm.AreaSalesManID                        
                        where ur.RoleName='SE' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if ((ddlRole.SelectedItem.Text == "DSM"))
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.DivisionalSalesManID as PartyUserID,'' as PartyName,'' as PartyID,manager.FirstName + ' ' + manager.MiddleName + ' ' + manager.LastName as Manager,'' as ASMName,'' as AreaSalesManID from DivisionalSalesManMaster t
                        left outer join Login_Users lu on t.DivisionalSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join ManagerMaster manager on t.ManagerID=manager.ManagerID                         
                        where ur.RoleName='DSM' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if ((ddlRole.SelectedItem.Text == "RSM"))
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.RegionalSalesManID as PartyUserID,'' as PartyName,'' as PartyID,manager.FirstName + ' ' + manager.MiddleName + ' ' + manager.LastName as Manager,'' as ASMName,'' as AreaSalesManID from RegionalSalesManMaster t
                        left outer join Login_Users lu on t.RegionalSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join ManagerMaster manager on t.ManagerID=manager.ManagerID                         
                        where ur.RoleName='RSM' and lu.LogUserID='" + UserName + "' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "Dealer")
                    {
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divUserGrid.Visible = false;
                        divPartyGrid.Visible = true;
                    }
                    else
                    {
                        query = @"select pu.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,pm.PartyName from PartyUsers pu
                        left outer join Login_Users lu on pu.PartyUserID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on pu.CreatedBy=logusr.LogUserID
                        left outer join PartyMaster pm on pu.PartyID=pm.PartyID
                        where ur.RoleName='" + ddlRole.SelectedItem.Text + "' and lu.LogUserID='" + UserName + "' order by pu.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = true;

                    }
                }
                else
                {
                    if (ddlRole.SelectedItem.Text == "Admin")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.AdminUserID as PartyUserID,'' as PartyName,'' as PartyID from AdminUsers t
                        left outer join Login_Users lu on t.AdminUserID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID                        
                        where ur.RoleName='Admin' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divUserGrid.Visible = true;
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "Manager")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.ManagerID as PartyUserID,'' as PartyName,'' as PartyID from ManagerMaster t
                        left outer join Login_Users lu on t.ManagerID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID                        
                        where ur.RoleName='Manager' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divUserGrid.Visible = true;
                    }
                    else if (ddlRole.SelectedItem.Text == "ASM")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.AreaSalesManID as PartyUserID,'' as PartyName,'' as PartyID,RSM.FirstName + ' ' + RSM.MiddleName + ' ' + RSM.LastName as RSMName,DSM.FirstName + ' ' + DSM.MiddleName + ' ' + DSM.LastName as DSMName from AreaSalesManMaster t
                        left outer join Login_Users lu on t.AreaSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID  
                        left outer join RegionalSalesManMaster RSM on t.RegionalSalesManID=RSM.RegionalSalesManID     
                        left outer join DivisionalSalesManMaster DSM on t.DivisionalSalesManID=DSM.DivisionalSalesManID                    
                        where ur.RoleName='ASM' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridASM.DataSource = ds;
                        GridASM.DataBind();
                        divASMGrid.Visible = true;
                        divSEGrid.Visible = false;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "SE")
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.SalesExecutiveID as PartyUserID,'' as PartyName,'' as PartyID,asm.FirstName + ' ' + asm.MiddleName + ' ' + asm.LastName as ASMName,'' as Manager,'' as ManagerID from SalesExecutiveMaster t
                        left outer join Login_Users lu on t.SalesExecutiveID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join AreaSalesManMaster asm on t.AreaSalesManID=asm.AreaSalesManID                        
                        where ur.RoleName='SE' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if ((ddlRole.SelectedItem.Text == "DSM"))
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.DivisionalSalesManID as PartyUserID,'' as PartyName,'' as PartyID,manager.FirstName + ' ' + manager.MiddleName + ' ' + manager.LastName as Manager,'' as ASMName,'' as AreaSalesManID from DivisionalSalesManMaster t
                        left outer join Login_Users lu on t.DivisionalSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join ManagerMaster manager on t.ManagerID=manager.ManagerID                         
                        where ur.RoleName='DSM' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if ((ddlRole.SelectedItem.Text == "RSM"))
                    {
                        query = @"select t.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,t.RegionalSalesManID as PartyUserID,'' as PartyName,'' as PartyID,manager.FirstName + ' ' + manager.MiddleName + ' ' + manager.LastName as Manager,'' as ASMName,'' as AreaSalesManID from RegionalSalesManMaster t
                        left outer join Login_Users lu on t.RegionalSalesManID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on t.CreatedBy=logusr.LogUserID 
                        left outer join ManagerMaster manager on t.ManagerID=manager.ManagerID                         
                        where ur.RoleName='RSM' order by t.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridSE.DataSource = ds;
                        GridSE.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = true;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = false;
                    }
                    else if (ddlRole.SelectedItem.Text == "Dealer")
                    {
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divUserGrid.Visible = false;
                        divPartyGrid.Visible = true;
                    }
                    else
                    {
                        query = @"select pu.*,lu.UserName,lu.RoleID,ur.RoleName,logusr.UserName as MakeBy,pm.PartyName from PartyUsers pu
                        left outer join Login_Users lu on pu.PartyUserID=lu.UserID
                        left outer join UserRole ur on lu.RoleID=ur.RoleID
                        left outer join Login_Users logusr on pu.CreatedBy=logusr.LogUserID
                        left outer join PartyMaster pm on pu.PartyID=pm.PartyID where ur.RoleName='"+ddlRole.SelectedItem.Text+"' order by pu.Serial desc";
                        ds = dba.GetDataSet(query, "ConnDB230");
                        GridUser.DataSource = ds;
                        GridUser.DataBind();
                        divASMGrid.Visible = false;
                        divSEGrid.Visible = false;
                        divPartyGrid.Visible = false;
                        divUserGrid.Visible = true;

                    }
                }
               
              

            }
            catch (Exception ex)
            { throw ex; }

        }
        private void Clear()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = "";
            txtAreaCode.Text = "";
            txtPartyName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtOwnerName.Text = "";
            txtOwnerContactPhone1.Text = "";
            txtOwnerContactPhone2.Text = "";
            txtPhoneOffice1.Text = "";
            txtNID.Text = "";
            txtParmanentAddress.Text = "";
            txtPresentAddress.Text = "";
            txtEmailAddress.Text = "";
            rdbtnIsActive.SelectedValue = "1";
            txtPassword.ReadOnly = false;
            txtConfirmPassword.ReadOnly = false;
            txtPartyName.ReadOnly = false;
            txtAreaCode.ReadOnly = false;
            txtPassword.Attributes["value"] = "";
            txtConfirmPassword.Attributes["value"] = "";
            btnRegister.Text = "Submit";
            lblmessage.Text = "";
            divPartyGrid.Visible = false;
            divUserGrid.Visible = false;
            ddlPartyName.Enabled = true;
            divparty.Visible = false;
            divpartyphone.Visible = false;
            divUser.Visible = false;
            divpartywiseuser.Visible = false;
            divParmanentAddress.Visible = false;
            divManager.Visible = false;
            divRSMDSM.Visible = false;
            divSE.Visible = false;
            divASM.Visible = false;
            ddlManager.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlSE.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
            divASMGrid.Visible = false;
            divPartyGrid.Visible = false;
            divSEGrid.Visible = false;
            divUserGrid.Visible = false;
        }
        private void ClearInputFields()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = "";
            txtAreaCode.Text = "";
            txtPartyName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtOwnerName.Text = "";
            txtOwnerContactPhone1.Text = "";
            txtOwnerContactPhone2.Text = "";
            txtPhoneOffice1.Text = "";
            txtNID.Text = "";
            txtParmanentAddress.Text = "";
            txtPresentAddress.Text = "";
            txtEmailAddress.Text = "";
            rdbtnIsActive.SelectedValue = "1";
            txtPassword.ReadOnly = false;
            txtConfirmPassword.ReadOnly = false;
            txtPassword.Attributes["value"] = "";
            txtConfirmPassword.Attributes["value"] = "";
            btnRegister.Text = "Submit";
            lblmessage.Text = "";
          
            ddlManager.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
            ddlSE.SelectedIndex = 0;
            ddlArea.SelectedIndex = 0;
        }
        protected void Save()
        {
             
            string IsActive = "";
            if (rdbtnIsActive.SelectedValue == "0")
            {
                IsActive = "N";
            }
            else
            {
                IsActive = "Y";
            }
           
            string UserName = txtUserName.Text.ToString();
            string Password = txtPassword.Text.ToString();
            string FirstName = txtFirstName.Text.ToString();
            string MiddleName = txtMiddleName.Text.ToString();
            string LastName = txtLastName.Text.ToString();
            string role = ddlRole.SelectedValue.ToString();
           
            string PartyName = txtPartyName.Text.ToString();
            string PartyCode = txtUserName.Text.ToString();
            string OwnerName = txtOwnerName.Text.ToString();
            string vOwnerContactPhone1 = txtOwnerContactPhone1.Text.ToString();
            string vOwnerContactPhone2 = txtOwnerContactPhone2.Text.ToString();
            string vPresentAddress = txtPresentAddress.Text.ToString();
            string vParmanentAddress = txtParmanentAddress.Text.ToString();
            string vPhoneOffice1 = txtPhoneOffice1.Text.ToString();

            string vNID = txtNID.Text.ToString();
            string vEmailAddress = txtEmailAddress.Text.ToString();
            string vUserType = ddlRole.SelectedItem.Text.ToString();
            string vUserWiseParty = ddlPartyName.SelectedValue.ToString();
            string AreaID = ddlArea.SelectedValue.ToString();
            string Manager = ddlManager.SelectedValue.ToString();
            string RSM = ddlRSM.SelectedValue.ToString();
            string DSM = ddlDSM.SelectedValue.ToString();
            string ASM = ddlASM.SelectedValue.ToString();
            string SE = ddlSE.SelectedValue.ToString();

            string UserID = Session["UserID"].ToString();
            if (((ddlRole.SelectedItem.Text == "ASM") && ((ddlDSM.SelectedIndex != 0) || (ddlRSM.SelectedIndex != 0)))||(ddlRole.SelectedItem.Text!="ASM"))
            {
                if (((ddlRole.SelectedItem.Text == "Dealer") && ((ddlSE.SelectedIndex != 0) || (ddlASM.SelectedIndex != 0))) || (ddlRole.SelectedItem.Text != "Dealer"))
                {
                    try
                    {
                        if (btnRegister.Text == "Submit")
                        {
                            if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim())
                            {
                                if (UserNameChecker(txtUserName.Text.ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('UserName already exist Please Provide Unique UserName');", true);
                                    txtUserName.Focus();
                                }
                                else if (!objvalidator.CheckCredentials(txtPassword.Text.ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Password must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.');", true);
                                    txtPassword.Focus();

                                }
                                else
                                {

                                    String query = "EXEC Insert_Users '" + UserName + "','" + Password + "','" + role + "','" + PartyName + "','" + PartyCode + "','" + FirstName + "','" + MiddleName + "','" + LastName + "','" + OwnerName + "','" + vOwnerContactPhone1 + "','" + vOwnerContactPhone2 + "','" + vPhoneOffice1 + "','" + vNID + "','" + vPresentAddress + "','" + vParmanentAddress + "','" + vEmailAddress + "','" + IsActive + "','" + UserID + "','" + vUserType + "','" + vUserWiseParty + "','" + AreaID + "','" + Manager + "','" + RSM + "','" + DSM + "','" + ASM + "','" + SE + "'";
                                    dba.ExecuteQuery(query, "ConnDB230");
                                    ClearInputFields();
                                    Setstatus();
                                    LoadUserGrid();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved...');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Password and Confirm Password Should be Same');", true);

                                txtConfirmPassword.Focus();
                            }
                        }
                        else
                        {

                            try
                            {
                                String query = "EXEC Update_Users '" + HIDPartyID.Value.ToString() + "','" + role + "','" + PartyName + "','" + PartyCode + "','" + FirstName + "','" + MiddleName + "','" + LastName + "','" + OwnerName + "','" + vOwnerContactPhone1 + "','" + vOwnerContactPhone2 + "','" + vPhoneOffice1 + "','" + vNID + "','" + vPresentAddress + "','" + vParmanentAddress + "','" + vEmailAddress + "','" + IsActive + "','" + UserID + "','" + vUserType + "','" + AreaID + "','" + Manager + "','" + RSM + "','" + DSM + "','" + ASM + "','" + SE + "','" + UserName + "'";
                                dba.ExecuteQuery(query, "ConnDB230");
                                ClearInputFields();
                                Setstatus();
                                LoadUserGrid();
                                ScriptManager.RegisterStartupScript(this, GetType(), "Update", "alert('Data Updated...');", true);
                                btnRegister.Text = "Submit";
                            }
                            catch (Exception ex)
                            {
                                throw new Exception();
                            }

                        }
                    }
                    catch { }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Update", "alert('Please Select ASM or SE and then Try Again!!');", true);
                }

            }

            else 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Update", "alert('Please Select RSM or DSM and then Try Again!!');", true);
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Save();

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            LoadPartyGrid();
        }
       
        protected void GridView2_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDPartyID.Value = GridView2.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtAreaCode.Text=GridView2.Rows[e.NewSelectedIndex].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtPartyName.Text =GridView2.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
         
            txtUserName.Text = GridView2.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
          
            string area= GridView2.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "");
            if(!string.IsNullOrEmpty(area))
            {
                ddlArea.SelectedValue = GridView2.Rows[e.NewSelectedIndex].Cells[17].Text.ToString().Replace("&nbsp;", "");
            }
            txtEmailAddress.Text =GridView2.Rows[e.NewSelectedIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
            txtPresentAddress.Text =GridView2.Rows[e.NewSelectedIndex].Cells[9].Text.ToString().Replace("&nbsp;", "");
             if (GridView2.Rows[e.NewSelectedIndex].Cells[10].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
            txtOwnerName.Text = GridView2.Rows[e.NewSelectedIndex].Cells[11].Text.ToString().Replace("&nbsp;", "");
          
            txtOwnerContactPhone1.Text = GridView2.Rows[e.NewSelectedIndex].Cells[12].Text.ToString().Replace("&nbsp;", "");
            txtOwnerContactPhone2.Text = GridView2.Rows[e.NewSelectedIndex].Cells[13].Text.ToString().Replace("&nbsp;", "");
            txtPhoneOffice1.Text = GridView2.Rows[e.NewSelectedIndex].Cells[14].Text.ToString().Replace("&nbsp;", "");
            txtNID.Text = GridView2.Rows[e.NewSelectedIndex].Cells[15].Text.ToString().Replace("&nbsp;", "");
            string SE = GridView2.Rows[e.NewSelectedIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(SE))
            {
                ddlSE.SelectedValue = GridView2.Rows[e.NewSelectedIndex].Cells[18].Text.ToString().Replace("&nbsp;", "");
            }
         
            btnRegister.Text = "Update";
            txtPassword.ReadOnly = true;
            txtConfirmPassword.ReadOnly = true;           
            lblmessage.Text = "";
           // txtUserName.Enabled = false;
            txtAreaCode.ReadOnly = false;
            txtPartyName.ReadOnly = false;
        }

        protected bool UserNameChecker(string username)
        {
            bool result = false;
            try
            {
                string sqlchk = "SELECT UserID from Login_Users WHERE UserName = '" + username + "'";
                DataTable dt = dba.GetDataTable(sqlchk);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = true;
                   // ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('User Name Already Exist...');", true);
                }

                else {

                    result = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
                result = false;
            }
            return result;
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            string username = txtUserName.Text.ToString();
            if (UserNameChecker(username))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('UserName already exist Please Provide Unique UserName.');", true);
                
                txtUserName.Focus();
            }
            else { txtPassword.Focus(); } 

        }
        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string Password = txtPassword.Text.ToString();
            try
            {
                if (!objvalidator.CheckCredentials(Password))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Password must be between 8 and 10 characters, contain at least one digit and one alphabetic character, and must not contain special characters.');", true);
                 
                    txtPassword.Focus();

                }
                else { txtConfirmPassword.Focus(); }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
            
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Password and Confirm Password Should be Same...');", true);
                txtConfirmPassword.Focus();
            }
            else
            {
                txtFirstName.Focus();
              
            }

        }
        protected void Setstatus()
        {
           // if ((ddlRole.SelectedItem.Text == "Dealer") || (ddlRole.SelectedItem.Text == "Terriotory Sales Manager"))

            if (ddlRole.SelectedItem.Text == "--Select Role--")
            {
                //Visible for Party
                divparty.Visible = false;
                divpartyphone.Visible = false;
                
                divPartyGrid.Visible = false;

                divUser.Visible = false;
                divpartywiseuser.Visible = false;
                divParmanentAddress.Visible = false;
                divUserGrid.Visible = false;
                divManager.Visible = false;
                divRSMDSM.Visible = false;
                divSE.Visible = false;
                divASM.Visible = false;
            }
            if (ddlRole.SelectedItem.Text == "Dealer")
            {
                //Visible for Party
                divparty.Visible = true;
                divpartyphone.Visible = true;
                divPartyGrid.Visible = true;
                LoadPartyGrid();               
                divUser.Visible = false;
                divpartywiseuser.Visible = false;
                divParmanentAddress.Visible = false;               
                divManager.Visible = false;
                divRSMDSM.Visible = false;
                divSE.Visible = true;
                divASM.Visible = true;
            }
            else
            {
                //Visible for General User
                divUser.Visible = true;
                divParmanentAddress.Visible = true;
                LoadUserGrid();
                //divUserGrid.Visible = true;

              

                divparty.Visible = false;
                divpartyphone.Visible = false;
                divPartyGrid.Visible = false;
                if (ddlRole.SelectedItem.Text == "User")
                {
                    LoadParty();
                    divpartywiseuser.Visible = true;
                    ddlPartyName.Enabled = true;
                    divSE.Visible = false;
                    divASM.Visible = false;
                    divManager.Visible = false;
                    divRSMDSM.Visible = false;
                }
                else if (ddlRole.SelectedItem.Text == "Admin")
                {
                    divpartywiseuser.Visible = false;
                    divManager.Visible = false;
                    divRSMDSM.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;
                   
                }
                    else if (ddlRole.SelectedItem.Text == "Manager")
                {
                    divpartywiseuser.Visible = false;
                    divManager.Visible = false;
                    divRSMDSM.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;

                }
                else if (ddlRole.SelectedItem.Text == "RSM")
                {
                    divpartywiseuser.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;
                    divManager.Visible = true;
                    divRSMDSM.Visible = false;
                    LoadManager();
                    ddlManager.SelectedIndex = 0;
                }
                else if (ddlRole.SelectedItem.Text == "DSM")
                {
                    divpartywiseuser.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;
                    divManager.Visible = true;
                    divRSMDSM.Visible = false;
                    LoadManager();
                    ddlManager.SelectedIndex = 0;
                }
                else if (ddlRole.SelectedItem.Text == "ASM")
                {
                    divpartywiseuser.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;
                    divManager.Visible = false;
                    divRSMDSM.Visible = true;
                    LoadRSM();
                    LoadDSM();
                    ddlManager.SelectedIndex = 0;
                }
                else if (ddlRole.SelectedItem.Text == "SE")
                {
                    divpartywiseuser.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = true;
                    divManager.Visible = false;
                    divRSMDSM.Visible = false;
                    LoadASM();
                   
                }
                else
                {
                    divpartywiseuser.Visible = false;
                    divManager.Visible = false;
                    divRSMDSM.Visible = false;
                    divSE.Visible = false;
                    divASM.Visible = false;

                }
            }
        }
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {   
            LoadUserName();
            ClearInputFields();
            Setstatus();
        }
       
        protected void User_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUser.PageIndex = e.NewPageIndex;
            LoadUserGrid();

        }

        protected void GridUser_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDPartyID.Value = GridUser.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtUserName.Text = GridUser.Rows[e.NewSelectedIndex].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtFirstName.Text = GridUser.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtMiddleName.Text= GridUser.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
            txtLastName.Text = GridUser.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "");
            
            txtPresentAddress.Text = GridUser.Rows[e.NewSelectedIndex].Cells[6].Text.ToString().Replace("&nbsp;", "");
            txtParmanentAddress.Text = GridUser.Rows[e.NewSelectedIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");
            txtEmailAddress.Text = GridUser.Rows[e.NewSelectedIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
            if (GridUser.Rows[e.NewSelectedIndex].Cells[9].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }
           
            txtPhoneOffice1.Text = GridUser.Rows[e.NewSelectedIndex].Cells[11].Text.ToString().Replace("&nbsp;", "");
            txtOwnerContactPhone1.Text = GridUser.Rows[e.NewSelectedIndex].Cells[12].Text.ToString().Replace("&nbsp;", "");
            string vPartyName = GridUser.Rows[e.NewSelectedIndex].Cells[14].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(vPartyName))
            {
                ddlPartyName.SelectedValue = vPartyName;
                divpartywiseuser.Visible = true;
                ddlPartyName.Enabled = false;
            }
            btnRegister.Text = "Update";
            txtPassword.ReadOnly = true;
            
            txtConfirmPassword.ReadOnly = true;
            lblmessage.Text = "";
            txtUserName.Enabled = false;
        }

        protected void GridSE_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            HIDPartyID.Value = GridSE.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtUserName.Text = GridSE.Rows[e.NewSelectedIndex].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtFirstName.Text = GridSE.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtMiddleName.Text = GridSE.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
            txtLastName.Text = GridSE.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "");

            txtPresentAddress.Text = GridSE.Rows[e.NewSelectedIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
            txtParmanentAddress.Text = GridSE.Rows[e.NewSelectedIndex].Cells[9].Text.ToString().Replace("&nbsp;", "");
            txtEmailAddress.Text = GridSE.Rows[e.NewSelectedIndex].Cells[10].Text.ToString().Replace("&nbsp;", "");
            if (GridSE.Rows[e.NewSelectedIndex].Cells[11].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }

            txtPhoneOffice1.Text = GridSE.Rows[e.NewSelectedIndex].Cells[13].Text.ToString().Replace("&nbsp;", "");
            txtOwnerContactPhone1.Text = GridSE.Rows[e.NewSelectedIndex].Cells[14].Text.ToString().Replace("&nbsp;", "");
           
            string vASM = GridSE.Rows[e.NewSelectedIndex].Cells[17].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(vASM))
            {
                ddlASM.SelectedValue = vASM;
                divpartywiseuser.Visible = false;
                ddlPartyName.Enabled = false;
            }
            string vManager = GridSE.Rows[e.NewSelectedIndex].Cells[18].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(vManager))
            {
                ddlManager.SelectedValue = vManager;
                divpartywiseuser.Visible = false;
                ddlPartyName.Enabled = false;
            }
            btnRegister.Text = "Update";
            txtPassword.ReadOnly = true;

            txtConfirmPassword.ReadOnly = true;
            lblmessage.Text = "";
            txtUserName.Enabled = false;
        }

        protected void GridSE_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSE.PageIndex = e.NewPageIndex;
            LoadUserGrid();
        }

        protected void GridASM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridASM.PageIndex = e.NewPageIndex;
            LoadUserGrid();
        }

        protected void GridASM_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            ddlManager.SelectedIndex = 0;
            HIDPartyID.Value = GridASM.Rows[e.NewSelectedIndex].Cells[1].Text.ToString();
            txtUserName.Text = GridASM.Rows[e.NewSelectedIndex].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtFirstName.Text = GridASM.Rows[e.NewSelectedIndex].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtMiddleName.Text = GridASM.Rows[e.NewSelectedIndex].Cells[4].Text.ToString().Replace("&nbsp;", "");
            txtLastName.Text = GridASM.Rows[e.NewSelectedIndex].Cells[5].Text.ToString().Replace("&nbsp;", "");

            txtPresentAddress.Text = GridASM.Rows[e.NewSelectedIndex].Cells[6].Text.ToString().Replace("&nbsp;", "");
            txtParmanentAddress.Text = GridASM.Rows[e.NewSelectedIndex].Cells[7].Text.ToString().Replace("&nbsp;", "");
            txtEmailAddress.Text = GridASM.Rows[e.NewSelectedIndex].Cells[8].Text.ToString().Replace("&nbsp;", "");
            if (GridASM.Rows[e.NewSelectedIndex].Cells[11].Text.ToString().Replace("&nbsp;", "") == "Y")
            {
                rdbtnIsActive.SelectedValue = "1";
            }
            else
            {
                rdbtnIsActive.SelectedValue = "0";
            }

            txtPhoneOffice1.Text = GridASM.Rows[e.NewSelectedIndex].Cells[13].Text.ToString().Replace("&nbsp;", "");
            txtOwnerContactPhone1.Text = GridASM.Rows[e.NewSelectedIndex].Cells[14].Text.ToString().Replace("&nbsp;", "");
          
            string vDSM = GridASM.Rows[e.NewSelectedIndex].Cells[17].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(vDSM))
            {
                ddlDSM.SelectedValue = vDSM;
                divpartywiseuser.Visible = false;
                ddlPartyName.Enabled = false;
            }
            string vRSM = GridASM.Rows[e.NewSelectedIndex].Cells[18].Text.ToString().Replace("&nbsp;", "");
            if (!string.IsNullOrEmpty(vRSM))
            {
                ddlRSM.SelectedValue = vRSM;
              
            }
            btnRegister.Text = "Update";
            txtPassword.ReadOnly = true;

            txtConfirmPassword.ReadOnly = true;
            lblmessage.Text = "";
            txtUserName.Enabled = false;
        }

        protected void ddlSearchByUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInputFields();
            Setstatus();
        }
        protected void ddlASM_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value =String.Empty;


            DataSet ds = dba.GetDataSet("select top 1 t.PartyCode,am.AreaCode from PartyMaster t,AreaMaster am where t.AreaID=am.AreaID and t.AreaID='" + ddlArea.SelectedValue + "' and t.IsActive='Y' order by t.PartyCode desc", "ConnDB230");
            if (ds != null)
            {
                value = ds.Tables[0].Rows[0][0].ToString();
                txtAreaCode.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                ds = dba.GetDataSet("select AreaCode,AreaID,AreaName from AreaMaster where AreaID='" + ddlArea.SelectedValue + "' order by AreaName", "ConnDB230");
                value = ds.Tables[0].Rows[0][0].ToString();
                txtAreaCode.Text = ds.Tables[0].Rows[0][0].ToString();
            }
           
            
              //  txtPartyCode.Text = ds.Tables[0].Rows[0][2].ToString();
                
                int hashindex = value.IndexOf('-');
                int underscoreindex = value.IndexOf('_');
                if (hashindex != -1)
                {
                    string firstpart = value.Substring(0, hashindex);
                    Int32 lastpart = Convert.ToInt32((value.Substring(hashindex + 1)));
                    if (lastpart != 0)
                    {
                        Int32 username = lastpart + 1;
                        txtUserName.Text =firstpart+'-'+Convert.ToString(username);
                    }
                }
                else if (underscoreindex!=-1)
                {
                    string firstpart = value.Substring(0, underscoreindex);
                    Int32 lastpart = Convert.ToInt32((value.Substring(underscoreindex + 1)));
                    if (lastpart != 0)
                    {
                        Int32 username = lastpart + 1;
                        txtUserName.Text = firstpart + Convert.ToString(username);
                    }
                }

          
        }

        protected DataTable BindDataTableForExport()
        {
            DataTable dt = new DataTable();
            string sqlqry = @"EXEC Get_Users '" + ddlRole.SelectedItem.Text + "'";
            dt = dba.GetDataTable(sqlqry);
            return dt;
        }
       
        private void ExporttoExcel(DataTable table)
        {
            string role=String.Empty;
            if (ddlRole.SelectedItem.Text == "--Select Role--")
            {
                role = "All User";
            }
            else
            {
                role = ddlRole.SelectedItem.Text;
            }

            if (table != null && table.Rows.Count > 0)
            {
                try
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Charset = "";
                    string FileName = "Users List-" + DateTime.Now + ".xls";
                    StringWriter strwritter = new StringWriter();
                    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                    DataGrid dgGrid = new DataGrid();
                    dgGrid.DataSource = table;
                    dgGrid.DataBind();
                    dgGrid.GridLines = GridLines.Both;
                    dgGrid.HeaderStyle.Font.Bold = true;
                    dgGrid.RenderControl(htmltextwrtter);
                    Response.Write(strwritter.ToString());
                    Response.End();
                }
                catch (Exception ex)
                {

                    ex.ToString();
                }


            }


        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            GridView dgGrid = new GridView();
            DataTable dt = new DataTable();
            dt = BindDataTableForExport();
            if (dt != null)
            {
                ExporttoExcel(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data not found');",
                          true);
            }
        }
    }
}