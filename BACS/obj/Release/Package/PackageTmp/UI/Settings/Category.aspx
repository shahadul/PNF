<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="main.UI.Settings.Category" %>

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
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Category Setup</div>
                    <asp:UpdatePanel runat="server" ID="upduserregister">
                        <ContentTemplate>
                            <div class="col-md-12"></div>
                            <div class="col-md-12">
                                <div class="panel-body">
                                    <fieldset>
                                        <legend></legend>
                                        <div class="form-group col-xs-1">
                                        </div>
                                        <div class="form-group">
                                            
                                           
                                            <div class="col-xs-4">
                                                <Label for="ddlProductGroup">Product Group</Label>
                                                 <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-xs-4">
                                                <label for="txtCategory">Category</label>
                                                <asp:TextBox runat="server" ID="txtCategory" CssClass="form-control" Required />
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
                                        <asp:HiddenField runat="server" ID="HIDCategoryID" />

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
                        <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="gvCategory_SelectedIndexChanged" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="gvCategory_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">

                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="CategoryID" HeaderText="CategoryID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="ProductGroupID" HeaderText="ProductGroupID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GroupName" HeaderText="Product Group Name" />
                                 <asp:BoundField DataField="Category" HeaderText="Category Name" />


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
