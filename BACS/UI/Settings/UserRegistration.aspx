<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="PNF.UI.Settings.UserRegistrationUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
            background-color: #4f6b72;
            font-size: 18px;
            color: Highlight;
        }
    </style>
    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">User Registration Information</div>
                    <%--  <asp:UpdatePanel runat="server" ID="upduserregister">
                        <ContentTemplate>--%>
                    <div class="alert alert-success" id="divmsg" runat="server">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>
                            <asp:Label ID="AlertWindow" CssClass="alert alert-success" runat="server"></asp:Label>!</strong>
                    </div>
                    <%--<asp:Label runat="server"  ID="lblmessage" style="text-align:center" Visible="false" ></asp:Label>--%>
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                        <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                            CssClass="StrongText"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="ddlRole" CssClass="col-md-2 control-label">User Type</asp:Label>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlRole" Enabled="True" AutoPostBack="True" CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" required>
                                </asp:DropDownList>
                            </div>
                            <div id="divpartywiseuser" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlPartyName" CssClass="col-md-2 control-label">Dealer</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlPartyName" Enabled="True" CssClass="form-control col-md-2" runat="server" required>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divManager" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlManager" CssClass="col-md-3 control-label">Manager</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlManager" Enabled="True" CssClass="form-control col-md-2" runat="server" required>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divRSMDSM" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlRSM" CssClass="col-md-2 control-label">RSM</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlRSM" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label runat="server" AssociatedControlID="ddlDSM" CssClass="col-md-2 control-label">DSM</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlDSM" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divASM" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlASM" CssClass="col-md-2 control-label">ASM</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlASM" Enabled="True" CssClass="form-control col-md-2" OnSelectedIndexChanged="ddlASM_SelectedIndexChanged" AutoPostBack="True" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divSE" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlSE" CssClass="col-md-2 control-label">SE</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlSE" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div id="divparty" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlArea" CssClass="col-md-2 control-label">Area</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlArea" Enabled="True" CssClass="form-control col-md-2" runat="server" required AutoPostBack="True" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtAreaCode" CssClass="col-md-2 control-label">Area Code</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox runat="server" ID="txtAreaCode" CssClass="form-control" ReadOnly="True" />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtPartyName" CssClass="col-md-2 control-label">Dealer Name</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox runat="server" ID="txtPartyName" CssClass="form-control" Required />
                                </div>
                                
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtUserName" CssClass="col-md-2 control-label">User Name</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" OnTextChanged="txtUserName_TextChanged" AutoPostBack="True" Required />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" OnTextChanged="txtPassword_TextChanged" AutoPostBack="true" CssClass="form-control" Required />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm Password</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" OnTextChanged="txtConfirmPassword_TextChanged" AutoPostBack="true" CssClass="form-control" Required />
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divUser">
                            <asp:Label runat="server" AssociatedControlID="txtFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{1,40}" Required />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtMiddleName" CssClass="col-md-2 control-label">Middle Name</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{2,40}" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{2,40}" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtOwnerContactPhone1" CssClass="col-md-2 control-label">Owner Phone</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtOwnerContactPhone1" pattern="[0-9]{8,11}" CssClass="form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtPhoneOffice1" CssClass="col-md-2 control-label">Office Phone</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtPhoneOffice1" pattern="[0-9]{8,11}" CssClass="form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtEmailAddress" CssClass="col-md-2 control-label">Email</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" for="email" type="email" ID="txtEmailAddress" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" id="divpartyphone" runat="server">
                            <asp:Label runat="server" AssociatedControlID="txtOwnerContactPhone2" CssClass="col-md-2 control-label">Owner 2nd phone</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtOwnerContactPhone2" pattern="[0-9]{8,11}" CssClass="form-control" />

                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtNID" CssClass="col-md-2 control-label">NID</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtNID" CssClass="form-control" />
                            </div>

                            <asp:Label runat="server" AssociatedControlID="txtOwnerName" CssClass="col-md-2 control-label">Owner Name</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtOwnerName" CssClass="form-control" />

                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPresentAddress" CssClass="col-md-2 control-label">Present Address</asp:Label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtPresentAddress" TextMode="Multiline" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{3,200}" Required />
                            </div>
                            <div id="divParmanentAddress" runat="server">
                                <asp:Label runat="server" AssociatedControlID="txtParmanentAddress" CssClass="col-md-2 control-label">Permanent Address</asp:Label>
                                <div class="col-xs-3">
                                    <asp:TextBox runat="server" ID="txtParmanentAddress" TextMode="Multiline" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{3,200}" />
                                </div>
                            </div>

                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Is Active</asp:Label>

                            <div class="col-xs-2">
                                <fieldset class="well the-fieldset">

                                    <div class="checkbox-inline">
                                        <asp:RadioButtonList ID="rdbtnIsActive" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="HIDUnique_UserId" />
                        <asp:HiddenField runat="server" ID="HIDPartyID" />

                        <div class="form-group">
                            <div class="col-md-offset-4 col-md-10">
                                <asp:Button ID="btnRegister" runat="server" Text="Submit" OnClick="btnRegister_Click" CssClass="btn btn-primary btn-lg" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btn-danger" formnovalidate />
                                
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-primary btn-lg" formnovalidate OnClick="btnExport_Click" />
                       
                            </div>

                        </div>
                    </div>
                    <%--   </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
            <legend></legend>
            <div class="form-group">

                <div class="row" runat="server">
                    <div class="col-md-3"></div>
                    <div class="col-md-2">
                        <label for="ddlSearchByUserName">Find By User Name</label>
                    </div>
                      <div class="col-md-2">
                     <asp:DropDownList ID="ddlSearchByUserName" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchByUserName_SelectedIndexChanged">
                        </asp:DropDownList>
                           </div>
                    
                    <br />
                </div>
            </div>


            <div class="form-group" id="divPartyGrid" runat="server">
                <%-- <asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    OnSelectedIndexChanging="GridView2_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                    <PagerStyle CssClass="cssPager" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:CommandField>
                        <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaCode" HeaderText="Area Code" />
                        <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="AreaName" HeaderText="Area Name" />
                        <asp:BoundField DataField="ASM" HeaderText="ASM" />
                        <asp:BoundField DataField="SE" HeaderText="SE" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="OwnerName" HeaderText="Owner Name" />
                        <%--   <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:MM/dd/yy}" />
                        <asp:BoundField DataField="MakeBy" HeaderText="Created By" />--%>
                        <asp:BoundField DataField="OwnerContactPhone1" HeaderText="OwnerContactPhone1">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OwnerContactPhone2" HeaderText="OwnerContactPhone2">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OfficePhone1" HeaderText="OfficePhone1">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NID" HeaderText="NID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleID" HeaderText="RoleID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaID" HeaderText="AreaID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SalesExecutiveID" HeaderText="SalesExecutiveID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaSalesManID" HeaderText="AreaSalesManID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Bold="True" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="form-group" id="divUserGrid" runat="server">
                <%--  <asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="GridUser" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    OnSelectedIndexChanging="GridUser_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="User_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                    <PagerStyle CssClass="cssPager" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:CommandField>
                        <asp:BoundField DataField="PartyUserID" HeaderText="PartyUserID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                        <asp:BoundField DataField="ParmanentAddress" HeaderText="Parmanent Address" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                        <asp:BoundField DataField="PhoneOffice" HeaderText="PhoneOffice">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhonePersonal">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleID" HeaderText="RoleID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Bold="True" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="form-group" id="divASMGrid" runat="server">
                <%--  <asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="GridASM" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    OnSelectedIndexChanging="GridASM_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="GridASM_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                    <PagerStyle CssClass="cssPager" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:CommandField>
                        <asp:BoundField DataField="PartyUserID" HeaderText="PartyUserID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                        <asp:BoundField DataField="ParmanentAddress" HeaderText="Parmanent Address" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="DSMName" HeaderText="DSM" />
                        <asp:BoundField DataField="RSMName" HeaderText="RSM" />
                        <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-HorizontalAlign="Center" />
                        <%--<asp:BoundField DataField="PartyName" HeaderText="Dealer" />--%>
                        <asp:BoundField DataField="PartyName" HeaderText="PartyName">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhoneOffice" HeaderText="PhoneOffice">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhonePersonal">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleID" HeaderText="RoleID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DivisionalSalesManID" HeaderText="DivisionalSalesManID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RegionalSalesManID" HeaderText="RegionalSalesManID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Bold="True" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="form-group" id="divSEGrid" runat="server">
                <%--  <asp:UpdatePanel runat="server">
                    <ContentTemplate>--%>
                <asp:GridView ID="GridSE" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    OnSelectedIndexChanging="GridSE_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="GridSE_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                    <PagerStyle CssClass="cssPager" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:CommandField>
                        <asp:BoundField DataField="PartyUserID" HeaderText="PartyUserID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="ASMName" HeaderText="ASM" />
                        <asp:BoundField DataField="Manager" HeaderText="Manager" />
                        <asp:BoundField DataField="PresentAddress" HeaderText="Present Address" />
                        <asp:BoundField DataField="ParmanentAddress" HeaderText="Parmanent Address" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />

                        <asp:BoundField DataField="IsActive" HeaderText="Active Status" ItemStyle-HorizontalAlign="Center" />
                        <%--  <asp:BoundField DataField="PartyName" HeaderText="Dealer" />   --%>
                        <asp:BoundField DataField="PartyName" HeaderText="PartyName">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhoneOffice" HeaderText="PhoneOffice">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PhonePersonal">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleID" HeaderText="RoleID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AreaSalesManID" HeaderText="AreaSalesManID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ManagerID" HeaderText="ManagerID">
                            <ItemStyle CssClass="hidden" />
                            <HeaderStyle CssClass="hidden" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Bold="True" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>

    </div>
    <script src="../assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="../assets/plugins/bootstrap/bootstrap.min.js"></script>
    <script src="../assets/plugins/metisMenu/metisMenu.min.js"></script>
    <script src="../assets/plugins/pace/pace.js"></script>
    <script src="../assets/scripts/siminta.js"></script>
</asp:Content>
