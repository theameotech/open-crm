
var app = angular.module('acApp').controller('sellerdashboard-controller',
    ['$scope', '$rootScope',
function ($scope, $rootScope) {
    if ($rootScope.UserPrivilege === "Auction seller") {

        $rootScope.Authaside = false;
        $rootScope.MasterSideBar = false;
        $rootScope.BuyerSideBar = false;
        $rootScope.SellerSideBar = true;
    }
}]);





