
var app = angular.module('acApp').controller('chat-controller',
    ['$scope', 'userService', 'messageService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, messageService, $rootScope, $routeParams, $interval) {
    $scope.messageModel = {
        CompanyID: $rootScope.CompanyId,
        UserID: $rootScope.UserId,
        ToUserID: $routeParams.id,
        UserName: $rootScope.FirstName,
        MessageBody: ""
    };

    $scope.MessageList = [];
    $scope.SendMessage = function ($event) {
        var keycode = ($event.keyCode ? $event.keyCode : $event.which);
        if (keycode == 13) {

            messageService.sendMessage($scope.messageModel)
                    .then(function (response) {
                        $scope.messageModel.MessageBody = "";
                    })
        }
    };

    $scope.SendMessageOnButtonClick = function () {
        messageService.sendMessage($scope.messageModel)
                .then(function (response) {
                    $scope.messageModel.MessageBody = "";
                })

    };
    $scope.GetAllMessages = function () {
        var lastMessageId = 0;
        if ($scope.MessageList.length > 0) {
            lastMessageId = $scope.MessageList[$scope.MessageList.length - 1].MessageID;
        }
        messageService.getMessages(lastMessageId, $scope.messageModel.UserID, $scope.messageModel.ToUserID)
            .then(function (response) {
                angular.forEach(response.data, function (item) {
                    $scope.MessageList.push(item);
                });
            });
    };

    var getMessageInterval = $interval(function () { $scope.GetAllMessages(); }, 2000);

    $scope.stopInterval = function () {
        if (angular.isDefined(getMessageInterval)) {
            $interval.cancel(getMessageInterval);
            getMessageInterval = undefined;
        }
    };

    $scope.$on('$destroy', function () {
        // Make sure that the interval is destroyed too
        $scope.stopInterval();
    });




    $scope.UsersList = [];
    $scope.GetAllUsers = function () {
        userService.getUser()
            .then(function (response) {
                $scope.UsersList = response.data;
            });
    };
    $scope.GetAllUsers();
    $scope.GetAllMessages();
}]);





