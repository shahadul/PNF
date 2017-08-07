<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="OrderApproval.aspx.cs" Inherits="PNF.UI.Selling_Process.OrderApproval" %>

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

        th, td {
            text-align: center;
        }
    </style>

    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <legend></legend>
            <asp:Panel runat="server" ID="pnlOrderDetails">
                <%--<div class="form-group col-xs-12">--%>
                <fieldset class="oderFieldSet">
                    <legend class="orderLegend">Pending Order</legend>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    OnSelectedIndexChanging="gvOrder_SelectedIndexChanging" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="gvOrder_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" />
                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total price" />
                                        <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                                        <asp:BoundField DataField="OrderID" HeaderText="OrderID">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                                            <ItemStyle CssClass="hidden" />
                                            <HeaderStyle CssClass="hidden" />
                                        </asp:BoundField>

                                    </Columns>
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <HeaderStyle BackColor="#003399" ForeColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                    <RowStyle BackColor="White" ForeColor="#003399" />
                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </fieldset>


                <fieldset id="divorderdetails" runat="server" class="oderFieldSet">
                    <legend class="orderLegend">Order Details</legend>
                    <div class="row">

                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvOrderDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                    <RowStyle CssClass="rowStyle"></RowStyle>
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtQuantity" min="1" oninput="validity.valid||(value='');" type="number" AutoPostBack="True" Text='<%# Eval("Qty") %>' OnTextChanged="txtQuantity_TextChanged" required />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" type="number" Text='<%# Eval("Price") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount in Percent(%)" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtDiscount" min="0" onkeypress="return isNumberKey(event)" type="text" AutoPostBack="True" Text='<%# Eval("Discount") %>' OnTextChanged="txtDiscount_TextChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
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
                                        <asp:BoundField DataField="Qty" HeaderText="Qty">
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

                    <div class="row">
                    </div>
                    <legend></legend>
                    <div class="form-group">
                        <asp:Label ID="lblGrandTotal" runat="server" AssociatedControlID="txtGrandTotal" CssClass="col-md-2 control-label">Grand Total</asp:Label>
                        <div class="col-xs-4">
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" Enabled="False" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Text="Approve" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" formnovalidate="" OnClick="btnCancel_Click" Text="Cancel" />
                        </div>
                    </div>
        </div>
        <br />

        </fieldset>
                <fieldset class="oderFieldSet">
                    <legend class="orderLegend">Approved Orders</legend>
                    <div class="form-group">
                        <div id="divpartywiseuser" runat="server">
                            <asp:Label runat="server" CssClass="col-md-4 control-label">Search by Dealer</asp:Label>

                            <div class="col-xs-4">
                                <%--<label for="txtDealer">Dealer</label>--%>
                                <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />

                            </div>
                            <%--   <div class="col-xs-4">
                                    <asp:DropDownList ID="ddlPartyName" Enabled="True" AutoPostBack="true" CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="grvApprovedOrder" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="grvApprovedOrder_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                        <asp:BoundField DataField="orderdate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yy}" />
                                        <asp:BoundField DataField="OrderQty" HeaderText="Order Qunatity" />
                                        <asp:BoundField DataField="ApprovedQty" HeaderText="Approved Qunatity" />
                                        <asp:BoundField DataField="orderamount" HeaderText="Total Order Amount" />
                                        <asp:BoundField DataField="ApprovedTotalAmnt" HeaderText="Total Approved Amount" />
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
                            </div>

                        </div>
                    </div>
                    <asp:Button ID="btnDealerchange" runat="server" OnClick="btnDealerchange_Click" Text="Button" CssClass="hidden" formnovalidate />
                    <asp:HiddenField runat="server" ID="HIDDealerID" />
                    <asp:HiddenField runat="server" ID="HIDDealerValue" />
                </fieldset>
        </asp:Panel>
    </div>
    </div>
   
    
    <script type="text/javascript">
        // if you use jQuery, you can load them when dom is read.
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();
        });

        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            // after update occur on UpdatePanel re-init the Autocomplete
            InitAutoCompl();

        }

        function InitAutoCompl() {
            $("[id$=txtDealer]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "OrderApproval.aspx/GetAutoCompleteData",
                        data: "{'Party':'" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('//')[0],
                                    val: item.split('//')[1]
                                }
                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (event, ui) {
                    //$('#txtAreaName').text(ui.item.val);
                    //$("#hfCustomerId").val(i.item.val);
                    $("[id$=HIDDealerID]").val(ui.item.val);
                    $("[id$=HIDDealerValue]").val(ui.item.label);

                },
                change: function (event, ui, response) {
                    $("[id$=btnDealerchange]").click();

                }
            });

        }
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


