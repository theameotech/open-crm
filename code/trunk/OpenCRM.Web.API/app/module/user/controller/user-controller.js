
var app = angular.module('acApp').controller('user-controller',
    ['$scope', 'baseUrl', '$location', '$routeParams', 'userService', 'growl', 'companyService', '$rootScope', '$q',
function ($scope, baseUrl, $location, $routeParams, userService, growl, companyService, $rootScope, $q) {

    var defered = $q.defer();
    var promise = defered.promise;

    $scope.UserId = 0;
    $scope.CompanyID = 0;
    if ($routeParams.userId !== undefined)
        $scope.UserId = $routeParams.userId;
    $scope.Error = "";
    $scope.Message = "";
    $scope.ComparepasswordStatus = false;
    $scope.CurrentUser = {};


    $scope.User = {
        UserName: "",
        UserPassword: "",
        UserEmail: "",
        UserPhone: "",
        FirstName: "",
        LastName: "",
        UserOfficePhoneExt: "",
        UserAddress: "",
        UserAlternateAddress: "",
        UserCity: "",
        UserCountry: "",
        UserOfficPhone: "",
        UserState: "",
        UserZipCode: "",
        Isblock: 0,
        IsVerify: 0,
        IsActive: 0,
        Gender: "",
        UserPrivilege: "",
        ConfirmPassword: "",
        CompanyName: ""
    };

    $scope.Roles = [];
    $scope.AssignRoles = [];
    $scope.Countries = [];

    $scope.GetUser = function (data) {
        if (data.data != null) {
            $scope.User = data.data;
            if ($scope.UserId > 0) {
                $scope.GetUserRoles();
            }
        }
    };

    $scope.GetUserRoles = function () {
        userService.getUserRoles($scope.UserId)
           .then(function (response) {
               $scope.AssignRoles = response.data;
               $scope.AssignRole();
               //$scope.GetCompanyById();
              
    });
    };



    var userModel = {
        User: {},
        Roles: [],

    };



    $scope.CreateUser = function () {
        //if ($.fn.validateForceFully($("#formID")) == true) {
        userModel.User = $scope.User;
        userModel.User.Id = $scope.UserId;
        userModel.Roles = $scope.AssignRoles;
        userModel.User.CompanyID = $rootScope.CompanyId;
        userModel.User.CompanyName = $scope.User.CompanyName;
        userService.createUser(userModel)
            .then(function (response) {
                if (response.data.Success) {
                    if ($scope.UserId > 0) {
                        growl.success('all updates has been saved.');
                    }
                    else {
                        growl.success('new user has been created.');
                    }

                    $location.path("admin/user");
                }
                else {
                    $scope.Error = response.data.Message;
                }
            }, function (err) {
                $scope.Error = "We are unable to create user at this time, Please try again later.";
            });
        //   }
    };

    $scope.GetRoles = function (data) {
        $scope.Roles = data.data;
    };

    $scope.GetCountries = function (data) {
        $scope.Countries = data.data;
    };
 

    $scope.SelectRole = function (context) {
        context.Checked = !context.Checked;
    };

    $scope.UnAssignRole = function () {
        angular.forEach($scope.AssignRoles, function (item, index) {
            if (item.Checked == true) {
                $scope.Roles.push(item);
            };
        });
        angular.forEach($scope.Roles, function (item) {
            var index = $scope.AssignRoles.indexOf(item);
            if (index > -1) {
                $scope.AssignRoles.splice(index, 1);
            }
        });
    };

    $scope.AssignRole = function () {
        angular.forEach($scope.Roles, function (item, index) {
            if (item.Checked == true) {
                item.Checked = false;
                $scope.AssignRoles.push(item);

            };
        });

        function ReadIndex(roleId) {
            var roleIndex = -1;
            angular.forEach($scope.Roles, function (item, index) {
                if (item.Id == roleId) {
                    roleIndex = index;
                }
            });

            return roleIndex;
        }

        angular.forEach($scope.AssignRoles, function (item) {
            var index = ReadIndex(item.Id);
            if (index > -1) {
                $scope.Roles.splice(index, 1);
            }
        });
    };


    $scope.UnSelectRoles = function (context) {
        context.Checked = !context.Checked;
    };


    $scope.Comparepassword = function () {
        if ($scope.User.UserPassword !== $scope.User.ConfirmPassword) {
            $scope.ComparepasswordStatus = true;
        }
        else {
            $scope.ComparepasswordStatus = false;
        }
    };
    $scope.ComparepasswordChanged = function () {
        $scope.ComparepasswordStatus = false;
    };

    $scope.GetCompanyById = function (data) {
        $scope.User.CompanyName = data.data.CompanyName;
        
        //$scope.CompanyLatest = "";
        //$scope.CompanyLatest = data.data.CompanyName;
        // = $scope.CompanyLatest;
      
        //companyService.getCompanyById($rootScope.UserId)
        //.then(function (response) {
            
        //    $scope.CompanyLatest = response.data.CompanyName;
        //    $scope.User.CompanyName = $scope.CompanyLatest;
        //})
    }

    $scope.OnLoad = function () {
        var promises = [
                        //userService.getCountries(),
                        //userService.getUserById($scope.UserId),
                        // userService.getRoles(),
                        //, companyService.getCompanyById($rootScope.UserId)
                        userService.getCountries(),
                        userService.getRoles(),
                        userService.getUserById($scope.UserId),
                       companyService.getCompanyById($rootScope.UserId)
                        
        ];

        $q.all(promises).then(function (response) {
            response // [array of response]
           //company
            //$scope.GetCountries(response[0]);
            //$scope.GetUser(response[1]);
            //$scope.GetRoles(response[2]);
            //$scope.GetCompanyById(response[3]);
            $scope.GetCountries(response[0]);
            $scope.GetRoles(response[1]);
            $scope.GetUser(response[2]);
          $scope.GetCompanyById(response[3]);
         

        });

    }

    //if ($scope.UserId === 0) {

    //    $scope.GetCompanyById()

    //}
    $scope.OnLoad();
}]);





