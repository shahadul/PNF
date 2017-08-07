<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="SellReturn.aspx.cs" Inherits="PNF.UI.Return_Process.PurchaseReturn" %>
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
        <asp:Label runat="server" ID="lblMsg"></asp:Label>
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info" style="align-items: center;">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Sell Return</div>
                    <fieldset>

                        <div class="col-md-2"></div>
                        <div class="col-md-8">
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <label for="ddlPartyName">Dealer</label>
                                        <asp:DropDownList runat="server" ID="ddlPartyName" AutoPostBack="true" CssClass="form-control" Required OnSelectedIndexChanged="ddlPartyName_TextChanged" />
                                    </div>
                                      
                                    <div class="col-xs-4">
                                        <Label for="ddlProductGroup">Product Group</Label>
                                        <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    
                                    <div class="col-xs-4">
                                         <Label for="ddlProductName">Product Name</Label>
                                        <asp:DropDownList ID="ddlProductName" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-xs-4">
                                        <label for="ddlInvoice">Invoice Number</label>
                                        <asp:DropDownList runat="server" ID="ddlInvoice" CssClass="form-control" Required OnSelectedIndexChanged="ddlInvoice_SelectedIndexChanged" AutoPostBack="True" />
                                    </div> 
                                     <div class="col-xs-4">
                                        <label for="txtRemarks">Remarks</label>
                                       <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" type="text" />
                                    </div>
                                     <div class="col-xs-4" runat="server" Visible="False">
                                        <label for="ddlChallan">Challan Number</label>
                                        <asp:DropDownList runat="server" ID="ddlChallan" CssClass="form-control"  OnSelectedIndexChanged="ddlChallan_SelectedIndexChanged" AutoPostBack="True" />
                                    </div>

                                </div>
                               
                            </div>
                        </div>
                        <legend></legend>

                        <asp:HiddenField runat="server" ID="HIDOrderID" />
                        <asp:HiddenField runat="server" ID="HIDOrderDetailID" />
                       
                    </fieldset>
                </div>
                  <asp:Panel runat="server" ID="pnlOrderDetails">
                <div class="form-group">
                    <fieldset class="oderFieldSet" id="divInvoicedetails" runat="server">
                        <legend class="orderLegend">Invoice Details</legend>
                        <div class="row">

                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvInvoiceDetails" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" >
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                            <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtQuantity" Text='<%# Eval("Quantity") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount in Percent(%)" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="txtDiscount" min="0"  Text='<%# Eval("Discount") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Total" Visible="False" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Visible="False" Text='<%# Eval("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Return Quantity" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtReturnQuantity" onkeypress="return isNumberKey(event)" type="text" AutoPostBack="True" min="0" OnTextChanged="txtReturnQuantity_TextChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Return Total" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReturntotal" runat="server"></asp:Label>
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
                                           <asp:BoundField DataField="ReturnQuantity" HeaderText="ReturnQuantity">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReturnTotal" HeaderText="ReturnTotal">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                               <asp:TemplateField HeaderText="Is Sellable">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsSellable" runat="server"/>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="ChallanDetailID" HeaderText="ChallanDetailID">
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
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Text="Return" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-sm" Visible="False" formnovalidate="" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>

                    </fieldset>
                </div>


            </asp:Panel>
             <asp:Panel runat="server" ID="pnlReturnDetails">
                <div class="form-group">
                    <fieldset class="oderFieldSet" id="Fieldset1" runat="server">
                        <legend class="orderLegend">Return Details</legend>
                        <div class="row">

                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvReturnDetails" runat="server" Width="100%" PageSize="10" AllowPaging="True"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" OnPageIndexChanging="gvReturnDetails_PageIndexChanging" OnRowDataBound="gvReturnDetails_RowDataBound">
                                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                                        <RowStyle CssClass="rowStyle"></RowStyle>
                                        <PagerStyle CssClass="cssPager" />
                                        <Columns>
                                             <asp:BoundField DataField="InvNumber" HeaderText="Invoice Number" />
                                            <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                             <asp:BoundField DataField="UnitPrice" HeaderText="Price" />
                                         <asp:BoundField DataField="Discount" HeaderText="Discount in Percent(%)" />
                                             <asp:BoundField DataField="ReturnQuantity" HeaderText="Return Quantity" />
                                           <asp:BoundField DataField="ReturnTotal" HeaderText="ReturnTotal" />
                                            <asp:BoundField DataField="IsSellable" HeaderText="Is Sellable" />
                                            <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:MM/dd/yy}" />
                                            <asp:BoundField DataField="ProdId" HeaderText="ProdId">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InvDetailID" HeaderText="InvDetailID">
                                                <ItemStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                            </asp:BoundField>
                                            
                                        </Columns>
                                       
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>

                       
                    </fieldset>
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
   <%-- <script src="../assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="../DatePicker/jquery-ui.js"></script>
    <link href="../DatePicker/jquery-ui.css" rel="stylesheet" />
    <link href="../DatePicker/style.css" rel="stylesheet" />
    <script src="../assets/plugins/bootstrap/bootstrap.min.js"></script>
    <script src="../assets/plugins/metisMenu/metisMenu.min.js"></script>
    <script src="../assets/plugins/pace/pace.js"></script>
    <script src="../assets/scripts/siminta.js"></script>
    <script src="../DatePicker/script.js"></script>--%>

</asp:Content>
