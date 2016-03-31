﻿var app = angular.module('acApp').controller('company-controller',
    ['$scope', 'growl', 'companyService', 'userService','$location',
function ($scope, growl, companyService, userService, $location) {
    $scope.ConfirmepasswordStatus = false;
    $scope.CompanyModel = {
        CompanyName: "",
        BusinessEmail: "",
        CompanyType: "",
        CompanyAdmin: "",
        AdminPassword: "",
        CompanyPhone: "",
        CompanyAddress: "",
        CompanyCity: "",
        CompanyState: "",
        CompanyZipCode: "",
        CompanyCountry: "",
        ConfirmPassword:""
    };

    $scope.Confirmpassword = function () {
        if ($scope.CompanyModel.AdminPassword !== $scope.CompanyModel.ConfirmPassword) {
            $scope.ConfirmepasswordStatus = true;
        }
        else {
            $scope.ConfirmepasswordStatus = false;
        }
    };
    $scope.ConfirmpasswordChanged = function () {
        $scope.ConfirmepasswordStatus = false;
    };

    $scope.Test = function () {

        $scope.fval = Math.floor(10 * Math.random())
        $scope.sval = Math.floor($scope.fval * Math.random())

        localStorage.setItem("f", $scope.fval);
        localStorage.setItem("s", $scope.sval);
    }


    $scope.CreateCompany = function () {
        var f = localStorage.getItem("f");
        var s = localStorage.getItem("s");
        var entervalue = $scope.valu;

        if ($.fn.validateForceFully($("#registerform")) == true) {
         

            if ((parseInt(entervalue) + parseInt(s)) === parseInt(f)) {

                companyService.addCompany($scope.CompanyModel)
                        .then(function (response) {
                            console.log(response);
                            if (response.data.Success) {
                                growl.success(response.data.Message);
                            }
                            else {
                                growl.error(response.data.Message);
                            }
                        }, function (err) {
                            $scope.Error = "We are unable to create user at this time, Please try again later.";
                        });

            }
            else {
                console.log("user invalid hai");
            }

            //if ($.fn.validateForceFully($("#registerform")) == true) {

        }

    };
    $scope.Countries = [];
    $scope.GetCountries = function () {
        userService.getCountries()
            .then(function (response) {
                $scope.Countries = response.data;
            });
    };

    $scope.GetCountries();

    $scope.Test();
}]);





