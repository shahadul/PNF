<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="GroupAccount.aspx.cs" Inherits="PNF.UI.Accounting.GroupAccount" %>

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
                    <div class="panel-heading text-left" style="font-weight: bold; font-size: medium;">Group Account Setup</div>
                    <asp:UpdatePanel runat="server" ID="upduserregister">
                        <ContentTemplate>
                            <div class="col-md-12"></div>
                            <div class="col-md-12">
                                <div class="panel-body">
                                    <fieldset>
                                     

                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="txtGroupAccCode" CssClass="col-md-2 control-label">Acc Code</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtGroupAccCode" placeholder="Two digit Number as 01" CssClass="form-control" type="number" Required pattern="[0-9]{2}" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtGroupAccName" CssClass="col-md-2 control-label">Name</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:TextBox runat="server" ID="txtGroupAccName" CssClass="form-control" Required />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="ddlAccBalanceType" CssClass="col-md-2 control-label">Acc Balance Type</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="ddlAccBalanceType" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
                                                    <asp:ListItem Text="--Select Type--" Value="SelectType" />
                                                    <asp:ListItem Text="Debit" Value="Debit" />
                                                    <asp:ListItem Text="Credit" Value="Credit" />
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="ddlCompanyID" CssClass="col-md-2 control-label">Company</asp:Label>
                                            <div class="col-xs-4">
                                                <asp:DropDownList ID="ddlCompanyID" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
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
                                        <asp:HiddenField runat="server" ID="HIDGroupAccID" />

                                    </fieldset>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group col-xs-3">
            </div>
            <div class="form-group col-xs-6">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvGroupAccount" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="gvGroupAccount_SelectedIndexChanged" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="gvGroupAccount_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">

                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="GroupAccID" HeaderText="GroupAccID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>

                                <asp:BoundField DataField="GroupAccCode" HeaderText="Group Acc Code" />
                                <asp:BoundField DataField="GroupAccName" HeaderText="Name" />
                                <asp:BoundField DataField="AccBalanceType" HeaderText="AccBalanceType" />
                                <asp:BoundField DataField="Company" HeaderText="Company" />
                                <asp:BoundField DataField="AccBalanceType" HeaderText="AccBalanceType">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompanyID" HeaderText="CompanyID">
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

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
