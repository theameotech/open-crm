var app = angular.module('acApp')
    .service('inboxService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var inboxService = {};

        inboxService.sendEmail = function (inboxModel) {
            return $http.post(baseUrl + '/api/inbox/SendEmail', inboxModel);
        };

        inboxService.getEmails = function (userId, companyId) {
            return $http.get(baseUrl + '/api/inbox/GetEmails?userId=' + userId + '&companyId=' + companyId);
        }
        return inboxService;

    }])