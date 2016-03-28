var app = angular.module('acApp', ['ngRoute', 'ui.bootstrap', 'angular-growl']);

app.value("baseUrl", 'http://localhost:7597');

app.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(5000);
}]);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'app/templates/auction.html',
        controller: 'auctionController'
    })
         .when('/buyers', {
                templateUrl: 'app/templates/buyer.html',
                controller: 'buyer-controller'
         })
    .when('/buyerinfo/:id', {
        templateUrl: 'app/templates/buyerinfo.html',
        controller: 'bids-controller'
    })
     
     //.otherwise({ redirectTo: '/' });
}]);



