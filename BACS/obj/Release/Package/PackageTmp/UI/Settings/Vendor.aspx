<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="Vendor.aspx.cs" Inherits="PNF.UI.Settings.Vendor" %>

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
        /*th,td
    {
        text-align:center;
         padding-left: 4px;
            padding-right: 4px;
    }*/
    </style>
    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info" style="align-items: center;">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Vendor Setup</div>
                    <asp:UpdatePanel runat="server" ID="updVendor">
                        <ContentTemplate>
                            <div class="col-md-12"></div>
                            <div class="col-md-12">
                                <div class="panel-body">
                                    <fieldset>
                                        
                                        <%-- <div class="form-group col-xs-3">
                                        </div>--%>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="txtVendor" CssClass="col-md-2 control-label">Vendor</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtVendor" CssClass="form-control" Required ReadOnly="True" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtAddress" CssClass="col-md-2 control-label">Address</asp:Label>
                                            <div class="col-md-2">
                                                <asp:TextBox runat="server" ID="txtAddress" TextMode="Multiline" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{3,200}" Required />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtMobile" CssClass="col-md-2 control-label">Mobile</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtMobile" pattern="[0-9]{8,11}" CssClass="form-control" />
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" AssociatedControlID="txtLandline" CssClass="col-md-2 control-label">Landline</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtLandline" pattern="[0-9]{8,11}" CssClass="form-control" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtFax" CssClass="col-md-2 control-label">Fax</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtFax" CssClass="form-control" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtEmailAddress" CssClass="col-md-2 control-label">Email</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" for="email" type="email" ID="txtEmailAddress" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">

                                            <asp:Label runat="server" AssociatedControlID="txtCreditLimit" CssClass="col-md-2 control-label">Credit Limit</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtCreditLimit" CssClass="form-control" value="0" type="number" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtTin" CssClass="col-md-2 control-label">Tin</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtTin" CssClass="form-control" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtAccountNo" CssClass="col-md-2 control-label">Account No</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtAccountNo" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">


                                            <asp:Label runat="server" AssociatedControlID="txtWebsite" CssClass="col-md-2 control-label">Website</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtWebsite" CssClass="form-control" />
                                            </div>
                                            <asp:Label runat="server" AssociatedControlID="txtStartingBalance" CssClass="col-md-2 control-label">Starting Balance</asp:Label>
                                            <div class="col-xs-2">
                                                <asp:TextBox runat="server" ID="txtStartingBalance" CssClass="form-control" value="0" type="number" />
                                            </div>
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

                                        <div class="form-group col-xs-2">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-xs-4">
                                                <div class="col-md-offset-2 col-md-10">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btnExtra" formnovalidate />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField runat="server" ID="HIDVendorID" />

                                    </fieldset>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                 <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvVendor" runat="server" AutoGenerateColumns="False" CellPadding="1"
                            OnSelectedIndexChanging="gvVendor_SelectedIndexChanged" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="gvVendor_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">

                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="VendorID" HeaderText="VendorID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                <asp:BoundField DataField="Address" HeaderText="Address" />
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                  <asp:BoundField DataField="Landline" HeaderText="Landline">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="Fax" HeaderText="Fax">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                             
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="CreditLimit" HeaderText="Credit Limit" />
                                 <asp:BoundField DataField="Tin" HeaderText="Tin">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="AccountNo" HeaderText="Account No">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="Tin" HeaderText="Tin" />
                                <asp:BoundField DataField="AccountNo" HeaderText="Account No" />--%>

                                <asp:BoundField DataField="Website" HeaderText="Website" />
                                <asp:BoundField DataField="StartingBalance" HeaderText="Starting Balance" />

                                <asp:BoundField DataField="IsActive" HeaderText="Active Status" />

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
        
            <div class="form-group col-md-12">
               
            </div>
            <br/>
            
        </div>
    </div>
    
</asp:Content>

