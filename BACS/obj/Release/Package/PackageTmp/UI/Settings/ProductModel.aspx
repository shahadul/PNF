<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ProductModel.aspx.cs" Inherits="PNF.UI.Settings.ProductModel" %>
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
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">Product Model</div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                          
                            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                                    CssClass="StrongText"></asp:Label>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtModelName" CssClass="col-md-2 control-label">Model Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtModelName" CssClass="form-control" Required />
                                    </div>
                                    <asp:Label runat="server" AssociatedControlID="txtPetName" CssClass="col-md-2 control-label">Pet Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtPetName" CssClass="form-control" Required />
                                    </div>
                                  
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" AssociatedControlID="txtGenericName" CssClass="col-md-2 control-label">Generic Name</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:TextBox runat="server" ID="txtGenericName" CssClass="form-control" Required />
                                    </div>
                                     <asp:Label runat="server" AssociatedControlID="ddlProductGroup" CssClass="col-md-2 control-label">Product Group</asp:Label>
                                       <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlProductGroup" Enabled="True" CssClass="form-control col-md-2" Required runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                     <asp:Label runat="server" AssociatedControlID="ddlcategory" CssClass="col-md-2 control-label">Product Category</asp:Label>
                                    <div class="col-xs-2">
                                        <asp:DropDownList ID="ddlcategory" Enabled="True" CssClass="form-control col-md-2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                     <asp:Label runat="server" CssClass="col-md-2 control-label">Is Active</asp:Label>
                                   
                                    <div class="col-xs-2">
                                        <fieldset class="well the-fieldset">
                                            
                                            <div class="checkbox-inline">
                                                <asp:RadioButtonList ID="rdbtnIsActive" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <asp:HiddenField runat="server" ID="HIDModelID" />
                               
                                <div class="form-group">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-sm" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-primary btn-sm" formnovalidate/>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GRVProductModel" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            OnSelectedIndexChanging="GRVProductModel_SelectedIndexChanged" AllowSorting="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="GRVProductModel_PageIndexChanging" Width="100%" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                            <PagerStyle CssClass="cssPager" />
                             <Columns>
                                <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/arrow-right-double-2.png" ShowSelectButton="true" SelectImageUrl="~/images/arrow-right-double-2.png" SelectText="">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:CommandField>
                                <asp:BoundField DataField="ModelID" HeaderText="ModelID">
                                    <ItemStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="Model" />
                                <asp:BoundField DataField="Pet_Name" HeaderText="Pet Name" />
                                <asp:BoundField DataField="Generic" HeaderText="Generic" />
                                <asp:BoundField DataField="IsActive" HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>    
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


