<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MP/MpAdmin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PNF.UI.MP.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style>
        .anim {
            width: 400px;
            height: 50px;
            padding-top: 12px;
            border-radius: 11px;
            background-color: green;
            position: relative;
            -webkit-animation-name: example; /* Chrome, Safari, Opera */
            -webkit-animation-duration: 10s; /* Chrome, Safari, Opera */
            -webkit-animation-iteration-count: 3; /* Chrome, Safari, Opera */
            -webkit-animation-direction: reverse; /* Chrome, Safari, Opera */
            animation-name: example;
            animation-duration: 40s;
            animation-iteration-count: 3;
            animation-direction: reverse;
        }

        /* Chrome, Safari, Opera */
        @-webkit-keyframes example {
            0% {
                background-color: lemonchiffon;
                left: 200px;
                top: 0px;
            }

            25% {
                background-color: yellow;
                left: 200px;
                top: 0px;
            }

            50% {
                background-color: blue;
                left: 600px;
                top: 200px;
            }

            75% {
                background-color: green;
                left: 0px;
                top: 200px;
            }

            100% {
                background-color: whitesmoke;
                left: 200px;
                top: 0px;
            }
        }

        /* Standard syntax */
        @keyframes example {
            0% {
                background-color: lemonchiffon;
                left: 200px;
                top: 0px;
            }

            25% {
                background-color: yellow;
                left: 200px;
                top: 120px;
            }

            50% {
                background-color: blue;
                left: 600px;
                top: 200px;
            }

            75% {
                background-color: green;
                left: 150px;
                top: 200px;
            }

            100% {
                background-color: whitesmoke;
                left: 200px;
                top: 0px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="form-horizontal" role="form">
        <div class="row">
            <div class="panel panel-info" align="center">
                <marquee scrollamount="5" width="50">&lt;&lt;&lt;</marquee>
                <span style="font-weight: bold; color: chocolate; font-size: 15px;">...Welcome to PNF...</span>
                <marquee scrollamount="5" direction="right" width="50">&gt;&gt;&gt;</marquee>
            </div>
        </div>
          <div class="row">

             <div class="col-lg-6">
                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                       <b> Water Pump Sales summary of Last Three Months</b> 
                        </div>
                        <div class="panel-body">
                            <div id="pump_sales_summary_piechart" style="width: 100%; height: 100%;">
                            </div>
                        </div>
                    </div>
                     
                </div>
               <div class="col-lg-6">
                   
                    <div class="panel panel-default">
                        <div class="panel-heading">
                          <b>Kitchen War Sales summary of Last Three Months</b> 
                        </div>
                        <div class="panel-body">
                            <div id="Kitchen_War_sales_summary_piechart" style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                      
                </div>
            
        </div>
          <div class="row">

             <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                          <b>Comparative Group Wise sales summary of Last Three Months</b> 
                        </div>
                        <div class="panel-body">
                            <div id="Comparative_sales_summary_piechart" style="width: 100%; height: 100%;"></div>
                        </div>
                    </div>
                </div>
               <div class="col-lg-6">
                     <div class="panel panel-default">
                        <div class="panel-heading">
                           Group Wise Stock summary
                        </div>
                        <div class="panel-body">
                            <div id="piechart" style="width: 100%; height: 100%;">
                            </div>
                        </div>
                    </div>
                </div>
          
        </div>
        
       
    </div>
    
     <script>

         // VISUALIZATION API AND THE PIE CHART PACKAGE.

         google.load("visualization", "1", { packages: ["corechart"] });

         google.setOnLoadCallback(drawData);

         function drawData() {
             var options = {
                 title: '',
                 colors: ['#888', 'orange','black'],
                 colors: ['#e0440e', '#f3b49f', '#ec8f6e', '#e6693e', '#f6c7b6'],
                 is3D: true
             };
             $.ajax({
                 type: "POST",
                 url: "Default.aspx/PumpSellSummaryList",
                 data: "{}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function(data) {

                     var arrValues = [['Month', 'TotalQuantity']]; // DEFINE AN ARRAY.
                     var iCnt = 0;

                     $.each(data.d, function() {

                         // POPULATE ARRAY WITH THE EXTRACTED DATA.
                         arrValues.push([data.d[iCnt].Month, data.d[iCnt].TotalQuantity]);
                         iCnt += 1;

                     });

                     // CREATE A DataTable AND ADD THE ARRAY (WITH DATA) IN IT.
                     var figures = google.visualization.arrayToDataTable(arrValues);
                     var piechart = new google.visualization.PieChart(document.getElementById('pump_sales_summary_piechart'));
                     piechart.draw(figures, options);
                   
                 },
                 error: function(XMLHttpRequest, textStatus, errorThrown) {
                     alert('Got an Error');
                 }
             });


             $.ajax({
                 type: "POST",
                 url: "Default.aspx/KitchenWarSellSummaryList",
                 data: "{}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function(data) {

                     var arrValues = [['Month', 'TotalQuantity']]; // DEFINE AN ARRAY.
                     var iCnt = 0;

                     $.each(data.d, function() {

                         // POPULATE ARRAY WITH THE EXTRACTED DATA.
                         arrValues.push([data.d[iCnt].Month, data.d[iCnt].TotalQuantity]);
                         iCnt += 1;

                     });

                     // CREATE A DataTable AND ADD THE ARRAY (WITH DATA) IN IT.
                     var figures = google.visualization.arrayToDataTable(arrValues);
                     var piechart = new google.visualization.PieChart(document.getElementById('Kitchen_War_sales_summary_piechart'));
                     piechart.draw(figures, options);

                 },
                 error: function(XMLHttpRequest, textStatus, errorThrown) {
                     alert('Got an Error');
                 }
             });

             var options1 = {
                 title: 'sales summary of Last Three Months',
                 chartArea: { width: '50%' },
                 colors: ['#b0120a', '#ffab91', 'black'],
                 hAxis: {
                     title: 'Total Quantity',
                     minValue: 0
                 },
                 vAxis: {
                     title: 'Months'
                 }
             };

             $.ajax({
                 type: "POST",
                 url: "Default.aspx/GroupWiseSellSummaryList",
                 data: "{}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (data) {

                     var arrValues1 = [['Month', 'Pump Quantity', 'Kitchen Quantity']]; // DEFINE AN ARRAY.
                     var iCnt = 0;

                     $.each(data.d, function () {
                         arrValues1.push([data.d[iCnt].Month, data.d[iCnt].PumpQuantity, data.d[iCnt].KitchenQuantity]);
                         iCnt += 1;

                     });
                     var figures = google.visualization.arrayToDataTable(arrValues1);
                     var chart = new google.visualization.BarChart(document.getElementById('Comparative_sales_summary_piechart'));
                     chart.draw(figures, options1);

                 },
                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     alert('Got an Error');
                 }
             });


             $.ajax({
                 type: "POST",
                 url: "Default.aspx/StockSummaryList",
                 data: "{}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function(data) {

                     var arrValues = [['ProductGroupName', 'TotalQuantity']]; // DEFINE AN ARRAY.
                     var iCnt = 0;

                     $.each(data.d, function() {

                         // POPULATE ARRAY WITH THE EXTRACTED DATA.
                         arrValues.push([data.d[iCnt].ProductGroupName, data.d[iCnt].TotalQuantity]);
                         iCnt += 1;

                     });

                     // CREATE A DataTable AND ADD THE ARRAY (WITH DATA) IN IT.
                     var figures1 = google.visualization.arrayToDataTable(arrValues);
                     var chart1 = new google.visualization.PieChart(document.getElementById('piechart'));
                     chart1.draw(figures1, options);
                  
                 },
                 error: function(XMLHttpRequest, textStatus, errorThrown) {
                     alert('Got an Error');
                 }
             });
            
        
         }

         $(window).resize(function () {
             console.log("resize");
             drawData();
         });
</script>
</asp:Content>
