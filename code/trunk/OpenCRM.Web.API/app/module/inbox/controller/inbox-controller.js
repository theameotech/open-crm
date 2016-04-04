
var app = angular.module('acApp').controller('inbox-controller',
    ['$scope', 'userService', 'inboxService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, inboxService, $rootScope, $routeParams, $interval) {
    $scope.inboxeModel = {
        CompanyID: 1,
        UserID: $rootScope.UserId,
      };

    $scope.InboxList = [];
    //$scope.SendMessage = function ($event) {
    //    var keycode = ($event.keyCode ? $event.keyCode : $event.which);
    //    if (keycode == 13) {

    //        messageService.sendMessage($scope.messageModel)
    //                .then(function (response) {
    //                    $scope.messageModel.MessageBody = "";
    //                })
    //    }
    //};

   
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





