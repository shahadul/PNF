<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ControlAccount.aspx.cs" Inherits="PNF.UI.Accounting.ControlAccount" %>

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
                <div class="panel panel-info" style="align-items: center;">
                    <div class="panel-heading text-left" style="font-weight: bold; font-size: medium;">Control Account Setup</div>
                    <asp:UpdatePanel runat="server" ID="upduserregister">
                        <ContentTemplate>
                            <div class="col-md-12"></div>
                            <div class="col-md-12">
                                <div class="panel-body">
                                    <fieldset>


                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="ddlGroupAcc" CssClass="col-md-2 control-label">Group Account</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="ddlGroupAcc" Enabled="True" CssClass="form-control col-md-2" Required runat="server" OnSelectedIndexChanged="ddlGroupAcc_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtGroupAccCode" CssClass="col-md-2 control-label">Group Acc Code</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtGroupAccCode" CssClass="form-control" ReadOnly="True" />
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="txtControlAccount" CssClass="col-md-2 control-label">Control Account</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtControlAccount" CssClass="form-control" Required />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtControlAccCode" CssClass="col-md-2 control-label">Control Acc Code</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtControlAccCode" CssClass="form-control" Required ReadOnly="True"/>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="ddlAccountItem" CssClass="col-md-2 control-label">Accounts Item</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="ddlAccountItem" Enabled="True" AutoPostBack="True" CssClass="form-control col-md-2" Required runat="server" OnSelectedIndexChanged="ddlAccountItem_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtItemCode" CssClass="col-md-2 control-label">Acc Item Code</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtItemCode" CssClass="form-control" ReadOnly="True" required/>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="txtAccBalance" CssClass="col-md-2 control-label">Acc Balance</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtAccBalance" CssClass="form-control" type="number" required/>
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="ddlAccBalanceType" CssClass="col-md-2 control-label">Balance Type</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="ddlAccBalanceType" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group col-xs-3">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-xs-4">
                                                <div class="col-md-offset-2 col-md-10">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btnExtra" formnovalidate />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="HIDControlAccID" />

                                    </fieldset>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group col-xs-3">
            </div>
            <div class="form-group col-xs-10">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvControlAccount" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="gvControlAccount_SelectedIndexChanged" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="gvControlAccount_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">

                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ControlAccID" HeaderText="ControlAccID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>

                                <asp:BoundField DataField="GroupAccCode" HeaderText="Code" />
                                <asp:BoundField DataField="GroupAccName" HeaderText="Group Account" />
                                <asp:BoundField DataField="ControlAccCode" HeaderText="Code" />
                                <asp:BoundField DataField="ControlAccount" HeaderText="Control Account" />

                                <asp:BoundField DataField="AccItemCode" HeaderText="Code" />
                                <asp:BoundField DataField="AccountItem" HeaderText="Account Item" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="BalanceType" HeaderText="Type" />
                               <%-- <asp:BoundField DataField="GroupAccID" HeaderText="GroupAccID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>--%>
                               <%-- <asp:BoundField DataField="ItemCode" HeaderText="ItemCode">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>--%>
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

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

