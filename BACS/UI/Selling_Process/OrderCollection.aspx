<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="OrderCollection.aspx.cs" Inherits="PNF.UI.Selling_Process.rtOrderCollection" %>

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

        .ui-widget-content {
            top: 195px !important;
            left: 501px !important;
        }
    </style>
    <div id="page-wrapper">
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <div class="form-horizontal" role="form">
            <div class="panel-group">

                <div class="panel panel-info" style="align-items: center;">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Order Collection</div>
                    <fieldset>

                        <div class="col-md-2"></div>
                        <div class="col-md-8">
                            <div class="panel-body">
                                <div class="form-group">
                                    <%--  <div class="col-xs-4">
                                        <label for="ddlPartyName">Dealer</label>
                                        <asp:DropDownList runat="server" ID="ddlPartyName" AutoPostBack="true" CssClass="form-control" Required OnSelectedIndexChanged="ddlPartyName_TextChanged" />
                                    </div>--%>
                                    <div class="col-xs-4">

                                        <label for="txtDealer">Dealer</label>
                                        <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />


                                    </div>
                                    <div class="col-xs-4">

                                        <label for="txtDealerCode">Dealer Code</label>
                                        <asp:TextBox runat="server" ID="txtDealerCode" class="form-control" ReadOnly="true" />

                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtAreaName">Area Name</label>
                                        <asp:TextBox runat="server" ID="txtAreaName" class="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <label for="txtDate">Date</label>
                                        <asp:TextBox ID="txtDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server"
                                            Required />
                                        <%--<asp:TextBox runat="server" ID="txtDate" class="form-control datepicker" placeholder="dd/MM/yyyy" ReadOnly="False" Required />--%>
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="ddlProductGroup">Product Group</label>
                                        <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" Required runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="ddlCategory">Product Category</label>
                                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" Required AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" />
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <label for="ddlProductName">Product Name</label>
                                        <asp:DropDownList runat="server" ID="ddlProductName" CssClass="form-control" Required OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged" AutoPostBack="True" />
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtProductCode">Product Code</label>
                                        <asp:TextBox runat="server" ID="txtProductCode" class="form-control" ReadOnly="true" />
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtUnitPrice">Price</label>
                                        <asp:TextBox runat="server" ID="txtUnitPrice" CssClass="form-control" value="0" type="number" ReadOnly="True" Required />
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <label for="txtOrderQty">Order Quantity</label>
                                        <asp:TextBox runat="server" ID="txtOrderQty" CssClass="form-control" min="1" oninput="validity.valid||(value='');" type="number" Required pattern="[0-9]{1,10}" AutoPostBack="True" OnTextChanged="txtQty_TextChanged" />
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtDiscount">Discount in Percent(%)</label>
                                        <asp:TextBox runat="server" ID="txtDiscount" CssClass="form-control" placeholder="Discount in Percent(%)" min="0" onkeypress="return isNumberKey(event)" type="text" AutoPostBack="True" OnTextChanged="txtDiscount_TextChanged" />
                                    </div>
                                    <div class="col-xs-4">
                                        <label for="txtTotalPrice">Total</label>
                                        <asp:TextBox runat="server" ID="txtTotalPrice" CssClass="form-control" type="number" ReadOnly="True" Required />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <label for="txtGrandTotal">Grand Total</label>
                                        <asp:TextBox runat="server" ID="txtGrandTotal" CssClass="form-control" type="number" ReadOnly="True" Required />
                                    </div>
                                   
                                </div>

                            </div>
                        </div>
                        <legend></legend>

                        <asp:HiddenField runat="server" ID="HIDDealerID" />
                        <asp:HiddenField runat="server" ID="HIDDealerValue" />

                        <asp:HiddenField runat="server" ID="HIDDealerCode" />
                        <asp:HiddenField runat="server" ID="HIDDealerArea" />
                        <asp:HiddenField runat="server" ID="HIDOrderDetailID" />
                        <div class="form-group">
                            <div class="col-xs-4"></div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click" Text="Add Item" />
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                        </label>
                    </fieldset>
                </div>
            </div>

            <asp:Panel runat="server" ID="pnlOrderDetails">
                <div class="form-group col-xs-12">
                    <fieldset class="oderFieldSet">
                        <legend class="orderLegend">Order Details</legend>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderDetail" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDeleting="gvOrderDetail_RowDeleting" OnPageIndexChanging="gvOrderDetail_PageIndexChanging" OnRowCommand="gvOrderDetail_RowCommand">
                                        <%-- <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                    <RowStyle CssClass="rowStyle"></RowStyle>--%>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                            <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" />
                                            <asp:BoundField DataField="Discount" HeaderText="Discount in Percent(%)" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" />

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
                                        <%--<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />--%>
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
            <%--------------------------------------------------------------------%>
            <asp:Panel runat="server" ID="pnlOrderShow">
                <div class="form-group col-xs-12">
                    <fieldset class="oderFieldSet">
                        <legend class="orderLegend">Order Information</legend>
                        <div class="row">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <div class="col-lg-10 ">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvOrderShow" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" AllowPaging="True">
                                                <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                                <RowStyle CssClass="rowStyle"></RowStyle>
                                                <Columns>
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <legend></legend>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>

            <br />
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
                        url: "OrderCollection.aspx/GetAutoCompleteData",
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

                    $.ajax({

                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "OrderCollection.aspx/GetDealercode",
                        data: "{'Party':'" + ui.item.val + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {

                                $("[id$=HIDDealerCode]").val(item.split('//')[0]);
                                $("[id$=HIDDealerArea]").val(item.split('//')[1]);
                                $("[id$=txtDealerCode]").val(item.split('//')[0]);
                                $("[id$=txtAreaName]").val(item.split('//')[1]);

                            }));
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });


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
