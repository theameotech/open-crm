
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
        $scope.UserPhoto="";
        $scope.filetype="";
       $scope.filetype=input.files[0].type;
      
       
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#photo-id').attr(e.target.result);

                var base64Image = e.target.result;
              
                if ($scope.filetype == "text/plain")
                {
                    $scope.UserPhoto = base64Image.replace('data:text/plain;base64,', '');
                }
                else if ($scope.filetype == "text/html")
                {
                    $scope.UserPhoto = base64Image.replace('data:text/html;base64,', '');
                }

                else if ($scope.filetype == "image/png")
                {
                    $scope.UserPhoto = base64Image.replace('data:image/png;base64,', '');
                }
                 console.log($scope.UserPhoto);
            }              
            reader.readAsDataURL(input.files[0]);
        }
    };


    $scope.SendEmail = function () {

        $scope.inboxeModel.AttachmentName = $scope.UserPhoto;
       // $scope.inboxeModel.AttachmentType = $scope.filetype;

        console.log($scope.inboxeModel);
      
        inboxService.sendEmail($scope.inboxeModel)
                .then(function (response) {
                    $scope.inboxeModel.EmailContent = "";
                    $scope.inboxeModel.EmailSubject = "";
                    $scope.inboxeModel.EmailRecipient = "";
                })

    };

    $scope.InboxList = [];
    $scope.GetAllEmails = function () {

        inboxService.getEmails($scope.inboxeModel.UserID, $scope.inboxeModel.CompanyID)
            .then(function (response) {
                angular.forEach(response.data, function (item) {
                    $scope.InboxList.push(item);

                    console.log($scope.InboxList);
                });
            });
    };


    $scope.GetAllEmails();
}]);







