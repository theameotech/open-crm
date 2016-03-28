
var app = angular.module('acApp').controller('userprofile-controller',
    ['$scope', 'baseUrl', 'userService', 'growl',
function ($scope, baseUrl, userService, growl) {
    $scope.User = {
        UserName: "",
        Password: "",
        Email: "",
        Address: "",
        Phone: ""
    };
    $scope.TotalItems = 0;
    $scope.CurrentPage = 1;
    $scope.PageSize = 50;


    $scope.PageChanged = function () {
        $scope.GetAllUsers();
    };
    $scope.UsersList = [];
    $scope.GetAllUsers = function () {
        userService.getUser()
            .then(function (response) {
                $scope.UsersList = response.data;
                $scope.totalUsers = response.data.length;
            });
    };
    $scope.DeleteUser = function (Id) {
        if (confirm("Are you sure?")) {
            userService.deleteUser(Id)
              .then(function (response) {
                  $scope.GetAllUsers();
              })
        }
    };

}]);





