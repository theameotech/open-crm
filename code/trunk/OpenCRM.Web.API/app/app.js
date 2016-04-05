
var app = angular.module('acApp', ['ngRoute', 'ui.bootstrap', 'angular-growl', 'blockUI', 'ngCookies', 'ui-rangeSlider', 'daterangepicker', 'ngCookies', 'ngMask']);

//app.value("baseUrl", 'http://auction.ameotech.com');

app.value("baseUrl", 'http://localhost:26159');

app.config(['growlProvider', function (growlProvider) {
    growlProvider.globalTimeToLive(5000);
}]);


app.run(['$rootScope', '$cookieStore', 'loginService', '$http', '$location', 'buyerService', '$rootScope', 'userService',
    function ($rootScope, $cookieStore, loginService, $http, $location, buyerService, $rootScope, userService) {


        $rootScope.globals = $cookieStore.get('globals') || ''
        $cookieStore.IsAdmin = $cookieStore.get('IsAdmin');
        $rootScope.IsAdmin = $cookieStore.get('IsAdmin');
        //$cookieStore.userName = $cookieStore.get('UserName');
        $cookieStore.FirstName = $cookieStore.get('FirstName');
        $cookieStore.LastName = $cookieStore.get('LastName');
        $cookieStore.UserId = $cookieStore.get('UserId');
        $cookieStore.CompanyId = $cookieStore.get('CompanyId');
        $cookieStore.CreateTime = $cookieStore.get('CreateTime');

        //$rootScope.userName = $cookieStore.userName;
        $rootScope.FirstName = $cookieStore.FirstName;
        $rootScope.LastName = $cookieStore.LastName;
        $rootScope.UserId = $cookieStore.UserId;
        $rootScope.CompanyId = $cookieStore.CompanyId;
        $rootScope.CreateTime = $cookieStore.CreateTime;

        $rootScope.Logout = function () {
            $rootScope.shownavbar = false;
            $rootScope.TabBar = false;
            loginService.logout({ AuthToken: $rootScope.globals })
            .then(function () {

                $rootScope.globals = ''
                $cookieStore.remove('globals');
                $location.path("login");
            });
        };


        var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
        var mediumRegex = new RegExp("^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})");


        //$rootScope.passwordStrength = {
        //    "float": "left",
        //    "width": "100px",
        //    "height": "13px",
        //    "margin-left": "5px",
        //    "margin-left": "5px",
        //    "text-align": "center",

        //};
        $rootScope.passwordStrength = {
           
        };


        $rootScope.analyze = function (value) {
            if (strongRegex.test(value)) {
                $rootScope.passwordStrength["color"] = "green";
                $rootScope.status = "Strong";

            } else if (mediumRegex.test(value)) {
                $rootScope.passwordStrength["color"] = "orange";
                $rootScope.status = "Medium";
            } else {
                $rootScope.passwordStrength["color"] = "red";
                $rootScope.status = "Weak";
            }
        };



        $rootScope.DateHelper = {
            SelectedDate: { startDate: new Date(), endDate: new Date() },
        };


        if ($rootScope.globals != "") {
            $http.defaults.headers.common['Authorization'] = $rootScope.globals; // jshint ignore:line
            $rootScope.Auth = true;
            $rootScope.shownavbar = true;
            $rootScope.TabBar = true;
            if ($.inArray($location.path(), ['/login', '/forgotpassword']) !== -1)
                $location.path("dashboard");

            loadSearchParam();
        }
        else {
            $rootScope.shownavbar = false;
            $location.path("login");
        }


        $rootScope.AssignRoles = [];
        $rootScope.GetRoles = function () {
            userService.getUserRoles($rootScope.UserId)
                .then(function (response) {
                    $rootScope.AssignRoles = response.data;
                });
        };

        if ($rootScope.UserId > 0)
        {
            $rootScope.GetRoles();
        }
 

        $rootScope.UsersList = [];
        $rootScope.GetAllUsers = function () {
            userService.getUser()
                .then(function (response) {
                    $rootScope.UsersList = response.data;

                });
        };
        $rootScope.GetAllUsers();




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
            var restrictedPage = $.inArray($location.path(), ['/login', '/forgotpassword', '/registercompany',
                '/leadregister', '/sellerregister', '/Leadlogin', '/companylogin', '/sellerlogin']) === -1;
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


        .when('/registercompany', {
            templateUrl: 'module/company/views/register.html',
            controller: 'company-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Register User";
                }
            }
        })

           .when('/companylogin', {
               templateUrl: 'module/register/views/loginregister.html',
               controller: 'register-controller',
               resolve: {
                   setPageTitle: function ($rootScope) {
                       $rootScope.PageTitle = " Login";
                   }
               }
           })
         .when('/leadregister', {
             templateUrl: 'module/leadregister/views/leadregister.html',
             controller: 'leadregister-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Register Lead";
                 }
             }
         })

         .when('/Leadlogin', {
             templateUrl: 'module/leadregister/views/loginlead.html',
             controller: 'leadregister-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "login ";
                 }
             }
         })
         .when('/sellerregister', {
             templateUrl: 'module/sellerregister/views/sellerregister.html',
             controller: 'sellerregister-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Register Seller";
                 }
             }
         })

         .when('/sellerlogin', {
             templateUrl: 'module/sellerregister/views/sellerregister.html',
             controller: 'sellerregister-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Login";
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
         .when('/inbox', {
             templateUrl: 'module/inbox/views/inbox.html',
             controller: 'inbox-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Inbox";
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
        .when('/admin/user/userprofile/:userId', {
            templateUrl: 'module/user/views/userprofile.html',
            controller: 'user-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "User Profile";
                }
            }
        })



        .when('/sales', {
            templateUrl: 'module/sales/views/sales.html',
            controller: 'auction-show-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Auction List";
                }
            }
        })
        .when('/finance', {
            templateUrl: 'module/finance/views/finance.html',
            controller: 'auction-show-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Auction List";
                }
            }
        })
        .when('/accounting', {
            templateUrl: 'module/accounting/views/accounting.html',
            controller: 'auction-show-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Auction List";
                }
            }
        })
        .when('/arbitration', {
            templateUrl: 'module/arbitration/views/arbitration.html',
            controller: 'auction-show-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Auction List";
                }
            }
        })
        .when('/transportation', {
            templateUrl: 'module/transportation/views/transportation.html',
            controller: 'auction-show-controller',
            resolve: {
                setPageTitle: function ($rootScope) {
                    $rootScope.PageTitle = "Auction List";
                }
            }
        })
         .when('/marketing', {
             templateUrl: 'module/marketing/views/marketing.html',
             controller: 'auction-show-controller',
             resolve: {
                 setPageTitle: function ($rootScope) {
                     $rootScope.PageTitle = "Auction List";
                 }
             }
         })
    .otherwise({ redirectTo: '/dashboard' });
}]);



