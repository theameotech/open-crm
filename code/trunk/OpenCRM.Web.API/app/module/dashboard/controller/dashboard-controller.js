
var app = angular.module('acApp').controller('dashboard-controller',
    ['$scope', 'userService', 'messageService', 'inboxService', 'companyService', '$rootScope', '$routeParams', '$uibModal','doListService','$q',
function ($scope, userService, messageService, inboxService, companyService, $rootScope, $routeParams, $uibModal, doListService, $q) {
    var defered = $q.defer();
    var promise = defered.promise;
  

    $rootScope.Ismaster = false;
    $scope.Isblock = false;
    $rootScope.Authaside = true;
    $rootScope.SellerSideBar = false;
    $rootScope.MasterSideBar = false;
    $rootScope.BuyerSideBar = false;

    if ($rootScope.UserPrivilege == "Master") {
        $rootScope.Ismaster = true;
        $rootScope.MasterSideBar = true;
        $rootScope.Authaside = false;
        $rootScope.SellerSideBar = false;
        $rootScope.MasterSideBar = false;
    }

    if ($rootScope.UserPrivilege === " Auction  Buyer") {
       
        $rootScope.Authaside = false;
        $rootScope.MasterSideBar = false;
        $rootScope.BuyerSideBar = true;
        $rootScope.SellerSideBar = false;
    }
    if ($rootScope.UserPrivilege === "Auction seller") {
     
        $rootScope.Authaside = false;
        $rootScope.MasterSideBar = false;
        $rootScope.BuyerSideBar = false;
        $rootScope.SellerSideBar = true;
    }

    $scope.DoListId = 0;
    if ($routeParams.dolistId !== undefined)
        $scope.DoListId = $routeParams.dolistId;



    $scope.Dolistmodel = {
        UserID: $rootScope.UserId,
        CompanyID: $rootScope.CompanyId,
        ListPriority: "",
        ListMessage: "",
        ListCategory: "",
        IsRead: 0,
        IsDelete: 0,
        IsActive: 0,
        IsAcheived: 0,
        IsCompleted: 0,
         TargetDate:""

    };


    $scope.inboxeModel = {
        CompanyID: $rootScope.CompanyId,
        UserID: $rootScope.UserId,
        IsRead: 0,
        IsDraft: 0,
        IsTrash: 0,
        IsReply: 0,
        IsFlagged: 0,
        IsForwarded: 0,
        IsAttachment: 0,
        EmailSubject: "",
        EmailContent: "",
        EmailSender: $rootScope.EmailId,
        EmailRecipient: "",
        AttachmentName: "test",
        AttachmentType: "test",
        FromUserName: $rootScope.FirstName,
        SystemDate: new Date()
    };

    $scope.User = {
        UserName: "",
        UserPassword: "",
        UserEmail: "",
        UserPhone: "",
        FirstName: "",
        LastName: "",
        UserOfficePhoneExt: "",
        UserAddress: "",
        UserAlternateAddress: "",
        UserCity: "",
        UserCountry: "",
        UserState: "",
        UserZipCode: "",
        Isblock: 0,
        IsVerify: 0,
        IsActive: 0,
        Gender: "",
        UserPrivilege: "Master"
    };
   
    var userModel = {
        User: {},
        Roles: [],
        CompanyName: ""
    };



    $scope.SendEmail = function () {
        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.inboxeModel.EmailContent = "";
                    $scope.inboxeModel.EmailSubject = "";
                    $scope.inboxeModel.EmailRecipient = "";
                })
    };


    $scope.CreateMasterUser = function () {
        //    if ($.fn.validateForceFully($("#formID")) == true) {
        userModel.User = $scope.User;

        userModel.CompanyName = $scope.User.CompanyName;
        userModel.User.CompanyID = $rootScope.CompanyId;
        userService.createUser(userModel)
            .then(function (response) {
                if (response.data.Success) {
                    if ($scope.UserId > 0) {
                        growl.success('all updates has been saved.');
                    }
                    else {
                        growl.success('new user has been created.');
                    }
                    // $location.path("admin/user");
                }
                else {
                    $scope.Error = response.data.Message;
                }
            }, function (err) {
                $scope.Error = "We are unable to create user at this time, Please try again later.";
            });
        // }
    };


$scope.UsersList = [];
$scope.GetAllUsers = function (data) {
    $scope.UsersList = data;
     $scope.GetAllCompany();
    //userService.getUser()
    //    .then(function (response) {
    //        $scope.UsersList = response.data;
    //        $scope.GetAllCompany();
           
     
    //    });
};

$scope.GetAllCompanyList = [];

$scope.GetAllCompany = function () {
    companyService.getAllCompany()
        .then(function (response) {
            $scope.GetAllCompanyList = response.data;
        });

};


$scope.BlockCompany = function (id) {

    companyService.blockCompany(id)
    .then(function (response) {

    })

};
$scope.GetDoList = function () {
    doListService.getListById($scope.DoListId)
        .then(function (response) {
            $scope.Dolistmodel = response.data;

        });
};
$scope.CreateDolist = function () {
    doListService.adddolist($scope.Dolistmodel)
        .then(function (response) {
            if (response.data.Success) {
                $location.path('dashboard');
            }
            else {
                $scope.Error = response.data.Message;
            }
        }, function (err) {
            $scope.Error = "We are unable to create ToDo List at this time, Please try again later.";
        });
}
$scope.Messagelist = [];
$scope.DoList = [];
$scope.GetAlllist = function (data) {
    $scope.Messagelist = [];
    $scope.DoList = [];
    $rootScope.counter = 0;
    var date = new Date().toJSON().slice(0, 10);

    $scope.DoList = data.data;
        angular.forEach($scope.DoList.data, function (item) {
            var currentdate = item.CreateDate.slice(0, 10);
            if (currentdate == date) {
                $rootScope.counter++;
                $scope.Messagelist.push(item);
            }
        });
        //.then(function (response) {
        //    $scope.DoList = response.data;
        //    angular.forEach($scope.DoList, function (item) {
        //        var currentdate = item.CreateDate.slice(0, 10);
        //        if (currentdate == date) {
        //            $rootScope.counter++;
        //            $scope.Messagelist.push(item);
        //        }
        //    });
        //});
};

$scope.DeleteDoList = function (Id) {
    if (confirm("Are you sure?")) {
        doListService.deletedoList(Id)
          .then(function (response) {
              $scope.GetAlllist();
          })
    }
};


$scope.IsRead = function (Id) {
    $scope.showlistmassage = {};
    $scope.Priority = {};
    $scope.ListCreateDate = {};
    doListService.IsRead(Id)
          .then(function (response) {
              $scope.showlistmassage = response.data.ListMessage;
              $scope.Priority = response.data.ListPriority;
              $scope.ListCreateDate = response.data.CreateDate;

          })
    modalInstance = $uibModal.open({
        templateUrl: 'module/dashboard/views/ToDoList.html',
        scope: $scope,
        size: 'sm',
        resolve: {
            Context: function () {
                return {

                };
            }
        }
    });
};

$scope.CompleteTask = function (Id) {
    doListService.CompleteTask(Id)
          .then(function (response) {
                  $scope.GetAlllist();
              
          })
};

$scope.cancel = function () {
    modalInstance.dismiss('cancel');
};

    //$scope.GetAllUsers();
if ($scope.DoListId > 0) {
    $scope.GetDoList();
}

//$scope.GetCountries = function (data) {
    //$scope.Countries = data;
    //userService.getCountries()
    //    .then(function (response) {
    //        $scope.Countries = response.data;
    //    });
//};

$scope.UnblockCompany = function (id) {

    companyService.unblockCompany(id)
    .then(function (response) {

    })

};

$scope.today = function () {
    $scope.Dolistmodel.TargetDate = new Date();
};

$scope.today();

    // Disable weekend selection
$scope.disabled = function (date, mode) {
    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
};

$scope.toggleMin = function () {
    $scope.minDate = $scope.minDate ? null : new Date();
};

$scope.toggleMin();

$scope.maxDate = new Date(2020, 5, 22);

$scope.open1 = function () {
    $scope.popup1.opened = true;
};

$scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
$scope.format = $scope.formats[0];
$scope.altInputFormats = ['M!/d!/yyyy'];

$scope.popup1 = {
    opened: false
};
$scope.OnPageLoad = function () {
    var promises = [/*userService.getCountries(),*/userService.getUser(),doListService.getdoList()];
    $q.all(promises).then(function (response) {
        response // [array of response]
        /* $scope.GetCountries(response[0]);*/
        $scope.GetAllUsers(response[0]);
        $scope.GetAlllist(response[1]);
      
    
       
    });
}


$scope.OnPageLoad();
//promise.then($scope.GetAllUsers()).then($scope.GetCountries());
}]);





