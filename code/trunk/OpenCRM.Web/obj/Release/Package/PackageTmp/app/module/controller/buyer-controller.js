
var app = angular.module('acApp').controller('buyer-controller',
    ['$scope', 'buyerService', '$uibModal', 'growl', function ($scope, buyerService, $uibModal, growl) {

        $scope.Intervals = [{ Name: 'Last 24 hrs', IntervalType: 'DAY' },
            { Name: 'Last One Week', IntervalType: 'WEEK' },
            { Name: 'Last One Month', IntervalType: 'MONTH' },
            { Name: 'Last One Year', IntervalType: 'YEAR' }];

        $scope.CurrentPage = 1;
        $scope.PageSize = 10;

        $scope.PageChanged = function () {
            $scope.GetAllBuyers();
        };

        $scope.Interval = $scope.Intervals[0];

        $scope.MostActiveBuyers = [];

        $scope.GetAllBuyers = function () {
            
            buyerService.getMostActive(1, $scope.Interval.IntervalType, $scope.CurrentPage, $scope.PageSize)
                    .success(function (response) {
                        $scope.MostActiveBuyers = response.Records;
                        $scope.TotalItems = response.TotalCount - 1;
                        console.log($scope.MostActiveBuyers);
                    });
        };

        $scope.OnIntervalChange = function () {
            $scope.GetAllBuyers();
        };

        var modalInstance;
        $scope.Close = function () {
            modalInstance.close();
        };

        $scope.OkButtonClicked = function () {
            $scope.CurrentBuyer.ContactFirstName = angular.copy($scope.BuyerContact.FirstName);
            $scope.CurrentBuyer.ContactLastName = angular.copy($scope.BuyerContact.LastName);
            $scope.CurrentBuyer.ContactEmail = angular.copy($scope.BuyerContact.Email);
            $scope.CurrentBuyer.ContactPhone = angular.copy($scope.BuyerContact.Phone);

            buyerService.updatebuyer($scope.CurrentBuyer)
            .success(function (response) {
                growl.success('Buyer has been updated!');
            }, function () {
                growl.error('Something went wrong, please try again later.');
            });
            modalInstance.close();
        };

        $scope.BuyerContact = {
            FirstName: "",
            LastName: "",
            Email: "",
            Phone: ""
        };

        $scope.OpenBuyerEditPopup = function (buyer) {
            $scope.CurrentBuyer = buyer;
            $scope.BuyerContact.FirstName = buyer.ContactFirstName;
            $scope.BuyerContact.LastName = buyer.ContactLastName;
            $scope.BuyerContact.Email = buyer.ContactEmail;
            $scope.BuyerContact.Phone = buyer.ContactPhone;

            modalInstance = $uibModal.open({
                templateUrl: 'app/templates/buyermodelpop.html',
                scope: $scope,
                size: 'sm',
                resolve: {
                    Context: function () {
                        return {

                        };
                    }
                }
            });
        }
        $scope.GetAllBuyers();
    }]);



