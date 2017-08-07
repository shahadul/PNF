<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="InvoiceApproval.aspx.cs" Inherits="PNF.UI.Selling_Process.InvoiceApproval" %>

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
                <div class="form-group col-xs-12">
                    <fieldset class="oderFieldSet">
                        <legend class="orderLegend">Pending Invoice</legend>
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvInvoice" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvInvoice_PageIndexChanging" OnSelectedIndexChanging="gvInvoice_SelectedIndexChanging">
                                        <%-- <asp:GridView ID="gvInvoice" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvInvoice_PageIndexChanging" OnSelectedIndexChanging="gvInvoice_SelectedIndexChanging">--%>
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="InvNumber" HeaderText="Invoice Number" />
                                            <asp:BoundField DataField="InvDate" HeaderText="Invoice Date" DataFormatString="{0:MM/dd/yy}" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" />
                                            <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
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
                            <asp:HiddenField runat="server" ID="HIDTotalQTY" />
                        </div>
                    </fieldset>
                    <fieldset class="oderFieldSet" id="divInvoicedetails" runat="server">
                        <legend class="orderLegend">Invoice Details</legend>
                        <div class="row">

                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvInvoiceDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtQuantity" AutoPostBack="True" min="1" Text='<%# Eval("Quantity") %>' OnTextChanged="txtQuantity_TextChanged" required />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
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
                                            <asp:BoundField DataField="InvDetailID" HeaderText="InvDetailID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
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

                    </fieldset></div>
                </fieldset>
                <div class="row">
                    <legend class="orderLegend">Approved Invoice</legend>
                    <div class="form-group">
                        <div id="divpartywiseuser" runat="server">
                            <asp:Label runat="server" CssClass="col-md-2 control-label">Find By Dealer</asp:Label>
                            <div class="col-xs-3">
                                <asp:TextBox type="text" ID="txtDealer" runat="server" class="form-control" />
                                <%--<asp:DropDownList ID="ddlPartyName" runat="server" AutoPostBack="true" CssClass="form-control col-md-2" Enabled="True" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                            </div>
                            <asp:Label runat="server" AssociatedControlID="ddlInvoiceNumber" CssClass="col-md-2 control-label">Challan / Invoice Number</asp:Label>
                            <div class="col-xs-2">
                                <asp:DropDownList ID="ddlInvoiceNumber" runat="server" CssClass="form-control col-md-2" Enabled="True">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="btnChallan" runat="server" Visible="False" CssClass="btn btn-primary btn-sm" OnClick="btnChallan_Click" Text="Print Challan" />
                            <asp:Button ID="btnInvoice" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnInvoice_Click" Text="Print Invoice" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="grvApprovedInvoice" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="grvApprovedInvoice_PageIndexChanging" OnSelectedIndexChanging="grvApprovedInvoice_SelectedIndexChanging" PageSize="8" Width="100%">
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="Select" ShowSelectButton="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="InvNumber" HeaderText="Invoice Number" />
                                        <asp:BoundField DataField="InvDate" DataFormatString="{0:MM/dd/yy}" HeaderText="Invoice Date" />
                                        <asp:BoundField DataField="AreaName" HeaderText="Area" />
                                        <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                                        <asp:BoundField DataField="ApprovedQty" HeaderText="Qunatity" />
                                        <asp:BoundField DataField="ApprovedAmount" HeaderText="Total Invoice Amount" />
                                        <asp:BoundField DataField="IsChallan" HeaderText="Challan Status" />
                                         <asp:TemplateField HeaderText="Select" Visible="False" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                         <asp:BoundField DataField="InvID" HeaderText="InvID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                         <asp:BoundField DataField="OrderID" HeaderText="OrderID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                        <%-- <asp:BoundField DataField="ApprovedTotalAmnt" HeaderText="Total Approved Amount"/>--%>
                                    </Columns>
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
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
                     <legend></legend>
                      <div class="form-group">
                           <div class="col-xs-9">
                               
                            </div>
                        <div id="div1" Visible="False" runat="server">
                            <asp:Button ID="btnCancelNow" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancelNow_Click" Text="Cancel Now" />
                           &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDeliver" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnDeliver_Click" Text="Deliver Now" />
                        </div>
                         
                    </div>
                </div>
                <div class="row" id="divdeliver" Visible="False" runat="server">
                    <legend class="orderLegend">Delivered Order</legend>
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="grvDeliverOrder" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnPageIndexChanging="grvDeliverOrder_PageIndexChanging" OnSelectedIndexChanging="grvDeliverOrder_SelectedIndexChanging" PageSize="8" Width="100%">
                                    <PagerStyle CssClass="cssPager" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="Select" ShowSelectButton="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="InvNumber" HeaderText="Invoice Number" />
                                        <asp:BoundField DataField="InvDate" DataFormatString="{0:MM/dd/yy}" HeaderText="Invoice Date" />
                                        <asp:BoundField DataField="AreaName" HeaderText="Area" />
                                        <asp:BoundField DataField="PartyName" HeaderText="Dealer" />
                                        <asp:BoundField DataField="ApprovedQty" HeaderText="Qunatity" />
                                        <asp:BoundField DataField="ApprovedAmount" HeaderText="Total Invoice Amount" />
                                        <asp:BoundField DataField="DeliveryStatus" HeaderText="Delivery Status" />                                       
                                         <asp:BoundField DataField="InvID" HeaderText="InvID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                         <asp:BoundField DataField="OrderID" HeaderText="OrderID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                       
                                    </Columns>
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
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
                </div>
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
                         url: "InvoiceApproval.aspx/GetAutoCompleteData",
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

