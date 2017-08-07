<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="PNF.UI.Selling_Process.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
            font-size: 18px;
            color: black;
        }

        th, td {
            text-align: center;
        }
    </style>
    <div id="page-wrapper">

        <div class="form-horizontal" role="form">
            <br />
            <fieldset class="oderFieldSet">

                <legend class="orderLegend">Create Invoice</legend>
                <div class="row">

                    <div class="col-xs-8">
                        <asp:GridView ID="grdOrder" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="grdOrder_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="grdOrder_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" />
                                <asp:TemplateField HeaderText="Total Price" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovedTotalAmnt" runat="server" Text='<%# Eval("ApprovedTotalAmnt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="ApprovedTotalAmnt" HeaderText="Total price" />--%>
                                <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                                <asp:BoundField DataField="OrderID" HeaderText="OrderID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductGroupID" HeaderText="ProductGroupID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DueAmount" HeaderText="Due Amount" />
                            </Columns>
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
            <div id="divoerderdetails" class="form-group col-xs-12" runat="server">
                <asp:Panel runat="server" ID="pnlOrderDetails" class="panel panel-default">
                    <fieldset class="oderFieldSet">
                        <legend class="orderLegend">Invoice Details</legend>
                        <div class="row">

                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                            <asp:TemplateField HeaderText="Stock Quantity" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockQty" runat="server" Text='<%# Eval("StockQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtQuantity" min="1" oninput="validity.valid||(value='');" type="text" AutoPostBack="True" Text='<%# Eval("pending_quantity") %>' OnTextChanged="txtQuantity_TextChanged" required />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount in Percent(%)" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtDiscount" min="0" type="text" onkeypress="return isNumberKey(event)" AutoPostBack="True" Text='<%# Eval("ApprovedDiscount") %>' OnTextChanged="txtDiscount_TextChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("pending_total") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProdId" HeaderText="ProdId">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrderDetailID" HeaderText="OrderDetailID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="pending_quantity" HeaderText="pending_quantity">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>

                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>

                                </div>
                            </div>

                        </div>
                    </fieldset>
                    <div class="row">
                        <legend></legend>
                        <div class="form-group">
                            <asp:Label ID="lblGrandTotal" runat="server" AssociatedControlID="txtGrandTotal" CssClass="col-md-2 control-label">Grand Total</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtGrandTotal" CssClass="form-control" Enabled="False" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtInvoiceDate" CssClass="col-md-2 control-label">Invoice Date</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox ID="txtInvoiceDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server"
                                    Required />
                                <%--<asp:TextBox runat="server" ID="txtInvoiceDate" class="form-control" placeholder="dd/MM/yyyy" ReadOnly="True" />--%>
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtRemarks" CssClass="col-md-1 control-label">Remarks</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" type="text" />
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-xs-2">
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" OnClientClick="Javascript: return confirm('Are you Confirm??');" OnClick="btnSubmit_Click" />

                            </div>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script>
        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : evt.keyCode;

            if (charCode == 8 || charCode == 13 || charCode == 99 || charCode == 118 || charCode == 46)
            { return true; }
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            { return false; }
            return true;
        }
    </script>

</asp:Content>

