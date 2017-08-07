<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PNF.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PNF Software</title>
    <link href="design/default.css" rel="stylesheet" />
    <link href="design/bootstrap.min.css" rel="stylesheet" />
    <script src="design/bootstrap.min.js"></script>
    <style>
        body {
            margin: 0px !important;
            padding-top: 122px !important;
            /*background-color: rgb(189, 174, 174) !important;*/
            background-image: url("./images/pimgpsh_fullsize_distr.jpg");
            /*background-image: url("/images/gradient_bg.png");*/
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>--%>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4" style="padding-left: 10px;">
                <div class="panel panel-info" style="border-radius: 22px;">
                    <div class="panel-heading" style="border-radius: 50px;">Login to PNF System</div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="row">
                                <br />
                            </div>
                            <asp:Label runat="server" ID="lblerror" ForeColor="red"></asp:Label>
                            <div class="row">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="txtUserName" CssClass="col-md-4 control-label">User name</asp:Label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="User name" required />
                                </div>
                            </div>
                            <div class="row">
                                <br />
                            </div>
                            <div class="row">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword" CssClass="col-md-4 control-label">Password</asp:Label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="Password" />
                                </div>
                            </div>
                            <div class="row">
                                <input type="hidden" id="_ispostback" value="<%=Page.IsPostBack.ToString()%>" />                               
                                <asp:HiddenField runat="server" ID="HIDLT" />
                                <asp:HiddenField runat="server" ID="HIDLG" />

                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                <%--<asp:Label ID="lblLT" runat="server"></asp:Label>
                                <asp:HiddenField ID="Hidden1" runat="server" />--%>
                                <asp:Label ID="lblLG" runat="server" Text="" ></asp:Label>
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-offset-4 col-md-10">
                                    <asp:Button ID="btnLogIn" runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <%-- <script>
         window.onload = function () {
          
            // document.getElementById("Label3").innerHTML = "sdfgh";
         };
         document.addEventListener('DOMContentLoaded', function () {
             var LT;
             LT = document.getElementById("HIDLT");
             var LG = document.getElementById("HIDLG");
           //  document.getElementById("lblLT").innerHTML = LT.innerHTML;
             function isPostBack() { //function to check if page is a postback-ed one
                 return document.getElementById('_ispostback').value == 'True';
             }
             if (!isPostBack())
             {
                 getLocation();

             }
             function getLocation() {
                 
                 if (navigator.geolocation) {
                     navigator.geolocation.getCurrentPosition(showPosition, showError);
                 } else {
                     alert("Geolocation is not supported by this browser.");
                     
                 }

             }

             function showPosition(position) {
                 document.getElementById('<%= HIDLT.ClientID %>').value = position.coords.latitude;
                 document.getElementById('<%= HIDLG.ClientID %>').value = position.coords.longitude;
                 //LT.innerHTML = position.coords.latitude;              
                 //LG.innerHTML = position.coords.longitude;
               
             }

             function showError(error) {
                 switch (error.code) {
                     case error.PERMISSION_DENIED:
                        
                         alert("User denied the request for Geolocation.");
                         break;
                     case error.POSITION_UNAVAILABLE:
                        
                         alert("Location information is unavailable.");
                         break;
                     case error.TIMEOUT:
                         alert("The request to get user location timed out.");
                         
                         break;
                     case error.UNKNOWN_ERROR:
                         alert("Location information is unavailable.");
                       
                         break;
                 }
             }
           
         });
        
    </script>--%>
</body>
</html>
