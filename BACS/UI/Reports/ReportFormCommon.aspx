<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="ReportFormCommon.aspx.cs" Inherits="PNF.UI.Reports.ReportFormCommon" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
     <div id="page-wrapper">
        <div class="form-horizontal" role="form">
            <div class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-heading text-center" style="font-weight: bold; font-size: medium;">
                        
                        Report</div>
                 
                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                        <asp:Label ID="lblmessage" runat="server" Text="" Font-Bold="true"
                            CssClass="StrongText"></asp:Label>
                    </div>

                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Panel runat="server" ID="dvReport">
                                <div align="center">
                                  <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /> 

                                </div>
                            </asp:Panel>
                             
                        </div>

                    </div>
                    
                </div>
            </div>
        </div>

    </div>
    <%--  <script type="text/javascript">
       window.onload= function Print() {
              var dvReport = document.getElementById("MainContent_dvReport");
              var frame1 = dvReport.getElementsByTagName("iframe")[0];
              if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1 || navigator.appName.indexOf("Chrome") != -1) {
                  frame1.name = frame1.id;
                  window.frames[frame1.id].focus();
                  window.frames[frame1.id].print();
              }
              else {
                  var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                  frameDoc.print();
              }
          }
    </script>--%>
  
</asp:Content>
