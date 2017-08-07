/// <reference path="../angular.js" />
var myp = angular.module('myapp');
myp.controller('ProjectPlan', ['$scope', '$http', ProjectPlanDetails]);
function ProjectPlanDetails($scope, $http) {


    var ProjectPlan;
    $scope.ProjectPlan = [];
    var ProjectPlanDetails;
    $scope.ProjectPlanDetails = [];
    var Locations;
    $scope.Locations = [];
    var Activitys;
    $scope.Activitys = [];
    var SubmitList;
    $scope.SubmitList = [];
    $scope.Remarks = "";
    var LoadLocation = function () {
        $http.post('WebService1.asmx/getLocations').success(function (data) {
            $scope.Locations = data;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer! " + data;
            $scope.loading = false;
        });
    };
    var LoadActivitys = function () {
        $http.post('WebService1.asmx/getActivities').success(function (data) {
            $scope.Activitys = data;

        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer! " + data;
            $scope.loading = false;
        });
    };
    $scope.loadddl = function () {
        LoadLocation();
        LoadActivitys();
    };

    $scope.add = function () {

        {

            $scope.ProjectPlanDetails.push(
                {
                    'ActivityID': $scope.MyActivitys.ActivityID,
                    'ActivityName': $scope.MyActivitys.ActivityName,
                    'VisitDate': $scope.VisitDate,
                    'Remarks': $scope.Remarks
                }
                );

        }
    };
    $scope.AddedBy = "";
    var details = function () {
        $scope.ProjectPlan.push(
               {
                   'ProjectTitle': $scope.ProjectTitle,
                   'LocationID': $scope.MyLocations.LocationID,
                   'AddedBy': ""

               }
               );
    }

    $scope.Submit = function () {
        details();
        $http.post('WebService1.asmx/Submit', { ProjectPlan: this.ProjectPlan, ProjectPlanDetails: this.ProjectPlanDetails }
          ).success(function (data) {
              $scope.loadddl();
              alert("Saved Successfully!!");

          }).error(function (data) {
              $scope.error = "An Error has occured while Adding Customer! " + data;
              $scope.loading = false;
          });
    };

    $scope.btnview = function () {

        $http.post('WebService1.asmx/getLocations').success(function (data) {
            alert("Added Successfully!!");
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer! " + data;
            $scope.loading = false;
        });

    };
    var key;
    $scope.edit = function (ProjectPlanDetails, index) {
        key = index;
        $scope.VisitDate = ProjectPlanDetails.VisitDate;
        $scope.MyActivitys.ActivityName = ProjectPlanDetails.ActivityName;
        $scope.MyActivitys.ActivityID = ProjectPlanDetails.ActivityID;
        $scope.Remarks = ProjectPlanDetails.Remarks;

    };
    $scope.btnupd = function (name, role, phone) {
        $scope.ProjectPlanDetails[key].name = name;
        $scope.ProjectPlanDetails[key].role = role;
        $scope.ProjectPlanDetails[key].phone = phone;
        $scope.name = '';
        $scope.role = '';
        $scope.phone = '';

    };
    $scope.del = function (role) {
        $scope.ProjectPlanDetails.splice(role, 1);
    };
}