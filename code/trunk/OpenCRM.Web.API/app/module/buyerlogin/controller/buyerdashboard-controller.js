
var app = angular.module('acApp').controller('buyerdashboard-controller',
    ['$scope','$rootScope',
function ($scope, $rootScope) {

   
    if ($rootScope.UserPrivilege === " Auction  Buyer") {

        $rootScope.Authaside = false;
        $rootScope.MasterSideBar = false;
        $rootScope.BuyerSideBar = true;
        $rootScope.SellerSideBar = false;
    }
    
}]);





