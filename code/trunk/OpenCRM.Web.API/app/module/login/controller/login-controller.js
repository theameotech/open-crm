
var app = angular.module('acApp')
    .controller('login-controller', ['$scope', '$rootScope', 'growl', 'loginService', '$location', '$http', '$cookieStore',
function ($scope, $rootScope, growl, loginService, $location, $http, $cookieStore) {
    $rootScope.Auth = false;
    $scope.UserModel = {
        UserName: "",
        Password: ""
    }

    $scope.Login = function () {
        if ($.fn.validateForceFully($("#LoginID")) == true) {
            loginService.login($scope.UserModel)
                 .then(function (response) {
                     if (response.data.Success) {
                         $rootScope.GetAllUsers();
                         $cookieStore.put('globals', response.data.Token);
                         $cookieStore.put('IsAdmin', response.data.IsAdmin);
                         //$cookieStore.put('UserName', $scope.UserModel.UserName);
                         $cookieStore.put('FirstName', response.data.FirstName);
                         $cookieStore.put('LastName', response.data.LastName);
                         $cookieStore.put('UserId', response.data.UserId);
                         $cookieStore.put('CompanyId', response.data.CompanyId);
                         $cookieStore.put('CreateTime', response.data.CreateTime);
                        
                         $rootScope.globals = response.data.Token;
                         $rootScope.IsAdmin = response.data.IsAdmin;
                         //$rootScope.userName = $scope.UserModel.UserName;
                         $rootScope.FirstName = response.data.FirstName;
                         $rootScope.LastName = response.data.LastName;
                         $rootScope.UserId = response.data.UserId;
                         $rootScope.CompanyId = response.data.CompanyId;
                         $rootScope.CreateTime = response.data.CreateTime;
                         $rootScope.Auth = true;
                         $rootScope.shownavbar = true;
                         $rootScope.showdasboard = false;
                         $rootScope.GetRoles();
                         $http.defaults.headers.common['Authorization'] = response.data.Token;
                         $rootScope.$broadcast('on-login', {});
                         $location.path('dashboard');
                     }
                     else {
                         growl.error('Invalid username and password.');
                     }
                 });
        }
    }
}])



