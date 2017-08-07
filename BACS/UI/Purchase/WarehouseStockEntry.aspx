<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="WarehouseStockEntry.aspx.cs" Inherits="PNF.UI.Purchase.WarehouseStockEntry" %>

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
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Warehouse Stock Entry</div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                                    CssClass="StrongText"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtInvoiceNo" CssClass="col-md-2 control-label">Inv No/Challan No</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtInvoiceNo" class="form-control" required/>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtInvoiceDate" CssClass="col-md-2 control-label">Invoice Date</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtInvoiceDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" required />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlVendor" CssClass="col-md-2 control-label">Vendor</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlVendor" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    
                                    <asp:Label runat="server" AssociatedControlID="ddlProductGroup" CssClass="col-md-2 control-label">Product Group</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" Required runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlcategory" CssClass="col-md-2 control-label">Product Category</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlcategory" Enabled="True" AutoPostBack="true" CssClass="form-control col-md-2" Required runat="server" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlModel" CssClass="col-md-2 control-label">Model Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlModel" Enabled="True" AutoPostBack="true" CssClass="form-control col-md-2" Required runat="server" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                               
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="ddlProductName" CssClass="col-md-2 control-label">Product Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlProductName" Enabled="True" CssClass="form-control col-md-2" Required runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                      <asp:Label runat="server" AssociatedControlID="txtSL" CssClass="col-md-2 control-label">S/N</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtSL" class="form-control" />
                                    </div>
                                      <asp:Label runat="server" AssociatedControlID="txtRemarks" CssClass="col-md-2 control-label">Remarks</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtRemarks" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    
                                   
                                  <asp:Label runat="server" AssociatedControlID="txtQty" CssClass="col-md-2 control-label">Quantity</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtQty" CssClass="form-control" min="1" oninput="validity.valid||(value='');" type="number" Required pattern="[0-9]{1,10}" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" />
                                    </div>
                                     <asp:Label runat="server" AssociatedControlID="txtImportPrice" CssClass="col-md-2 control-label">Import Price</asp:Label>
                                     <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtImportPrice" CssClass="form-control" value="0" type="number" Required OnTextChanged="txtImportPrice_TextChanged" AutoPostBack="True" />
                                    </div>
                                      <asp:Label runat="server" AssociatedControlID="txtWarranty" CssClass="col-md-2 control-label">Warranty In Month</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtWarranty" CssClass="form-control" min="0" oninput="validity.valid||(value='');" type="number" value="0" pattern="[0-9]{1,10}" />
                                    </div>
                                </div>
                               
                                <div class="form-group">
                                     <asp:Label runat="server" AssociatedControlID="ddlPurchaseType" CssClass="col-md-2 control-label">Purchase Type</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlPurchaseType" Enabled="True" CssClass="form-control col-md-2" Required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                     <asp:Label runat="server" AssociatedControlID="txtTotalPrice" CssClass="col-md-2 control-label">Total</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtTotalPrice" CssClass="form-control" value="0" type="number" ReadOnly="True" />
                                    </div>
                                      <asp:Label runat="server" AssociatedControlID="txtGrossTotal" CssClass="col-md-2 control-label">Gross Total</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtGrossTotal" CssClass="form-control" value="0" type="number" ReadOnly="True" Required />
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-xs-4"></div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click" Text="Add Item" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:Panel runat="server" ID="pnlPurchaseDetails">
                    <div class="form-group col-xs-12">
                        <fieldset class="oderFieldSet">
                            <legend class="orderLegend">Purchase Details</legend>
                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPurchaseDetail" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDeleting="gvPurchaseDetail_RowDeleting" OnPageIndexChanging="gvPurchaseDetail_PageIndexChanging" OnRowCommand="gvPurchaseDetail_RowCommand">

                                            <PagerStyle CssClass="cssPager" />
                                            <Columns>
                                                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="ImportPrice" HeaderText="ImportPrice" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" />
                                                <asp:BoundField DataField="S/N" HeaderText="S/N" />
                                                 <asp:BoundField DataField="Warranty" HeaderText="Warranty" />
                                                <asp:BoundField DataField="ProductId" HeaderText="ProductId">
                                                    <ItemStyle CssClass="hidden" />
                                                    <HeaderStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgbtnDeleteOrder" runat="server" CausesValidation="false"
                                                            ImageUrl="~/images/delete.png" OnClientClick="Javascript: return confirm('Do You Want to delete the row');" CommandName="Delete"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" formnovalidate />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                    <legend></legend>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="form-group">
                        <div class="col-xs-4">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" OnClientClick="Javascript: return confirm('Are you Confirm??');" Text="Submit Order" formnovalidate />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary btn-sm" formnovalidate="" OnClick="btnClear_Click" Text="Clear" />
                        </div>
                        <br />
                    </div>

                </asp:Panel>
            </div>
            <div class="form-group">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="WarehouseStock" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="WarehouseStock_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:BoundField DataField="GroupName" HeaderText="Product Group" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Category" HeaderText="Product Category" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ProdName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="StockQty" HeaderText="StockQty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ImportPrice" HeaderText="Import Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DealerPrice" HeaderText="Dealer Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Reorder" HeaderText="Reorder" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
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
