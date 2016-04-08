
var app = angular.module('acApp').controller('inbox-controller',
    ['$scope', 'userService', 'inboxService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, inboxService, $rootScope, $routeParams, $interval) {
    $scope.inboxeModel = {
        CompanyID: $rootScope.CompanyId,
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
     
        $scope.deletecounter = 0;
        inboxService.getEmails($scope.inboxeModel.UserID, $scope.inboxeModel.CompanyID)
            .then(function (response) {
                angular.forEach(response.data, function (item) {
                    if (item.IsTrash == true) {
                        $scope.deletecounter++;

                    }
                    if (item.IsDraft == false && item.IsTrash == false) {
                        
                        $scope.InboxList.push(item);
                        $scope.counter++;
                        console.log($scope.InboxList);
                    }

                    console.log($scope.InboxList);
                });
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





