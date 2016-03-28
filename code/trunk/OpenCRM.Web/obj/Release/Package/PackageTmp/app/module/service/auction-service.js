var app = angular.module('acApp')
    .service('auctionService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var auctionService = {};

        auctionService.postBids = function (data) {
            return $http.post(baseUrl + '/api/auction/PostBids', data);
        };

        return auctionService;

    }])