
var app = angular.module('acApp')
    .controller('vehicle-controller',
    ['$scope', 'buyerService', 'growl', '$routeParams', 'blockUI', function ($scope, buyerService, growl, $routeParams, blockUI) {
        $scope.auctionid = $routeParams.id;
        $scope.Bidsinfo = [];
        $scope.Vehicleauctioninfo = [];
        $scope.TotalItems = 0;
        $scope.CurrentPage = 1;
        $scope.PageSize = 50;

        $scope.PageChanged = function () {
            $scope.GetAllvechicleinfobyauction();
        };

        //used for pagination on bidsinfo
        $scope.OnPageChanged = function () {
            $scope.GetAllbidsinfo();
        };

      
        $scope.GetAllvechicleinfobyauction = function () {
            var blockUIContainer = blockUI.instances.get('block-vehcle-form');
            blockUIContainer.start();
            buyerService.getvehicleinfobyauction($scope.auctionid, $scope.CurrentPage, $scope.PageSize)
                    .success(function (response) {
                        blockUIContainer.stop();
                        $scope.Vehicleauctioninfo = response.Records;
                        if ($scope.TotalItems <= 0) {
                            // $scope.TotalItems = response.TotalCount -1;
                            $scope.TotalItems = response.TotalCount;
                        }
                    })
        };

        $scope.GetAllbidsinfo = function () {
            var blockUIContainer = blockUI.instances.get('block-bids-form');
            blockUIContainer.start();
            buyerService.getallbidsinfo($scope.auctionid, $scope.CurrentPage, $scope.PageSize)
                    .success(function (response) {
                        blockUIContainer.stop();
                        $scope.Bidsinfo = response.Records;
                        $scope.Name = $scope.Bidsinfo[0].Name;
                        $scope.Vin = $scope.Bidsinfo[0].Vin;
                        $scope.Odometer = $scope.Bidsinfo[0].Odometer;
                        $scope.Model = $scope.Bidsinfo[0].Model;
                        if ($scope.TotalItems <= 0) {
                            //$scope.TotalItems = response.TotalCount - 1;
                            $scope.TotalItems = response.TotalCount;
                        }
                    })
        };
    }]);



