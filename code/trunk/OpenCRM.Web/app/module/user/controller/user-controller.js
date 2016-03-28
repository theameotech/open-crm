
var app = angular.module('acApp').controller('user-controller',
    ['$scope', 'baseUrl', '$location', '$routeParams', 'userService', 'growl',
function ($scope, baseUrl, $location, $routeParams, userService, growl) {
    $scope.UserId = 0;
    if ($routeParams.userId !== undefined)
        $scope.UserId = $routeParams.userId;
    $scope.Error = "";
    $scope.Message = "";
    $scope.ComparepasswordStatus = false;
    $scope.CurrentUser = {};
    $scope.User = {
        UserName: "",
        Password: "",
        Email: "",
        Phone: "",
        FirstName: "",
        LastName: "",
        Office: "",
        StreetAddress1: "",
        StreetAddress2: "",
        City: "",
        State: "",
        PostalCode: "",
        Country: "",
        ConfirmPassword: "",
        EXT: "",
        CompanyName: ""


    };

    $scope.Roles = [];
    $scope.AssignRoles = [];
    $scope.Countries = [];

    $scope.GetUser = function () {
        userService.getUserById($scope.UserId)
            .then(function (response) {
                $scope.User = response.data;
                if ($scope.UserId > 0) {
                    $scope.GetUserRoles();
                }
            });
    };

    $scope.GetUserRoles = function () {
        userService.getUserRoles($scope.UserId)
            .then(function (response) {
                $scope.AssignRoles = response.data;
                $scope.AssignRole();
            });
    };

    if ($scope.UserId > 0) {
        $scope.GetUser();
    }
    var userModel = {
        User: {},
        Roles: [],
        CompanyName: ""
    };



    $scope.CreateUser = function () {
        if ($.fn.validateForceFully($("#formID")) == true) {
            userModel.User = $scope.User;
            userModel.User.Id = $scope.UserId;
            userModel.Roles = $scope.AssignRoles;
            userModel.CompanyName = $scope.User.CompanyName;
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
        }
    };

    $scope.GetRoles = function () {
        userService.getRoles()
            .then(function (response) {
                $scope.Roles = response.data;
            });
    };

    $scope.GetCountries = function () {
        userService.getCountries()
            .then(function (response) {
                $scope.Countries = response.data;
            });
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
        if ($scope.User.Password !== $scope.User.ConfirmPassword) {
            $scope.ComparepasswordStatus = true;
        }
        else {
            $scope.ComparepasswordStatus = false;
        }
    };
    $scope.ComparepasswordChanged = function () {
        $scope.ComparepasswordStatus = false;
    };

    $scope.GetCountries();
    $scope.GetRoles();

}]);





