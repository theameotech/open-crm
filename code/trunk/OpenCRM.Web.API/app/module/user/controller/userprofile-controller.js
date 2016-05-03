
var app = angular.module('acApp').controller('userprofile-controller',
    ['$scope', 'baseUrl', 'userService', 'growl',
function ($scope, baseUrl, userService, growl) {
    $scope.IsMasterShow = false;

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

    $scope.Counts = {};
    $scope.userPrivilege={};
    $scope.GetAllUsers = function () {
          $scope.UsersList = [];
        userService.getUser()
            .then(function (response) {
               // $scope.Counts = response.data.length;
                angular.forEach(response.data, function (item) {
                    if (item.UserPrivilege !== "Master") {
                        $scope.UsersList.push(item);
                        $scope.Counts = $scope.UsersList.length;
                    } 
                });




                // console.log($scope.UsersList);
                //   $scope.totalUsers = response.data.length;
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





