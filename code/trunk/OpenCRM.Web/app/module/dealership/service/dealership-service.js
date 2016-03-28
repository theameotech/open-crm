var app = angular.module('acApp').service('dealershipService', ['$http', 'baseUrl', function ($http, baseUrl) {

    var dealershipService = {};

    dealershipService.addDealerShip = function (addDealerShip) {

        return $http.post(baseUrl + '/api/dealership/adddealership', addDealerShip);
    };

    return buyerService;

}])