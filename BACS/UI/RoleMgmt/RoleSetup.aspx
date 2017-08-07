<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="RoleSetup.aspx.cs" Inherits="PNF.UI.RoleMgmt.RoleSetup" %>

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
                        Role Setup
                    </div>
                </div>
                <div class="portlet-body">

                    <%--Here is the Content Start--%>

                    <div class="panel-body">
                        <div class="row">
                            <%--<div class="col-md-2">
                            </div>--%>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="row">
                                       <%-- <div class="col-md-3">
                                            <label for="ddlLevel">ND / RD / RT Level</label>
                                            <asp:DropDownList runat="server" ID="ddlLevel" CssClass="form-control" Required>
                                                <asp:ListItem Value="">None</asp:ListItem>
                                                <asp:ListItem Value="Admin"></asp:ListItem>
                                                <asp:ListItem Value="ND"></asp:ListItem>
                                                <asp:ListItem Value="RD"></asp:ListItem>
                                                <asp:ListItem Value="RT"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="col-md-3">
                                            <label for="txtRole">Role</label>
                                            <asp:TextBox runat="server" ID="txtRole" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-3">
                                            <label for="txtRemarks">Remarks</label>
                                            <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control remarks" TextMode="Multiline" placeholder="Remarks"/>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="btnRdStatus">Active Status</label>
                                            <asp:RadioButtonList runat="server" ID="btnRdStatus" class="form-control" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Active" Value="1" />
                                                <asp:ListItem Text="Inactive" Value="0" />
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <asp:HiddenField runat="server" ID="HIDRoleId" />
                                    <br />
                                    <div class="row">
                                        <div class="form-group col-md-1">
                                        </div>
                                        <div class="form-group col-md-10" style="float: left;">
                                            <fieldset>
                                                <legend>Role Information</legend>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvRole" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvRole_PageIndexChanging" OnRowDataBound="gvRole_RowDataBound" OnSelectedIndexChanged="gvRole_SelectedIndexChanged">
                                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="RoleID" HeaderText="RoleID">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />                                                           
                                                            <asp:BoundField DataField="IsActive" HeaderText="Active Status" />                                                           
                                                            <asp:CommandField ShowSelectButton="True" SelectText="">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <legend></legend>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
