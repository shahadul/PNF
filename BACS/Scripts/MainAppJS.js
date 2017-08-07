/// <reference path="angular.js" />


(function () {
    'use strict';
    var myapp = angular.module("myapp", []);  
    myapp.config(function ($httpProvider) {
        $httpProvider.defaults.headers.post = {};
        $httpProvider.defaults.headers.post["Content-Type"] = "application/json; charset=utf-8";
    });

   
})();