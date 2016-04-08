
var app = angular.module('acApp')
    .controller('auction-show-controller', ['$scope', 'auctionService', '$location', function ($scope, auctionService, $location) {

        $scope.TotalItems = 0;
        //$scope.CurrentPage = 1;
        //$scope.PageSize = 10;

        $scope.Pages = [{ Text: '10', val: 10 }, { Text: '25', val: 25 }, { Text: '50', val: 50 }, { Text: 'All', val: 999999 }];
        //$scope.PageChanged = function () {
        //    $scope.GetAuctions();
        //};
        $scope.itemsPerPage=10;
        $scope.currentPage = 1;
        $scope.phItems = [];
        $scope.OnpageChanged = function () {
            var itemPerPage = 10;
            if ($scope.itemsPerPage == 999999) {
                $scope.currentPage = 1;
                itemPerPage = $scope.Auctions.length - 1;
            }
            itemPerPage = parseInt($scope.itemsPerPage);
            var from = ($scope.currentPage - 1) * itemPerPage + 1;
            var to = from + itemPerPage - 1;

            //Split array on basis of from and to then assign values to $scope.phItems
            $scope.phItems = $scope.Auctions.slice((from - 1), to);
            $scope.totalItems = $scope.Auctions.length - 1;
        };

        $scope.Auctions = [];
        $scope.GetAuctions = function () {
            auctionService.getAuctions()
                .then(function (response) {
                    $scope.Auctions = response.data;
                    $scope.OnpageChanged();
                });
        };
        $scope.AddAuction = function () {
            $location.path('auction')
        };
        $scope.GetAuctions();
     
    }]);



