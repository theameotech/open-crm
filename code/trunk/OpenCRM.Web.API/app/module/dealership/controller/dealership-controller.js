
var app = angular.module('acApp').controller('dealership-controller',
    ['$scope', '$rootScope', 'dealershipService', '$uibModal', 'growl', '$location', 'blockUI', 'baseUrl',
        function ($scope, $rootScope, dealershipService, $uibModal, growl, $location, blockUI, baseUrl) {


            $scope.DealerShip = {
                Id: "",
                BusinessName: "",
                CompanyStructure: "",
                IndustryType: "",
                BusinessAddress: "",
                City: "",
                State: "",
                ZipCode: "",
                BusinessPhone: "",
                MonthandYearEstablished: "",
                EmailAddress: "",
                ConfirmEmailAddress: "",
                Password: "",
                IsAdmin: "",
                CompanyId: ""
            };



            $scope.AddDealerShip = function () {
                dealershipService.addDealerShip($scope.DealerShip)
                    .then(function (response) {
                         if (response.data != null && response.data != "" && response.data != undefined) {
                            
                        }
                    });

            }



        }]);





