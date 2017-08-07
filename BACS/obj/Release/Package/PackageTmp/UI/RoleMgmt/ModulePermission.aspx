<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ModulePermission.aspx.cs" Inherits="PNF.UI.RoleMgmt.ModulePermission" %>
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
                        Module Permission
                    </div>
                </div>
                <div class="portlet-body">

                    <%--Here is the Content Start--%>
                    <div id="hidediv" class="col-md-12" align="center">
                        <asp:Label runat="server" ID="lblMsg" BorderColor="Red"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <%--<div class="col-md-2">
                            </div>--%>
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="row">                                       
                                        <div class="col-md-3">
                                            <label for="ddlRole">Role</label>
                                            <asp:DropDownList runat="server" ID="ddlRole" CssClass="form-control" Required OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="True" />
                                        </div>
                                        <%--<div class="col-md-3">
                                            <label for="txtRemarks">Remarks</label>
                                            <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control remarks" TextMode="Multiline" placeholder="Remarks"/>
                                        </div>--%>
                                    </div>

                                    <asp:HiddenField runat="server" ID="HIDModuleID" />
                                    <asp:HiddenField runat="server" ID="HPPRIORITY" />
                                    <br />
                                    <asp:Panel runat="server" ID="pnlParent">
                                        <%--<div class="row">--%>
                                        <div class="form-group col-md-4" style="float: left;">
                                            <fieldset>
                                                <legend>Module Group List</legend>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvParent" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowDataBound="gvParent_RowDataBound" OnSelectedIndexChanged="gvParent_SelectedIndexChanged">
                                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="ModuleName" HeaderText="Group Name" />
                                                            <asp:BoundField DataField="ModuleID" HeaderText="ModuleID">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Priority" HeaderText="Priority">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:BoundField>
                                                            <asp:CommandField ShowSelectButton="True" SelectText="">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:CommandField>
                                                             <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="chkselect" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60"></HeaderStyle>
                                                            </asp:TemplateField>
                                                             <asp:BoundField DataField="RoleWiseModuleStatus" HeaderText="RoleWiseModuleStatus">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:BoundField>
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
                                        <%--</div>--%>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlChild">
                                        <%--<div class="row">--%>
                                        <div class="form-group col-md-6" style="float: left;">
                                            <fieldset>
                                                <legend>Available Module for
                                                        <asp:Label runat="server" ID="spItemName" /></legend>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvChild" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnRowDataBound="gvChild_RowDataBound" OnSelectedIndexChanged="gvChild_SelectedIndexChanged">
                                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="chkselect" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="60"></HeaderStyle>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ModuleName" HeaderText="Item Name" />
                                                            <asp:BoundField DataField="ModuleID" HeaderText="ModuleID">
                                                                <ItemStyle CssClass="hidden" />
                                                                <HeaderStyle CssClass="hidden" />
                                                            </asp:BoundField>
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
                                        <%--</div>--%>

                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
