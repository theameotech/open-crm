var app = angular.module('acApp')
    .service('userService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var userService = {
            createUser : function (currentUser) {
                return $http.post(baseUrl + '/api/admin/user/AddUser', currentUser);
            },
            updatePassword: function (currentBuyer) {
                console.log(currentBuyer);
                return $http.post(baseUrl + '/api/admin/user/UpdatePassword', currentBuyer);
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
                return $http.get(baseUrl + '/api/lookup/GetAll');
            },
        };
        return userService;

    }]);