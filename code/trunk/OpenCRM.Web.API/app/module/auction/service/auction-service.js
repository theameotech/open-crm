var app = angular.module('acApp')
    .service('auctionService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var auctionService = {};

        auctionService.postBids = function (data) {
            return $http.post(baseUrl + '/api/auction/PostBids', data);
        };

        auctionService.getAuctions = function () {
            return $http.get(baseUrl + '/api/auction/all');
        };

        return auctionService;

    }])