
var app = angular.module('acApp').controller('buyer-controller',
    ['$scope', '$rootScope', 'buyerService', '$uibModal', 'growl', '$location', 'blockUI', 'baseUrl', '$cookieStore', '$routeParams',
function ($scope, $rootScope, buyerService, $uibModal, growl, $location, blockUI, baseUrl, $cookieStore, $routeParams) {
    $scope.BuyerId = 0;
    if ($routeParams.buyerId !== undefined)
        $scope.BuyerId = $routeParams.buyerId;
    $scope.Intervals = [{ Name: 'Last 24 hrs', IntervalType: 'DAY' },
        { Name: 'Last One Week', IntervalType: 'WEEK' },
        { Name: 'Last One Month', IntervalType: 'MONTH' },
        { Name: 'Last One Year', IntervalType: 'YEAR' }];

    $scope.ShowRecords = false;
    $scope.showMessage = true;
    $scope.TotalItems = 0;
    $scope.CurrentPage = 1;
    $scope.PageSize = 50;
    //$scope.CurrentBuyer = {};

    $scope.PageChanged = function () {
        $scope.GetAllBuyers();
    };
    $scope.Interval = $scope.Intervals[0];
    $scope.SearchText = "";
    $scope.MostActiveBuyers = [];

    function toDate(dateInput) {
        var monthNames = [
          "January", "February", "March",
          "April", "May", "June", "July",
          "August", "September", "October",
          "November", "December"
        ];

        var date = new Date(dateInput);
        var day = date.getDate();
        var monthIndex = date.getMonth();
        var year = date.getFullYear();
        return day + ' ' + monthNames[monthIndex] + ' ' + year;
    }


    $scope.GetBuyer = function () {
        buyerService.getBuyerById($scope.BuyerId)
            .then(function (response) {
                $scope.BuyerContact = response.data;
                console.log($scope.BuyerContact);
            }
            )};
    if ($scope.BuyerId > 0) {
        $scope.GetBuyer();
    }
   
  

    $scope.GetAllBuyers = function (updatePagination) {
        var blockUIContainer = blockUI.instances.get('block-buyer-form');
        blockUIContainer.start();

        if ($scope.SearchText == undefined)
            $scope.SearchText = "";

        var minDate = new Date();
        var maxDate = new Date();

        if ($rootScope.DateHelper.SelectedDate.startDate._d !== undefined) {
            var minDate = toDate($rootScope.DateHelper.SelectedDate.startDate._d);
            var maxDate = toDate($rootScope.DateHelper.SelectedDate.endDate._d);
        } else {
            var minDate = toDate($rootScope.DateHelper.SelectedDate.startDate);
            var maxDate = toDate($rootScope.DateHelper.SelectedDate.endDate);
        }

        $scope.MinDate = minDate;
        $scope.MaxDate = maxDate;
        buyerService.searchBuyers($scope.CurrentPage, $scope.PageSize, $scope.SearhParams.ModelMinValue
            , $scope.SearhParams.ModelMaxValue, $scope.SearhParams.OdoMeterMinValue, $scope.SearhParams.OdoMeterMaxValue,
            maxDate, minDate, $scope.SearchText)
                .then(function (response) {
                    blockUIContainer.stop();
                    $scope.MostActiveBuyers = response.data.Records;
                    if ($scope.TotalItems <= 0 || updatePagination) {
                        $scope.TotalItems = response.data.TotalCount;
                    }
                },
                function () {
                    blockUIContainer.stop();
                });
    };

    var dateInput = $cookieStore.getObject('search-input-date');
    if (dateInput !== undefined && dateInput != null) {
        //$rootScope.DateHelper = dateInput;
        $scope.MinDate = dateInput.startDate;
        $scope.MaxDate = dateInput.endDate;
    }
    else {
        $scope.MinDate = $rootScope.DateHelper.SelectedDate.startDate;
        $scope.MaxDate = $rootScope.DateHelper.SelectedDate.endDate;
    }

    $scope.OnIntervalChange = function () {
        $scope.GetAllBuyers();
    };

    var modalInstance;
    $scope.Close = function () {
        modalInstance.close();
    };

    $scope.GetExportToCsvLink = function () {
        return baseUrl;
    };

    $scope.OkButtonClicked = function () {
        //$scope.CurrentBuyer.ContactFirstName = angular.copy($scope.BuyerContact.FirstName);
        //$scope.CurrentBuyer.ContactLastName = angular.copy($scope.BuyerContact.LastName);
        //$scope.CurrentBuyer.BuyerEmail = angular.copy($scope.BuyerContact.Email);
        //$scope.CurrentBuyer.ContactPhone = angular.copy($scope.BuyerContact.Phone);
        //$scope.CurrentBuyer.BuyerAddress = angular.copy($scope.BuyerContact.Address);
        //$scope.CurrentBuyer.BuyerPhone = angular.copy($scope.BuyerContact.BuyerPhone);
        //$scope.CurrentBuyer.Name = angular.copy($scope.BuyerContact.BuyerName);

        if ($scope.BuyerContact.Id > 0) {
            buyerService.updatebuyer($scope.BuyerContact)
                .success(function (response) {
                    growl.success('Buyer has been updated!');
                }, function () {
                    growl.error('Something went wrong, please try again later.');
                });
        }
        else {
            buyerService.addbuyer($scope.BuyerContact)
               .success(function (response) {
                   growl.success('Buyer has been added!');
                   modalInstance.close();
               }, function () {
                   growl.error('Something went wrong, please try again later.');
               });
        }

    };

    $scope.BuyerContact = {
        Name:"",
        ContactFirstName: "",
        ContactLastName: "",
        BuyerEmail: "",
        BuyerAddress: "",
        BuyerPhone: ""
    };

    //$scope.OpenBuyerEditPopup = function (buyer) {
    //    if (buyer !== undefined) {
    //        $scope.CurrentBuyer = buyer;
    //        $scope.BuyerContact.FirstName = buyer.ContactFirstName;
    //        $scope.BuyerContact.LastName = buyer.ContactLastName;
    //        $scope.BuyerContact.Phone = buyer.ContactPhone;
    //        $scope.BuyerContact.Address = buyer.BuyerAddress;
    //        $scope.BuyerContact.BuyerPhone = buyer.BuyerPhone;
    //        $scope.BuyerContact.Email = buyer.BuyerEmail;
    //        $scope.BuyerContact.BuyerName = buyer.Name;
    //    }
    //    modalInstance = $uibModal.open({
    //        templateUrl: 'module/buyer/views/buyermodelpop.html',
    //        scope: $scope,
    //        size: 'md',
    //        resolve: {
    //            Context: function () {
    //                return {

    //                };
    //            }
    //        }
    //    });
    //};

    $scope.SearhParams = {
        OdoMeterMinValue: 0,
        OdoMeterMaxValue: 0,
        ModelMinValue: 0,
        ModelMaxValue: 0,
        MaxDate: "",
        MinDate: ""
    };

    $scope.SearhRange = {
        OdoMeterMinValue: 0,
        OdoMeterMaxValue: 0,
        ModelMinValue: 0,
        ModelMaxValue: 0,
        MaxDate: "",
        MinDate: ""
    };

    $scope.Date = { startDate: null, endDate: null };


    $scope.Vehiclebuyerinfo = [];

    $scope.getBuyerInfo = function (buyer) {
        if (buyer.HasVehicleInfodata == true) {
            buyer.HasVehicleInfodata = false;
            buyer.Loading = false;
            return;
        }
        var blockUIContainer = blockUI.instances.get('block-buyerinfo-form');
        blockUIContainer.start();
        buyer.Loading = true;

        var minDate = new Date();
        var maxDate = new Date();

        if ($rootScope.DateHelper.SelectedDate.startDate._d !== undefined) {
            var minDate = toDate($rootScope.DateHelper.SelectedDate.startDate._d);
            var maxDate = toDate($rootScope.DateHelper.SelectedDate.endDate._d);
        } else {
            var minDate = toDate($rootScope.DateHelper.SelectedDate.startDate);
            var maxDate = toDate($rootScope.DateHelper.SelectedDate.endDate);
        }

        buyerService.getvehicleinfo(buyer.Id, $scope.SearchText, $scope.SearhParams.ModelMinValue
            , $scope.SearhParams.ModelMaxValue, $scope.SearhParams.OdoMeterMinValue,
                $scope.SearhParams.OdoMeterMaxValue, minDate,
               maxDate)
                    .success(function (response) {
                        buyer.Loading = false;
                        blockUIContainer.stop();
                        buyer.Vehiclebuyerinfo = response;
                        $scope.totalrecords = response.length;
                        buyer.HasVehicleInfodata = true;
                    });
    };


    $scope.getSearchparams = function () {
        buyerService.getSearchParams()
            .success(function (response) {
                $scope.SearhParams.OdoMeterMinValue = response.OdoMeterMinValue;
                $scope.SearhParams.OdoMeterMaxValue = response.OdoMeterMaxValue;
                $scope.SearhParams.ModelMinValue = response.ModelMinValue;
                $scope.SearhParams.ModelMaxValue = response.ModelMaxValue;
                $scope.SearhParams.MaxDate = response.MaxDate;
                $scope.SearhParams.MinDate = response.MinDate;                
                $scope.SearhRange = angular.copy($scope.SearhParams);
            });
    };

    $scope.getSearchparams();

    $scope.OnFilterButtonClicked = function () {

        $scope.ShowRecords = true;
        $scope.showMessage = false;
        $scope.GetAllBuyers(true);
    };
  
}]);





