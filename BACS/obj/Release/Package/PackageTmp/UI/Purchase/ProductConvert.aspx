<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ProductConvert.aspx.cs" Inherits="PNF.UI.Purchase.ProductConvert" %>

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
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Product Convertion</div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                                    CssClass="StrongText"></asp:Label>
                            </div>
                            <div class="panel-body">


                                <div class="form-group">

                                    <asp:Label runat="server" AssociatedControlID="txtFromProductName" CssClass="col-md-2 control-label">Product(From)</asp:Label>
                                    <div class="col-xs-4">

                                        <asp:TextBox type="text" ID="txtFromProductName" runat="server" class="form-control" />

                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtToProductName" CssClass="col-md-2 control-label">Product(To)</asp:Label>
                                    <div class="col-xs-4">

                                        <asp:TextBox type="text" ID="txtToProductName" runat="server" class="form-control" />

                                    </div>


                                </div>
                                <div class="form-group">


                                    <asp:Label runat="server" AssociatedControlID="txtQty" CssClass="col-md-2 control-label">Quantity</asp:Label>
                                    <div class="col-xs-4">
                                        <asp:TextBox runat="server" ID="txtQty" CssClass="form-control" min="1" oninput="validity.valid||(value='');" type="number" Required pattern="[0-9]{1,10}" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" />
                                    </div>

                                    <asp:Label runat="server" AssociatedControlID="txtRemarks" CssClass="col-md-2 control-label">Remarks</asp:Label>
                                    <div class="col-xs-4">
                                        <asp:TextBox runat="server" ID="txtRemarks" class="form-control" />
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-xs-2"></div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnAdd_Click" Text="Add Item" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:Panel runat="server" ID="pnlPurchaseDetails">
                    <div class="form-group col-xs-12">
                        <fieldset class="oderFieldSet">
                            <legend class="orderLegend">Convert Details</legend>
                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvPurchaseDetail" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDeleting="gvPurchaseDetail_RowDeleting" OnPageIndexChanging="gvPurchaseDetail_PageIndexChanging">

                                            <PagerStyle CssClass="cssPager" />
                                            <Columns>
                                                <asp:BoundField DataField="FromProduct" HeaderText="Product(From)" />
                                                <asp:BoundField DataField="ToProduct" HeaderText="Product(To)" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="FromProductId" HeaderText="ProductId">
                                                    <ItemStyle CssClass="hidden" />
                                                    <HeaderStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ToProductId" HeaderText="ProductId">
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
                        <div class="col-xs-2"></div>
                        <div class="col-xs-3">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" OnClientClick="Javascript: return confirm('Are you Confirm??');" Text="Submit Order" formnovalidate />
                           
                        </div>
                        <div class="col-xs-3">
                         <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-block" formnovalidate="" OnClick="btnClear_Click" Text="Clear" />
                    </div>    
                            <br />
                    </div>

                </asp:Panel>
            </div>
            <asp:HiddenField runat="server" ID="HIDFromProductID" />
            <asp:HiddenField runat="server" ID="HIDFromProductValue" />
            <asp:HiddenField runat="server" ID="HIDToProductID" />
            <asp:HiddenField runat="server" ID="HIDToProductValue" />

            <div class="form-group">
                <div class="col-xs-2"></div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="WarehouseStock" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            AllowSorting="True" PageSize="10" AllowPaging="True" Width="50%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:BoundField DataField="FromProduct" HeaderText="Product(From)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="ToProduct" HeaderText="Product(To)" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                               <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
            $("[id$=txtFromProductName]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ProductConvert.aspx/GetAutoCompleteData",
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

                    $("[id$=HIDFromProductID]").val(ui.item.val);
                    $("[id$=HIDFromProductValue]").val(ui.item.label);

                }

            });

            $("[id$=txtToProductName]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ProductConvert.aspx/GetAutoCompleteData",
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

                    $("[id$=HIDToProductID]").val(ui.item.val);
                    $("[id$=HIDToProductValue]").val(ui.item.label);

                }

            });
        }

    </script>
</asp:Content>
