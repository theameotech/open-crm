
var app = angular.module('acApp', ['ngRoute', 'ui.bootstrap', 'angular-growl', 'blockUI', 'ngCookies', 'ui-rangeSlider', 'daterangepicker', 'ngCookies', 'ngMask']);

//app.value("baseUrl", 'http://auction.ameotech.com');

app.value("baseUrl", 'http://localhost:26159');

app.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(5000);
}]);

app.run(['$rootScope', '$cookieStore', 'loginService', '$http', '$location', 'buyerService','$rootScope','userService',
    function ($rootScope, $cookieStore, loginService, $http, $location, buyerService, $rootScope, userService) {
               
      
        $rootScope.globals = $cookieStore.get('globals') || ''
        $cookieStore.IsAdmin = $cookieStore.get('IsAdmin');
        $rootScope.IsAdmin = $cookieStore.get('IsAdmin');
        //$cookieStore.userName = $cookieStore.get('UserName');
        $cookieStore.FirstName = $cookieStore.get('FirstName');
        $cookieStore.LastName = $cookieStore.get('LastName');
        $cookieStore.UserId = $cookieStore.get('UserId');
        $cookieStore.CompanyId = $cookieStore.get('CompanyId');
        //$rootScope.userName = $cookieStore.userName;
        $rootScope.FirstName = $cookieStore.FirstName;
        $rootScope.LastName = $cookieStore.LastName;
        $rootScope.UserId = $cookieStore.UserId;
        $rootScope.CompanyId = $cookieStore.CompanyId;

        $rootScope.Logout = function () {
            $rootScope.shownavbar = false;
            loginService.logout({ AuthToken: $rootScope.globals })
            .then(function () {
             
                $rootScope.globals = ''
                $cookieStore.remove('globals');
                $location.path("login");
            });
        };

        $rootScope.DateHelper = {
            SelectedDate: { startDate: new Date(), endDate: new Date() },
        };


        if ($rootScope.globals != "") {
            $http.defaults.headers.common['Authorization'] = $rootScope.globals; // jshint ignore:line
            $rootScope.Auth = true;
            $rootScope.shownavbar = true;          
            if ($.inArray($location.path(), ['/login', '/forgotpassword']) !== -1)
                $location.path("dashboard");

            loadSearchParam();
        }
        else {
            $rootScope.shownavbar = false;
            $location.path("login");
        }



        function loadSearchParam() {

            buyerService.getSearchParams()
                      .success(function (response) {
                          $rootScope.DateHelper.SelectedDate.startDate = response.MinDate;
                          $rootScope.DateHelper.SelectedDate.endDate = response.MaxDate;
                          $cookieStore.putObject('search-input-date', angular.copy($rootScope.DateHelper));

                      });
        }
        $rootScope.$on('on-login', function () {
            loadSearchParam();
        });

        $rootScope.$on('$routeChangeStart', function (event, next, current) {
            var restrictedPage = $.inArray($location.path(), ['/login', '/forgotpassword']) === -1;
            $rootScope.globals = $cookieStore.get('globals') || ''
            if (restrictedPage && $rootScope.globals == "") {
                $rootScope.Auth = false;
                $rootScope.shownavbar = true;
                $location.path('login');
            }
        });
    }]);

app.config(['$routeProvider', 'blockUIConfig', 'growlProvider', function ($routeProvider, blockUIConfig, growlProvider) {

    blockUIConfig.message = 'Please wait...';
    blockUIConfig.autoBlock = false;

    growlProvider.globalDisableCountDown(true);

    $routeProvider
        .when('/dashboard', {
            templateUrl: 'module/dashboard/views/dashboard.html',
            controller: 'dashboard-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Dashboard";
                }
            }
        })
        .when('/login', {
            templateUrl: 'module/login/views/login.html',
        controller: 'login-controller',
        resolve: {
            setPageTitle: function ($rootScope) {
                $rootScope.PageTitle = "Login";
            }
        }
    })
        .when('/auction', {
            templateUrl: 'module/auction/views/auction.html',
            controller: 'auctionController',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Add Auctions";
                }
            }
        })
         .when('/leads', {
             templateUrl: 'module/buyer/views/buyer.html',
             controller: 'buyer-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "leads";
                 }
             }
         })

        .when('/Company', {
            templateUrl: 'module/company/views/company.html',
            controller: 'company-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Company Profile";
                }
            }
        })

         .when('/buyerinfo/:id', {
             templateUrl: 'module/buyer/views/buyerinfo.html',
             controller: 'bids-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Buyers Info";
                 }
             }
         })
           .when('/vehicleinfo/:id', {
               templateUrl: 'module/vehicle/views/vehicleinfo.html',
               controller: 'vehicle-controller',
               resolve: {
                   setPageTitle: function ($rootScope) {
                       $rootScope.PageTitle = "Auction Vehicle Info";
                   }
               }
           })
          .when('/bidsinfo/:id', {
              templateUrl: 'module/bids/views/bidsinfo.html',
              controller: 'vehicle-controller',
              resolve: {
                  setPageTitle: function ($rootScope) {
                      $rootScope.PageTitle = "Vehicle Bids Info";
                  }
              }
          })
          .when('/chat/:id', {
              templateUrl: 'module/dashboard/views/chat.html',
              controller: 'chat-controller',
              resolve: {
                  setPageTitle: function ($rootScope) {
                      $rootScope.PageTitle = "chat";
                  }
              }
          })
         .when('/auctionlist', {
             templateUrl: 'module/auction/views/auctionlist.html',
             controller: 'auction-show-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Auction List";
                 }
             }
         })

        .when('/admin/user', {
            templateUrl: 'module/user/views/userlist.html',
            controller: 'userprofile-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Users";
                }
            }
        })
          .when('/admin/user/createuser', {
              templateUrl: 'module/user/views/createuser.html',
              controller: 'user-controller',
              resolve: {
                  setPageTitle: function ($rootScope) {
                      $rootScope.PageTitle = "Create User";
                  }
              }
          })
         .when('/admin/user/createuser/:userId', {
             templateUrl: 'module/user/views/createuser.html',
             controller: 'user-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Edit User";
                 }
             }
         })
    .otherwise({ redirectTo: '/dashboard' });
}]);



