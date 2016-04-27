var app = angular.module('acApp')
    .service('doListService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var doListService = {};

        doListService.adddolist = function (doListModel) {
            return $http.post(baseUrl + '/api/dolist/AddDoList', doListModel);
        };

        doListService.getdoList = function () {
            return $http.get(baseUrl + '/api/dolist/GetAllList');
        }
        doListService.getListById= function (dolistId) {
            return $http.get(baseUrl + '/api/dolist/GetDoList?dolistId=' + dolistId);
        }
        doListService.deletedoList = function (dolistId) {
            return $http.get(baseUrl + '/api/dolist/DeleteDolist?dolistId=' + dolistId);
        }
        doListService.IsRead = function (dolistId) {
            return $http.get(baseUrl + '/api/dolist/IsRead?dolistId=' + dolistId);
        }
        doListService.CompleteTask = function (dolistId,isCompleted) {
            return $http.get(baseUrl + '/api/dolist/CompleteTask?dolistId=' + dolistId);
        }

        return doListService;

    }])