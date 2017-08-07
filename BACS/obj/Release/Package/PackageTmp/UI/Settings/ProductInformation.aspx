<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ProductInformation.aspx.cs" Inherits="PNF.UI.Settings.ProductInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style type="text/css">           
             
              .cssPager td
            {
                  padding-left: 4px;     
                  padding-right: 4px;  
                  background-color:#4f6b72;
                  font-size:18px;  
                  color:Highlight;
              }
        </style>
    <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Product Information</div>
                    <asp:UpdatePanel runat="server" ID="upduserregister">
                        <ContentTemplate>
                            <%--<asp:Label runat="server"  ID="lblmessage" style="text-align:center" Visible="false" ></asp:Label>--%>
                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                                    CssClass="StrongText"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtProductName" CssClass="col-md-2 control-label">Product Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtProductName" CssClass="form-control" Required />
                                    </div>
                                     <asp:Label runat="server" AssociatedControlID="ddlProductGroup" CssClass="col-md-2 control-label">Product Group</asp:Label>
                                       <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" Required runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                     <asp:Label runat="server" AssociatedControlID="ddlcategory" CssClass="col-md-2 control-label">Category</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlcategory" Enabled="True" CssClass="form-control col-md-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtShortName1" CssClass="col-md-2 control-label">Product Code</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtShortName1" CssClass="form-control" />
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                     <asp:Label runat="server" AssociatedControlID="txtShortName2" CssClass="col-md-2 control-label">2nd Short Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtShortName2" CssClass="form-control" pattern="[a-zA-Z0-9.'- ]{2,40}" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="ddlproductmodel" CssClass="col-md-2 control-label">Product Model Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlproductmodel" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtBarCode" CssClass="col-md-2 control-label">BarCode</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtBarCode" CssClass="form-control" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtBarCodeTemp" CssClass="col-md-2 control-label">BarCode Temp</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtBarCodeTemp" CssClass="form-control" />
                                    </div>
                                   
                                </div>
                                <div class="form-group">
                                     <asp:Label runat="server" AssociatedControlID="txtImpcost" CssClass="col-md-2 control-label">Imp Cost</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtImpcost" type="number" min="0" CssClass="form-control" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtDealerPrice" CssClass="col-md-2 control-label">Dealer Price</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtDealerPrice" type="number" min="0" CssClass="form-control" />
                                    </div>
                                    
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtRetailPrice" CssClass="col-md-2 control-label">Retail Price</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtRetailPrice" type="number" min="0" CssClass="form-control" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtDiscount" CssClass="col-md-2 control-label">Discount</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtDiscount" CssClass="form-control" />
                                    </div>
                                    
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtImportCode" CssClass="col-md-2 control-label">Import Code</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtImportCode" CssClass="form-control" />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtReorderPoint" CssClass="col-md-2 control-label">Re-orderPoint</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" type="number" min="0" value="0" ID="txtReorderPoint" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-xs-2">
                                        <fieldset class="well the-fieldset">
                                            <legend class="the-legend">Is Detail</legend>
                                            <div class="checkbox-inline">
                                                <asp:RadioButtonList ID="chkIsDetails" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkIsDetails_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-xs-2">
                                        <fieldset class="well the-fieldset">
                                            <legend class="the-legend">Is EOL</legend>
                                            <div class="checkbox-inline">
                                                <asp:RadioButtonList ID="chkIsEOL" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkIsEOL_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-xs-2">
                                        <fieldset class="well the-fieldset">
                                            <legend class="the-legend">Is Active</legend>
                                            <div class="checkbox-inline">
                                                <asp:RadioButtonList ID="rdbtnIsActive" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <asp:Label runat="server" ID="lblEOLDate" CssClass="col-md-2 control-label">EOL Date</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox ID="txtEOLDate" class="form-control datepicker" placeholder="dd/MM/yyyy" required pattern="\d{1,2}-\d{1,2}-\d{4}" runat="server"/>                                       
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlDetails">
                                      <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt1" CssClass="col-md-2 control-label">Attribute 1</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt1" CssClass="form-control"  />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt2" CssClass="col-md-2 control-label">Attribute 2</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt2" CssClass="form-control"  />
                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt3" CssClass="col-md-2 control-label">Attribute 3</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt3" CssClass="form-control" />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt4" CssClass="col-md-2 control-label">Attribute 4</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt4" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt5" CssClass="col-md-2 control-label">Attribute 5</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt5" CssClass="form-control"  />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt6" CssClass="col-md-2 control-label">Attribute 6</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt6" CssClass="form-control"  />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt7" CssClass="col-md-2 control-label">Attribute 7</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt7" CssClass="form-control"  />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt8" CssClass="col-md-2 control-label">Attribute 8</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt8" CssClass="form-control"  />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt9" CssClass="col-md-2 control-label">Attribute 9</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt9" CssClass="form-control"  />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="txtAtrbt10" CssClass="col-md-2 control-label">Attribute 10</asp:Label>
                                        <div class="col-xs-3">
                                            <asp:TextBox runat="server" ID="txtAtrbt10" CssClass="form-control"  />
                                        </div
                                    </div>
                                  
                                    

                                </asp:Panel>
                                 <asp:HiddenField runat="server" ID="HIDProdID" />
                                <asp:HiddenField runat="server" ID="HIDProdDetailID" />
                                <div class="form-group">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-sm" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btn-sm" formnovalidate />
                                    </div>

                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group" style="overflow: auto;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GRVProductInfo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="GRVProductInfo_SelectedIndexChanged" AllowSorting="True" PageSize="8" AllowPaging="True" OnPageIndexChanging="GRVProductInfo_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="" HeaderText="Select">
                                    <ItemStyle HorizontalAlign="Right" Wrap="True" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ProdID" HeaderText="ProdID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProdName" HeaderText="Product Name" />
                                  <asp:BoundField DataField="Barcode" HeaderText="Barcode">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="BarcodeTemp" HeaderText="BarcodeTemp">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="DealerPrice" HeaderText="Dealer Price" />
                                <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                                 <asp:BoundField DataField="EOLDate" HeaderText="EOLDate" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ICode" HeaderText="ICode">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="EOLDate" HeaderText="EOL Date"  DataFormatString="{0:dd-MM-yyyy}"/>--%>
                                <%--<asp:BoundField DataField="ICode" HeaderText="Import Code" />--%>
                                <asp:BoundField DataField="ImportCost" HeaderText="Import Cost" />
                                <asp:BoundField DataField="IsActive" HeaderText="Is Active" ItemStyle-HorizontalAlign="Center"/>
                                 <asp:BoundField DataField="IsDetail" HeaderText="IsDetail">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="IsEOL" HeaderText="IsEOL">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="IsDetail" HeaderText="Is Has Detail" />
                                <asp:BoundField DataField="IsEOL" HeaderText="Is EOL" />--%>
                                <asp:BoundField DataField="ReorderPoint" HeaderText="Reorder Point" />
                                <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" />
                                <asp:BoundField DataField="Short1" HeaderText="Short1">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                
                                  <asp:BoundField DataField="Short2" HeaderText="Short2">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="DateAdded" HeaderText="DateAdded" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="DateModified" HeaderText="DateModified" DataFormatString="{0:dd-MM-yyyy}">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="Short1" HeaderText="1st Short Name" />
                                <asp:BoundField DataField="Short2" HeaderText="2nd Short Name" />
                                <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:dd-MM-yyyy}" />
                                <asp:BoundField DataField="DateModified" HeaderText="Date Modified" DataFormatString="{0:dd-MM-yyyy}" />--%>

                                <asp:BoundField DataField="Atrbt1" HeaderText="Attribute 1" />
                                 <asp:BoundField DataField="Atrbt2" HeaderText="Atrbt2">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt3" HeaderText="Atrbt3">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt4" HeaderText="Atrbt4">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt5" HeaderText="Atrbt5">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt6" HeaderText="Atrbt6">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt7" HeaderText="Atrbt7">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt8" HeaderText="Atrbt8">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt9" HeaderText="Atrbt9">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="Atrbt10" HeaderText="Atrbt10">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
 <%--                               <asp:BoundField DataField="Atrbt2" HeaderText="Attribute 2" />
                                <asp:BoundField DataField="Atrbt3" HeaderText="Attribute 3" />
                                <asp:BoundField DataField="Atrbt4" HeaderText="Attribute 4" />
                                <asp:BoundField DataField="Atrbt5" HeaderText="Attribute 5" />
                                <asp:BoundField DataField="Atrbt6" HeaderText="Attribute 6" />
                                <asp:BoundField DataField="Atrbt7" HeaderText="Attribute 7" />
                                <asp:BoundField DataField="Atrbt8" HeaderText="Attribute 8" />
                                <asp:BoundField DataField="Atrbt9" HeaderText="Attribute 9" />
                                <asp:BoundField DataField="Atrbt10" HeaderText="Attribute 10" />--%>
                                <asp:BoundField DataField="Category" HeaderText="Product Category" />
                                <asp:BoundField DataField="Model" HeaderText="Product Model" />
                                
                                <asp:BoundField DataField="ProdDetailID" HeaderText="ProdDetailID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModelID" HeaderText="ModelID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="ProductGroupID" HeaderText="ProductGroupID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="CategoryID" HeaderText="CategoryID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                               
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
