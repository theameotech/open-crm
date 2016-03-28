var app = angular.module('acApp')
    .service('messageService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var messageService = {};

        messageService.sendMessage = function (messageModel) {
            return $http.post(baseUrl + '/api/message/SendMessage', messageModel);
        };

        messageService.getMessages = function (lastMessageId, userId, toUserId) {
            return $http.get(baseUrl + '/api/message/GetMessages?lastMessageId=' + lastMessageId + '&userId=' + userId + '&toUserId=' + toUserId);
        }
        return messageService;

    }])