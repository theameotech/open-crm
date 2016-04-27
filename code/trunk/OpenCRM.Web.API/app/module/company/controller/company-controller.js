var app = angular.module('acApp').controller('company-controller',
    ['$scope', 'growl', 'companyService', 'userService', '$location', '$rootScope', '$cookieStore', '$http',
function ($scope, growl, companyService, userService, $location, $rootScope, $cookieStore, $http) {
    $scope.ConfirmepasswordStatus = false;

    $scope.ShowSighnUpForm = false;
   
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
        ConfirmPassword: ""
    };


    $scope.User = {
        UserName: "",
        UserPassword: "",
        UserEmail: "",
        UserPhone: "",
        UserOfficPhone:"",
        FirstName: "",
        LastName: "",
        UserOfficePhoneExt: "",
        UserAddress: "",
        UserAlternateAddress: "",
        UserCity: "",
        UserCountry: "",
        UserState: "",
        UserZipCode: "",
        CompanyName: "",
        Isblock: 0,
        IsVerify: 0,
        IsActive: 0,
        UserPrivilege:"Admin",
        Gender: "",
        CompanyId:$scope.CompanyId


    };
   
  
    $scope.CreateUser = function () {
        console.log($scope.User);
        var userModel = {
            User: $scope.User,
            Roles: [],
            CompanyName: ""
        };
        userService.createUser(userModel)
            .then(function (response) {
                if (response.data.Success) {
                    if ($scope.UserId > 0) {
                        growl.success('all updates has been saved.');
                    }
                    else {
                        growl.success('new user has been created.');
                    }

                   // $location.path("admin/user");
                }
                else {
                    $scope.Error = response.data.Message;
                }
            }, function (err) {
                $scope.Error = "We are unable to create user at this time, Please try again later.";
            });
        // }
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
                console.log(" invalid user ");
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

    $scope.cancel = function () {
        $location.path('login')
    };
    $scope.Company = {};
    $scope.GetCompanyById = function () {
        companyService.getCompanyById($rootScope.UserId)
        .then(function (response) {
            $scope.Company = response.data.CompanyName;
            $scope.name = response.data.CompanyName.slice(0,1);
        })
    }
    if ($rootScope.UserId > 0)
    {
        $scope.GetCompanyById();
    }



    $scope.UserModel = {
        CompanyAdmin: "",
        AdminPassword: ""
    }
  

    $scope.Login = function () {
        companyService.login($scope.UserModel)
                 .then(function (response) {
                     if (response.data.Success == true && response.data.UserName == $scope.UserModel.CompanyAdmin 
                         && response.data.Password == $scope.UserModel.AdminPassword) {
                         $scope.ShowSighnUpForm = true;
                         $scope.User.CompanyName = response.data.CompanyName;
                         $scope.User.UserName = response.data.UserName;
                         $scope.User.UserPassword = response.data.Password;
                         $scope.User.UserEmail = response.data.BussinessEmail;
                         $scope.User.CompanyId = response.data.CompanyId;
                         $scope.IsRead();
                         }
                         else {
                             growl.error('Invalid username and password.');
                         }
                 
                 });
    }

    $scope.GetCountries = function () {
        userService.getCountries()
            .then(function (response) {
                $scope.Countries = response.data;
            });
    };


    $scope.IsRead = function () {
        $scope.CompanyId = $scope.User.CompanyId;
        companyService.IsVerify($scope.CompanyId)
              .then(function (response) {

              })
    }
   
        $scope.GetCountries();

    //$scope.GetCountries();

    $scope.Test();
}]);





