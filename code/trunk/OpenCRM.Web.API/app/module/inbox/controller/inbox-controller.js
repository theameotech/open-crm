
var app = angular.module('acApp').controller('inbox-controller',
    ['$scope', 'userService', 'inboxService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, inboxService, $rootScope, $routeParams, $interval) {


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

        $scope.inboxeModel.AttachmentName = $scope.UserPhoto;
        $scope.inboxeModel.AttachmentType = $scope.filetype;
        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.inboxeModel.EmailContent = "";
                    $scope.inboxeModel.EmailSubject = "";
                    $scope.inboxeModel.EmailRecipient = "";
                })
    };


    $scope.DraftEmail = function () {

        $scope.inboxeModel.AttachmentName = $scope.UserPhoto;
        $scope.inboxeModel.AttachmentType = $scope.filetype;
        $scope.inboxeModel.IsDraft = 1;

        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.emailStatusId = response.data;
                    $scope.inboxeModel.EmailContent = "";
                    $scope.inboxeModel.EmailSubject = "";
                    $scope.inboxeModel.EmailRecipient = "";
                })
    };


    $scope.InboxList = [];
    $scope.GetAllEmails = function () {
        $scope.counter = 0;
        $scope.length = 0;
        $scope.draftvalue = 0;
        $scope.deletecounter = 0;
        inboxService.getEmails($scope.inboxeModel.UserID, $scope.inboxeModel.CompanyID)
            .then(function (response) {
                $scope.length = response.data.length;
                angular.forEach(response.data, function (item) {
                    if (item.IsTrash == true) {
                        $scope.deletecounter++;

                    }
                    if (item.IsDraft == false && item.IsTrash == false) {
                        
                        $scope.InboxList.push(item);
                        $scope.counter++;
                        console.log($scope.InboxList);
                    }

                });
                $scope.draftvalue = $scope.length - $scope.counter;
            });



    };



    $scope.FlagMail = function (inbox) {
        $scope.updateIsFlag = {
            StatusID: inbox.StatusID,
            IsFlagged: 1
        }
        inboxService.flagMail($scope.updateIsFlag)
            .then(function (response) {
            });
    };

    $scope.DeleteItems = [];

    $scope.CheckIsDelete = function (IsChecked, id, index) {


        if (IsChecked == true) {
            $scope.DeleteItems.push(id);
            console.log($scope.DeleteItems);

        }
        else {

            $scope.DeleteItems.splice(index, 1);
            console.log($scope.DeleteItems);
        }


    }
    $scope.deletevalue = {};

    $scope.DeleteEmails = function () {



        console.log($scope.deletevalue);

        inboxService.deleteEmail($scope.DeleteItems)
            .then(function (response) {
            });
    }



    $scope.GetAllEmails();
}]);







