<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="MoneyReceipt.aspx.cs" Inherits="PNF.UI.Selling_Process.MoneyReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <br />

            <div class="panel panel-info" style="align-items: center;">
                <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Money Receipt</div>

                <fieldset>
                    <legend></legend>
                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    <div class="col-md-12">
                        <div class="panel-body">
                            <div class="form-group">

                                <asp:Label runat="server" AssociatedControlID="txtMRDate" CssClass="col-md-2 control-label">MR Date</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtMRDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server"
                                        Required />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtDealer" CssClass="col-md-2 control-label">Dealer</asp:Label>
                                <div class="col-xs-4">
                                    <%--<label for="txtDealer">Dealer</label>--%>
                                    <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />

                                </div>


                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="txtSerial" CssClass="col-md-2 control-label">Serial</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txtSerial" class="form-control" type="text" runat="server" />

                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtRemarks" CssClass="col-md-2 control-label">Remarks</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" type="text" />
                                </div>
                            </div>
                            <div class="form-group">

                                <asp:Label ID="lblTotalInvoice" runat="server" AssociatedControlID="txtTotalInvoice" CssClass="col-md-2 control-label">Total Invoice Amount</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox runat="server" ID="txtTotalInvoice" CssClass="form-control" Enabled="False" />
                                </div>
                                <asp:Label ID="lblTotalMRAmount" runat="server" AssociatedControlID="txtTotalMRAmount" CssClass="col-md-2 control-label">Total MR Amount</asp:Label>
                                <div class="col-xs-2">
                                    <asp:TextBox runat="server" ID="txtTotalMRAmount" CssClass="form-control" />
                                </div>

                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ddlCollectionType" CssClass="col-md-2 control-label">Collection Type</asp:Label>
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="ddlCollectionType" runat="server" CssClass="form-control col-md-2" Enabled="True" required>
                                    </asp:DropDownList>
                                </div>
                                <asp:Label runat="server" CssClass="col-md-2 control-label">Is Collect</asp:Label>

                                <div class="col-xs-2">
                                    <fieldset class="well the-fieldset">

                                        <div class="checkbox-inline">
                                            <asp:RadioButtonList ID="rdbtnIsCollect" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </fieldset>
                                </div>

                            </div>
                        </div>
                    </div>

                </fieldset>

                <fieldset class="oderFieldSet">
                    <legend class="orderLegend"></legend>
                    <div class="row" id="divdetails" runat="server">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False">
                                    <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                    <RowStyle CssClass="rowStyle"></RowStyle>
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                        <asp:TemplateField HeaderText="Due Amount" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDueAmount" runat="server" Text='<%# Eval("OutstandingAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPresentPayment" min="0" type="number" Text='<%# Eval("PresentPayment") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProductGroupID" HeaderText="ProductGroupID">
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

            </div>
            <div class="row">
                <legend></legend>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ddlCheckedBy" CssClass="col-md-2 control-label">Received By</asp:Label>
                    <div class="col-xs-3">
                        <asp:DropDownList runat="server" ID="ddlCheckedBy" CssClass="form-control" Required AutoPostBack="true" OnSelectedIndexChanged="ddlCheckedBy_SelectedIndexChanged">

                            <asp:ListItem Text="CASH" Value="1" />
                            <asp:ListItem Text="CHEQUE" Value="2" />
                            <asp:ListItem Text="PO" Value="3" />
                        </asp:DropDownList>
                    </div>
                    <div id="divbankname" runat="server">
                        <asp:Label runat="server" AssociatedControlID="ddlBankName" CssClass="col-md-2 control-label">Bank Name</asp:Label>
                        <div class="col-xs-3">
                            <asp:DropDownList runat="server" ID="ddlBankName" CssClass="form-control" Required>
                                <%-- <asp:ListItem Text="Bangladesh Bank" Value="1" />
                                            <asp:ListItem Text="Sonali Bank" Value="2" />
                                            <asp:ListItem Text="Rupali Bank" Value="3" />
                                            <asp:ListItem Text="Janata Bank" Value="4" />--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" runat="server" id="divCheque_PO_NO">

                    <asp:Label runat="server" AssociatedControlID="txtCheque_PO_NO" CssClass="col-md-2 control-label">Cheque/PO NO.</asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox runat="server" ID="txtCheque_PO_NO" CssClass="form-control" type="text" placeholder="Cheque/PO NO." required />
                    </div>
                    <asp:Label runat="server" AssociatedControlID="txtChequeDate" CssClass="col-md-2 control-label">Cheque/PO Date</asp:Label>
                    <div class="col-xs-3">
                        <asp:TextBox runat="server" ID="txtChequeDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" required />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-2">
                    </div>
                    <div class="col-xs-4">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btnExtra" OnClientClick="Javascript: return confirm('Are you Confirm??');" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btnExtra" formnovalidate />
                    </div>
                    <br />
                    <asp:HiddenField runat="server" ID="HIDDealerID" />
                    <asp:HiddenField runat="server" ID="HIDDealerValue" />
                </div>
            </div>
        </div>

        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" CssClass="hidden" formnovalidate />
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
                        url: "MoneyReceipt.aspx/GetAutoCompleteData",
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
                    $("[id$=Button2]").click();

                }
            });

        }

    </script>

</asp:Content>

