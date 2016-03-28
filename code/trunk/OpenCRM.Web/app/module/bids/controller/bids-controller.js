
var app = angular.module('acApp')
    .controller('bids-controller',
    ['$scope', 'buyerService', 'growl', '$routeParams', 'blockUI', function ($scope, buyerService, growl, $routeParams, blockUI) {

        //$scope.bidid = $routeParams.id;
        
        //$scope.Vehiclebuyerinfo = [];
        //$scope.GetAllvechicleinfo = function () {
        //    var blockUIContainer = blockUI.instances.get('block-buyerinfo-form');
        //    blockUIContainer.start();
        //    buyerService.getvehicleinfo($scope.bidid)
        //            .success(function (response) {
        //                blockUIContainer.stop();
        //                $scope.Vehiclebuyerinfo = response;
        //                $scope.BuyerName = $scope.Vehiclebuyerinfo[0].BuyerName;
        //                $scope.BuyerAddress = $scope.Vehiclebuyerinfo[0].BuyerAddress;
        //                $scope.BuyerPhone = $scope.Vehiclebuyerinfo[0].BuyerPhone;
        //                $scope.ContactFirstName = $scope.Vehiclebuyerinfo[0].ContactFirstName;
        //                $scope.ContactLastName = $scope.Vehiclebuyerinfo[0].ContactLastName;
        //                $scope.ContactEmail = $scope.Vehiclebuyerinfo[0].ContactEmail;
        //                $scope.ContactPhone = $scope.Vehiclebuyerinfo[0].ContactPhone;
                       
        //            })
        //}
        //$scope.GetAllvechicleinfo();
    }])