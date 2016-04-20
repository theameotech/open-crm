
var app = angular.module('acApp').controller('dashboard-controller',
    ['$scope', 'userService', 'messageService', 'inboxService', 'companyService', '$rootScope', '$routeParams', '$uibModal','doListService',
function ($scope, userService, messageService, inboxService, companyService, $rootScope, $routeParams, $uibModal, doListService) {

    $rootScope.Ismaster = false;

    if ($rootScope.UserPrivilege == "Master") {
        $rootScope.Ismaster = true;
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
        IsCompleted:0

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

   
   

    $scope.SendEmail = function () {
        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.inboxeModel.EmailContent = "";
                    $scope.inboxeModel.EmailSubject = "";
                    $scope.inboxeModel.EmailRecipient = "";
                })
    };





$scope.UsersList = [];
$scope.GetAllUsers = function () {
    userService.getUser()
        .then(function (response) {
            $scope.UsersList = response.data;
            $scope.GetAllCompany();
           
     
        });
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
$scope.GetAlllist = function () {
    $scope.Messagelist = [];
    $scope.DoList = [];
    $rootScope.counter = 0;
    var date = new Date().toJSON().slice(0, 10);
    doListService.getdoList()
        .then(function (response) {
            $scope.DoList = response.data;
            angular.forEach($scope.DoList, function (item) {
                var currentdate = item.CreateDate.slice(0, 10);
                if (currentdate == date) {
                    $rootScope.counter++;
                    $scope.Messagelist.push(item);
                }
            });
        });
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
$scope.GetAlllist();
    //$scope.GetAllUsers();
if ($scope.DoListId > 0) {
    $scope.GetDoList();
}



$scope.UnblockCompany = function (id) {

    companyService.unblockCompany(id)
    .then(function (response) {

    })

};

$scope.GetAllUsers();
}]);





