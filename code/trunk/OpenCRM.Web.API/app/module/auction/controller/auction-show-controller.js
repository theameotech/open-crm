
var app = angular.module('acApp')
    .controller('auction-show-controller', ['$scope', 'auctionService', '$location', function ($scope, auctionService, $location) {
        $scope.Auctions = [];
        $scope.GetAuctions = function () {
            auctionService.getAuctions()
                .then(function (response) {
                    $scope.Auctions = response.data;
                });
        };
        $scope.AddAuction = function () {
            $location.path('auction')
        };
        $scope.GetAuctions();
    }]);



