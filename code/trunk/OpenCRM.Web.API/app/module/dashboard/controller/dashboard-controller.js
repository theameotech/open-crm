
var app = angular.module('acApp').controller('dashboard-controller',
    ['$scope', 'userService', 'messageService', '$rootScope',
function ($scope, userService, messageService, $rootScope) {

$scope.UsersList = [];
$scope.GetAllUsers = function () {
    userService.getUser()
        .then(function (response) {
            $scope.UsersList = response.data;
     
        });
};
$scope.GetAllUsers();
}]);





