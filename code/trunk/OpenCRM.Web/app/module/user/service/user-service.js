var app = angular.module('acApp')
    .service('userService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var userService = {
            createUser : function (currentBuyer) {
                return $http.post(baseUrl + '/api/admin/user/AddUser', currentBuyer);
           },
            getUser: function () {
                return $http.get(baseUrl + '/api/admin/user/All');
            },
            getRoles: function () {
                return $http.get(baseUrl + '/api/roles/All');
            },
            getUserById: function (userId) {
                return $http.get(baseUrl + '/api/admin/user/GetUser?userId=' + userId);
            },
            getUserRoles: function (userId) {
                return $http.get(baseUrl + '/api/admin/user/GetUserRole?userId=' + userId);
            },

            deleteUser: function (userId) {
                return $http.get(baseUrl + '/api/admin/user/DeleteUser?userId=' + userId);
            },
            getCountries: function () {
                return $http.get(baseUrl + '/api/country/GetAll');
            },
        };
        return userService;

    }]);