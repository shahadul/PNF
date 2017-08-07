<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="SECSetup.aspx.cs" Inherits="PNF.UI.Settings.SECSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <%------------------------Here is the Content Start-------------------------------------%>
    <!-- BEGIN PAGE CONTENT-->
    <style type="text/css">
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
            background-color: #4f6b72;
            font-size: 18px;
            color: Highlight;
        }
    </style>
    <div class="row">
        <div class="col-md-12">
            <%--<div class="portlet blue-hoki box">--%>
            <div class="portlet color-blue">
                <div class="portlet-title">
                    <div class="caption">
                        <%--<i class="fa fa-paper-plane"></i>--%>
                        <i class="fa fa-database"></i>
                        SEC Setup
                    </div>
                </div>
                <div class="portlet-body">

                    <%--Here is the Content Start--%>

                    <div class="panel-body">
                        <div class="col-md-12">
                         <div class="row">
                                      <asp:Label runat="server" AssociatedControlID="txtSECEmployeeID" CssClass="col-md-2 control-label">SEC EmployeeID</asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtSECEmployeeID" Enabled="True" CssClass="form-control col-md-2" data-style="btn-primary" runat="server" required>
                                        </asp:TextBox>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtSECName" CssClass="col-md-2 control-label">SEC Name</asp:Label>
                                    <div class="col-md-3">
                                        <asp:textbox ID="txtSECName" Enabled="True" CssClass="form-control col-md-2" data-style="btn-primary" runat="server" required>
                                        </asp:textbox>
                                    </div>
                                </div>
                           
                             <div class="row">
                                 <asp:Label runat="server" AssociatedControlID="ddlManagedByID" CssClass="col-md-2 control-label">Assigned RDO</asp:Label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlManagedByID" Enabled="True" CssClass="form-control col-md-2" data-style="btn-primary" runat="server" required>
                                    </asp:DropDownList>
                                </div>
                                  <asp:Label runat="server" CssClass="col-md-2 control-label"></asp:Label>
                                  <div class="col-md-3">
                                    <fieldset class="well the-fieldset">
                                        <legend class="the-legend">Status</legend>
                                        <div class="checkbox-inline">
                                            <asp:RadioButtonList ID="chkstatus" runat="server" RepeatDirection="Vertical">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                         
                            <div class="row">
                               
                                <div class="col-md-2">
                                    
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClick="btnSubmit_Click" />
                                </div>
                            </div>

                            <asp:HiddenField runat="server" ID="HID" />
                            <br />
                               <div class="row">
                                   <div class="col-md-4">
                                    
                                </div>
                                
                                <div class="col-md-3">
                                     <label for="ddlFindByRDO">Find By Assigned RDO</label>
                                    <%-- <asp:Label runat="server" AssociatedControlID="ddlFindByRDO" CssClass="col-md-2 control-label">Find By Assigned RDO</asp:Label>--%>
                                    <asp:DropDownList ID="ddlFindByRDO" Enabled="True" CssClass="form-control col-md-2" data-style="btn-primary" runat="server" required AutoPostBack="True" OnSelectedIndexChanged="ddlFindByRDO_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-2">
                                </div>
                                <div class="form-group col-md-6" style="float: left;">
                                    <fieldset>
                                        <legend>SEC Information</legend>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvSEC" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="8" OnPageIndexChanging="gvSEC_PageIndexChanging" OnRowDataBound="gvSEC_RowDataBound" OnSelectedIndexChanged="gvSEC_SelectedIndexChanged">
                                                <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                                <RowStyle CssClass="rowStyle"></RowStyle>
                                                <PagerStyle CssClass="cssPager" />
                                                <Columns>
                                                    <asp:BoundField DataField="SECMASTERID" HeaderText="SECMASTERID">
                                                        <ItemStyle CssClass="hidden" />
                                                        <HeaderStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SECEmployeeID" HeaderText="SEC EmployeeID" />
                                                    <asp:BoundField DataField="SECName" HeaderText="SEC Name" />
                                                    <asp:BoundField DataField="ManagedBy" HeaderText="Assigned RDO" />
                                                    <asp:BoundField DataField="IsActive" HeaderText="Is Active" />
                                                    <asp:BoundField DataField="ManagedByID" HeaderText="ManagedByID">
                                                        <ItemStyle CssClass="hidden" />
                                                        <HeaderStyle CssClass="hidden" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ShowSelectButton="True" SelectText="" />
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

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
