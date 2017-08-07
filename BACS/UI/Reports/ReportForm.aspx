<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ReportForm.aspx.cs" Inherits="PNF.UI.Reports.ReportForm" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">

                        <div style="margin-left: auto; margin-right: auto; text-align: center;">
                            <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                                CssClass="StrongText"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="form-group" id="divreporttype" runat="server">
                                <asp:Label runat="server" AssociatedControlID="ddlReportType" CssClass="col-md-2 control-label">Report Type</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlReportType" Enabled="True" AutoPostBack="true" CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select Report Type--" Value="SelectReportType" />
                                        <asp:ListItem Text="Sales Process Report" Value="SalesProcessReport" />
                                        <asp:ListItem Text="Stock Related Report" Value="StockReport" />

                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divStock" runat="server">
                                <div class="form-group" runat="server">
                                    <asp:Label runat="server" AssociatedControlID="ddlreportcategorystock" CssClass="col-md-2 control-label">Report Category</asp:Label>
                                    <div class="col-xs-3">
                                        <asp:DropDownList ID="ddlreportcategorystock" Enabled="True" AutoPostBack="true" required CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="rbtnlist_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select One--" Value="Default" />
                                            <asp:ListItem Text="Stock Report" Value="StockReport" />
                                            <asp:ListItem Text="Purchase Details Report" Value="PurchaseDetailsReport" />
                                             <asp:ListItem Text="Product Wise Purchase Summary" Value="ProductWisePurchaseSummary" />
                                            <asp:ListItem Text="Stock Ledger Report" Value="StockLedgerReport" />
                                            <asp:ListItem Text="Stock Edging Report" Value="StockEdgingReport" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group" id="divStock_Stock" runat="server">
                                    <asp:Label runat="server" AssociatedControlID="txtDate" CssClass="col-md-2 control-label">Date</asp:Label>
                                    <div class="col-xs-3">
                                        <asp:TextBox ID="txtDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server" OnTextChanged="txtDate_TextChanged" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlProductGroupStock" CssClass="col-md-2 control-label">ProductGroup</asp:Label>
                                    <div class="col-xs-3">
                                        <asp:DropDownList ID="ddlProductGroupStock" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroupStock_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divStock_Purchase" runat="server">
                                    <div class="form-group" runat="server">
                                        <asp:Label runat="server" AssociatedControlID="txtStockFromDate" CssClass="col-md-2 control-label">From Date</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox ID="txtStockFromDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server" />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtStockToDate" CssClass="col-md-2 control-label">To Date</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox ID="txtStockToDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server" />
                                        </div>

                                    </div>
                                    <div class="form-group" runat="server">
                                        <asp:Label runat="server" AssociatedControlID="ddlVendor" CssClass="col-md-2 control-label">Vendor</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="ddlVendor" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="ddlPurchaseType" CssClass="col-md-2 control-label">Purchase Type</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:DropDownList ID="ddlPurchaseType" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-xs-5"></div>
                                <div class="col-xs-5">
                                    <asp:Button ID="btnSubmitStock" runat="server" Text="Generate Report" CssClass="btn btn-primary btn-block" OnClick="btnSubmitStock_Click" />
                                </div>
                            </div>
                        </div>


                        <div id="divsalesreport" runat="server">
                            <div class="form-group" runat="server">

                                <div id="divpartywiseuser" runat="server">
                                    <asp:Label runat="server" AssociatedControlID="ddlArea" CssClass="col-md-2 control-label">Area</asp:Label>
                                    <div class="col-xs-3">
                                        <asp:DropDownList ID="ddlArea" Enabled="True" AutoPostBack="True" CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtDealer" CssClass="col-md-2 control-label">Dealer</asp:Label>
                                    <div class="col-xs-3">
                                        <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />
                                        <%--  <asp:DropDownList ID="ddlPartyName" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                            </asp:DropDownList>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ddlProductGroup" CssClass="col-md-2 control-label">ProductGroup</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <asp:Label runat="server" AssociatedControlID="chkCategory" CssClass="col-md-2 control-label">Product Category</asp:Label>
                                <%--  <div class="col-xs-3">
                                        <asp:ListBox ID="ddlCategory" runat="server" Width="300px" SelectionMode="Multiple" Class="form-control multiddl"></asp:ListBox>

                                    </div>--%>

                                <div class="col-xs-3" style="overflow-y: scroll; background-color: #ffffff; text-align: left; height: 100px">

                                    <asp:CheckBoxList ID="chkCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkCategory_SelectedIndexChanged">
                                    </asp:CheckBoxList>
                                </div>




                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="ddlProductName" CssClass="col-md-2 control-label">Product Name</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList runat="server" ID="ddlProductName" CssClass="form-control" />
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtFromDate" CssClass="col-md-2 control-label">From Date</asp:Label>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txtFromDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server" />
                                </div>

                            </div>
                            <div class="form-group">

                                <asp:Label runat="server" AssociatedControlID="rbtnlist" CssClass="col-md-2 control-label">Report Category</asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="rbtnlist" Enabled="True" AutoPostBack="true" required CssClass="form-control col-md-2" runat="server" OnSelectedIndexChanged="rbtnlist_SelectedIndexChanged">
                                        <asp:ListItem Text="--Select One--" Value="Default" />
                                        <asp:ListItem Text="Pending Order" Value="Order" />
                                        <asp:ListItem Text="Approved Order" Value="ApprovedOrder" />
                                        <asp:ListItem Text="Pending Invoice" Value="Invoice" />
                                        <asp:ListItem Text="All Invoice" Value="AllInvoice" />
                                        <asp:ListItem Text="Net Invoice" Value="ApprovedInvoice" />
<%--                                        <asp:ListItem Text="Invoice/Deliverd Report" Value="InvoiceAndDeliverd" />
                                        <asp:ListItem Text="Deliverd Report" Value="Deliverd" />--%>
                                         <asp:ListItem Text="Deliverd Report" Value="DeliverdNew" />
                                        <asp:ListItem Text="Money Receipt" Value="MoneyReceipt" />
                                        <asp:ListItem Text="Sales Return" Value="SellReturn" />
                                     <%--   <asp:ListItem Text="Sales Summary" Value="SalesSummary" />--%>
                                        <asp:ListItem Text="Sales Summary" Value="SalesSummaryNew" />
                                       <%-- <asp:ListItem Text="Product Wise Sales Summary" Value="ProductWiseSalesSummary" />--%>
                                        <asp:ListItem Text="Product Wise Sales Summary" Value="ProductWiseSalesSummaryNew" />
                                        <asp:ListItem Text="Dealer Wise Sales Summary" Value="DealerWiseSalesSummary" />
                                        <asp:ListItem Text="Dealer Wise Account Statement" Value="DealerWiseAccountStatement" />
                                        <asp:ListItem Text="Dealer and Product Group Wise Account Statement " Value="DealerWiseProductGroupAccountStatement" />
                                        <asp:ListItem Text="Area Wise Account Statement" Value="AreaWiseAccountStatement" />
                                    </asp:DropDownList>
                                </div>
                                <asp:Label runat="server" AssociatedControlID="txtToDate" CssClass="col-md-2 control-label">To Date</asp:Label>
                                <div class="col-xs-3">
                                    <asp:TextBox ID="txtToDate" class="form-control datepicker" placeholder="dd/MM/yyyy" pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server" />
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-xs-5"></div>
                                <asp:Label runat="server" CssClass="col-md-2 control-label" ID="lblWhat" Font-Bold="True"></asp:Label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlNumber" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <legend></legend>
                            <div class="form-group">

                                <asp:Label runat="server" CssClass="col-md-2 control-label" Font-Bold="True"></asp:Label>
                                <div class="col-xs-3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Generate Report" CssClass="btn btn-primary btn-block" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" CssClass="hidden" formnovalidate />
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="HIDStockDetailID" />
                            <asp:HiddenField runat="server" ID="HIDDealerID" />
                            <asp:HiddenField runat="server" ID="HIDDealerValue" />
                            <div class="clearfix"></div>

                        </div>

                        <div class="row" align="center">
                            <asp:Panel runat="server" ID="pnlRpt">


                                <%--  <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" enableparameterprompt="False" toolpanelview="None" />--%>
                                <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" enableparameterprompt="False" toolpanelview="None"/>--%>
                                <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" />


                            </asp:Panel>
                        </div>

                    </div>
                </div>
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
            $("[id$=txtDealer]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ReportForm.aspx/GetAutoCompleteData",
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
                    $("[id$=HIDDealerID]").val(ui.item.val);
                    $("[id$=HIDDealerValue]").val(ui.item.label);
                },
                change: function (event, ui, response) {
                    $("[id$=Button2]").click();
                }

            });

        }

    </script>
    </div>
</asp:Content>
