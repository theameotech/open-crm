
var app = angular.module('acApp')
    .controller('bids-controller',
    ['$scope', 'buyerService', 'growl', '$routeParams', function ($scope, buyerService, growl, $routeParams) {

        $scope.bidid = $routeParams.id;
        $scope.DisplayData =
            {
                buyerid:0,
                buyername: "",
                vechicleid: 0,
                vechiclename: "",
                vechiclemodel: "",
                vechicleobometer: "",
                vechiclevinnumber:""

            }
        ;
        $scope.Vehiclebuyerinfo = [];

        $scope.GetAllvechicleinfo = function () {
            buyerService.getvehicleinfo($scope.bidid)
                    .success(function (response) { 
                        angular.forEach(response, function (item) {
                            $scope.DisplayData.buyerid = item.Buyer.Id;
                            $scope.DisplayData.buyername = item.Buyer.Name;
                            $scope.DisplayData.vechicleid = item.Vehicle.Id;
                            $scope.DisplayData.vechiclename = item.Vehicle.Name;
                            $scope.DisplayData.vechiclemodel = item.Vehicle.Model;
                            $scope.DisplayData.vechicleobometer = item.Vehicle.OdoMeter;
                            $scope.DisplayData.vechiclevinnumber = item.Vehicle.Vin;
                        });
                        $scope.Vehiclebuyerinfo.push($scope.DisplayData);
                        console.log($scope.Vehiclebuyerinfo);
                    })
            }
        $scope.GetAllvechicleinfo();
    }]);



