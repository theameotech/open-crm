var app = angular.module('acApp')
    .service('inboxService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var inboxService = {};

        inboxService.sendMessage = function (messageModel) {
            return $http.post(baseUrl + '/api/inbox/SendMessage', messageModel);
        };

        inboxService.getEmails = function ( userId, companyId) {
            return $http.get(baseUrl + '/api/inbox/GetEmails?userId='+ userId +'&companyId='+companyId);
        }
        return inboxService;

    }])