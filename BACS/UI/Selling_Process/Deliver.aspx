<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="Deliver.aspx.cs" Inherits="PNF.UI.Selling_Process.Deliver" %>

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
            <fieldset class="ChallanFieldSet">

                <legend class="ChallanLegend">Pending Challans for Deliver</legend>
                <div class="row">
                    <asp:Label runat="server" AssociatedControlID="ddlChallanNumber" CssClass="col-md-2 control-label">Challan Number</asp:Label>
                    <div class="col-xs-3">
                        <asp:DropDownList ID="ddlChallanNumber" runat="server" CssClass="form-control col-md-3" Enabled="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-3">
                        <asp:Button ID="btnChallan" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnChallan_Click" Text="Print Challan" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-xs-8">
                        <asp:GridView ID="grdChallan" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="grdChallan_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="grdChallan_PageIndexChanging" Width="100%" BackColor="White" BChallanColor="#3366CC" BChallanStyle="None" BChallanWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ChallanMasterID" HeaderText="ChallanMasterID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ChallanDate" HeaderText="Challan Date" DataFormatString="{0:dd/MM/yy}" />
                                <asp:BoundField DataField="ChallanNumber" HeaderText="Challan No" />
                                <asp:BoundField DataField="InvID" HeaderText="Invoice No" />
                                <asp:BoundField DataField="TTLChallanQty" HeaderText="Challan Qty" />
                                <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                <asp:BoundField DataField="InvID" HeaderText="InvID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PartyID" HeaderText="PartyID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PartyCode" HeaderText="PartyCode">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PartyName" HeaderText="PartyName">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Partymobile" HeaderText="Partymobile">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="asmMobile" HeaderText="asmMobile">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dsmMobile" HeaderText="dsmMobile">
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
            </fieldset>
            <br />
            <div id="divoerderdetails" runat="server">
                <asp:Panel runat="server" ID="pnlChallanDetails" class="panel panel-default">
                    <fieldset class="oderFieldSet">
                        <legend class="ChallanLegend">Pending Challan Details for Deliver</legend>
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-8">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvChallanDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                            <asp:BoundField DataField="ChalanQty" HeaderText="Quantity" />
                                            <asp:BoundField DataField="ProdId" HeaderText="ProdId">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ChallanDetailID" HeaderText="ChallanDetailID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="ChalanTotal" HeaderText="ChalanTotal">
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

                            <asp:Label runat="server" AssociatedControlID="txtDeliverDate" CssClass="col-md-2 control-label">Deliver Date</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtDeliverDate" class="form-control" ReadOnly="True" placeholder="dd/MM/yyyy" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="txtRemarks" CssClass="col-md-1 control-label">Remarks</asp:Label>
                            <div class="col-xs-2">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" type="text" />
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-xs-2">
                                <asp:HiddenField ID="HidTotalQty" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Deliver Now" CssClass="btn btn-primary btn-block" OnClientClick="Javascript: return confirm('Are you Confirm??');" OnClick="btnSubmit_Click" />

                            </div>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <br />
            <fieldset class="ChallanDetailsFieldSet">

                <legend class="ChallanLegend">Delivery Details </legend>
                <div class="form-group">
                    <div id="divpartywiseuser" runat="server">
                        <asp:Label runat="server" CssClass="col-md-2 control-label">Find By Dealer</asp:Label>
                        <div class="col-xs-3">
                              <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />
                          <%--  <asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="true" CssClass="form-control col-md-3" Enabled="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                            </asp:DropDownList>--%>
                        </div>

                        <div class="col-xs-4">
                            <asp:Button ID="btnInvoice" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnInvoice_Click" Text="Print Invoice" />
                            <asp:Button ID="btnChallan1" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnChallan1_Click" Text="Print Challan" />
                            <asp:Button ID="btnDelivery" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnDelivery_Click" Text="Print Delivery" />
                        </div>
                        <asp:HiddenField ID="hidChallanMasterID" runat="server" />
                        <asp:HiddenField ID="hidInvoiceID" runat="server" />

                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-xs-8">
                        <asp:GridView ID="grdDelivery" runat="server" AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanging="grdDelivery_SelectedIndexChanged"
                            AllowSorting="True" OnPageIndexChanging="grdDelivery_PageIndexChanging" PageSize="10" AllowPaging="True" Width="100%" BackColor="White" BChallanColor="#3366CC" BChallanStyle="None" BChallanWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yy}" />
                                <asp:BoundField DataField="DeliveryID" HeaderText="Delivery No" />
                                <asp:BoundField DataField="DeliveryQty" HeaderText="Delivery Qty" />
                                <asp:BoundField DataField="ChallanMasterID" HeaderText="Challan No" />
                                <asp:BoundField DataField="InvID" HeaderText="Inv ID" />
                                <asp:BoundField DataField="PartyName" HeaderText="Dealer" />

                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </fieldset>
             <asp:Button ID="btnDealerchange" runat="server" OnClick="btnDealerchange_Click" Text="Button" CssClass="hidden" formnovalidate />
                    <asp:HiddenField runat="server" ID="HIDDealerID" />
                    <asp:HiddenField runat="server" ID="HIDDealerValue" />
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
                         url: "Deliver.aspx/GetAutoCompleteData",
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
