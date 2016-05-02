
var app = angular.module('acApp').controller('read-controller',
    ['$scope', 'userService', 'inboxService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, inboxService, $rootScope, $routeParams, $interval) {

    $scope.Replycontent = false;


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
        AttachmentName: "",
        AttachmentType: "",
        FromUserName: $rootScope.FirstName,
        SystemDate: new Date()
    };



    $scope.ReadMail = function (id) {

        
        $scope.updateIsFlag = {
            StatusID: id,
            IsRead: 1
        }
        inboxService.readMail($scope.updateIsFlag)
            .then(function (response) {
            });
    };



    $scope.GetEmailBySenderId = function () {

        $scope.UserEmailSubject = "";
        $scope.UserEmailBody = "";
        $scope.SenderEmail = "";
        $scope.CreateDate = "";
        $scope.emailstatusid = 0;

        inboxService.getEmailBySenderId($routeParams.EmailID)
                    .then(function (response) {
                       
                        $scope.emailstatusid = response.data.StatusID
                        $scope.UserEmailBody = response.data.EmailContent;
                        $scope.UserEmailSubject = response.data.EmailSubject;
                        $scope.SenderEmail = response.data.EmailSender;
                        $scope.CreateDate = response.data.CreateTime;
                        $scope.ReadMail($scope.emailstatusid);
                    });
    };

    $scope.uploadFile = function (input) {
        $scope.UserPhoto = "";
        $scope.filetype = "";
        $scope.filetype = input.files[0].type;
        $scope.inboxeModel.IsAttachment = 1;

        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#photo-id').attr(e.target.result);

                var base64Image = e.target.result;

                if ($scope.filetype == "text/plain") {
                    $scope.UserPhoto = base64Image.replace('data:text/plain;base64,', '');
                }
                else if ($scope.filetype == "text/html") {
                    $scope.UserPhoto = base64Image.replace('data:text/html;base64,', '');
                }

                else if ($scope.filetype == "image/png") {
                    $scope.UserPhoto = base64Image.replace('data:image/png;base64,', '');
                }
                console.log($scope.UserPhoto);
            }
            reader.readAsDataURL(input.files[0]);
        }
    };


    $scope.SendEmail = function () {
        $scope.inboxeModel.EmailSubject = $scope.UserEmailSubject;
        $scope.inboxeModel.EmailRecipient = $scope.SenderEmail;
        $scope.inboxeModel.AttachmentName = $scope.UserPhoto;
        $scope.inboxeModel.AttachmentType = $scope.filetype;
        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.inboxeModel.EmailContent = "";
                })
    };

    $scope.DraftEmail = function () {
        $scope.inboxeModel.EmailSubject = $scope.UserEmailSubject;
        $scope.inboxeModel.EmailRecipient = $scope.SenderEmail;
        $scope.inboxeModel.AttachmentName = $scope.UserPhoto;
        $scope.inboxeModel.AttachmentType = $scope.filetype;
        $scope.inboxeModel.IsDraft = 1;

        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.emailStatusId = response.data;
                    $scope.inboxeModel.EmailContent = "";
                })
    };

    $scope.ShowReplyContent = function () {
        $scope.Replycontent = true;

    }








    $scope.GetEmailBySenderId();

}]);







