var app = angular.module('acApp').service('buyerService', ['$http', 'baseUrl', function ($http, baseUrl) {

    var buyerService = {};

    buyerService.getMostActive = function (interval, intervalType, page, pageSize) {

        return $http.get(baseUrl + '/api/buyers/GetMostActive?timeInterval=' + interval
            + '&intervalType=' + intervalType + '&page=' + page + '&pageSize=' + pageSize);
    };

    buyerService.searchBuyers = function (page, pageSize, modelmin, modelmax,
        odometermin, odometermax,maxDate,minDate, searchtext) {

        return $http.get(baseUrl + '/api/buyers/Search?page=' + page
            + '&pageSize=' + pageSize + '&searchText=' + searchtext
            + '&modelMin=' + modelmin
            + '&modelMax=' + modelmax
            + '&odometeMin=' + odometermin
            + '&odomoterMax=' + odometermax
            + '&maxDate=' + maxDate
            + '&minDate=' + minDate);
    };

    buyerService.updatebuyer = function (currentBuyer) {
        return $http.post(baseUrl + '/api/buyers/UpdateBuyer', currentBuyer);
    };

    buyerService.addbuyer = function (currentBuyer) {
        return $http.post(baseUrl + '/api/buyers/CreateBuyer', currentBuyer);
    };

    buyerService.getvehicleinfo = function (buyerid, searchtext, modelmin, modelmax,
        odometermin, odometermax, minDate, maxDate) {

        return $http.get(baseUrl + '/api/buyers/GetVehicleInfoBuyer?buyerId=' + buyerid
            + '&searchText=' + searchtext
            + '&modelMin=' + modelmin
            + '&modelMax=' + modelmax
            + '&odometeMin=' + odometermin
            + '&odomoterMax=' + odometermax
            + '&maxDate=' + maxDate
            + '&minDate=' + minDate);
    };
    buyerService.getvehicleinfobyauction = function (auctionid, page, pageSize) {

        return $http.get(baseUrl + '/api/buyers/GetVehicleInfoByAuction?auctionid=' + auctionid
            + '&page=' + page + '&pageSize=' + pageSize);
    };
    buyerService.getallbidsinfo = function (vehicleid, page, pageSize) {

        return $http.get(baseUrl + '/api/buyers/GetBidsInfoByVehicle?vehicleid=' + vehicleid
            + '&page=' + page + '&pageSize=' + pageSize);
    };
    buyerService.getSearchParams = function () {
        return $http.get(baseUrl + '/api/buyers/GetSearchParams');
    }
    return buyerService;

}])