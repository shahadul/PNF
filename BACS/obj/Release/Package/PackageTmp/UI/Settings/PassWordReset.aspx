<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="PassWordReset.aspx.cs" Inherits="PNF.UI.Settings.PassWordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%------------------------Here is the Content Start-------------------------------------%>
    <!-- BEGIN PAGE CONTENT-->
    <div class="row">
        <div class="col-md-12">
            <%--<div class="portlet blue-hoki box">--%>
            <div class="portlet color-blue">
                <div class="portlet-title">
                    <div class="caption">
                        <%--<i class="fa fa-paper-plane"></i>--%>
                        <i class="fa fa-database"></i>
                        Reset Your Password
                    </div>
                </div>
                <div class="portlet-body">

                    <%--Here is the Content Start--%>

                    <div class="panel-body">
                        <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;"></div>
                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="row">
                            <%-- <asp:Label runat="server" AssociatedControlID="txtUserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Required/>
               
            </div>--%>
                            <asp:Label runat="server" AssociatedControlID="ddlUserName" CssClass="col-md-2 control-label">User Name</asp:Label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlUserName" Enabled="false" AutoPostBack="True" CssClass="form-control col-md-2" data-style="btn-primary" runat="server">
                                </asp:DropDownList>
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtOldPassword" CssClass="col-md-2 control-label">Old Password</asp:Label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password" CssClass="form-control" pattern="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" Required />

                            </div>

                        </div>

                        <div class="row">
                            <asp:Label runat="server" AssociatedControlID="txtNewPassword" CssClass="col-md-2 control-label">New Password</asp:Label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" TextMode="Password" OnTextChanged="txtNewPassword_TextChanged" AutoPostBack="true" Required />

                            </div>

                            <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm New password</asp:Label>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" pattern="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$" Required />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button ID="ResetPassward" runat="server" OnClick="ResetPassward_Click" Text="Reset Password" CssClass="btn btn-primary btn-sm" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
