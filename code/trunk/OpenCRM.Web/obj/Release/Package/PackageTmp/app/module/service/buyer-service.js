var app = angular.module('acApp').service('buyerService', ['$http', 'baseUrl', function ($http, baseUrl) {

    var buyerService = {};

    buyerService.getMostActive = function (interval, intervalType, page, pageSize) {

        return $http.get(baseUrl + '/api/buyers/GetMostActive?timeInterval=' + interval
            + '&intervalType=' + intervalType + '&page=' + page + '&pageSize=' + pageSize);
    };
    
    buyerService.updatebuyer = function (currentBuyer) {
        return $http.post(baseUrl + '/api/buyers/UpdateBuyer', currentBuyer);
    };


    buyerService.getvehicleinfo = function (buyerid) {
      
        return $http.get(baseUrl + '/api/buyers/GetVehicleInfoBuyer?buyerId=' + buyerid);
    };

    return buyerService;

}])