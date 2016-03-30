var app = angular.module('acApp').controller('company-controller',
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



    $scope.CreateCompany = function () {
        if ($.fn.validateForceFully($("#registerform")) == true) {
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
    };
    $scope.Countries = [];
    $scope.GetCountries = function () {
        userService.getCountries()
            .then(function (response) {
                $scope.Countries = response.data;
            });
    };

    $scope.GetCountries();
}]);





