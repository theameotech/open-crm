var app = angular.module('acApp')
    .service('loginService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var loginService = {};

        loginService.login = function (userModel) {
            return $http.post(baseUrl + '/api/user/login', userModel);
        };

        loginService.logout = function (userModel) {
            return $http.post(baseUrl + '/api/user/logout', userModel);
        };

        return loginService;

    }])