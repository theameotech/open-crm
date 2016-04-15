
var app = angular.module('acApp').controller('dashboard-controller',
    ['$scope', 'userService', 'messageService', 'inboxService', '$rootScope',
function ($scope, userService, messageService, inboxService, $rootScope) {



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
     
        });
};
$scope.GetAllUsers();
}]);





