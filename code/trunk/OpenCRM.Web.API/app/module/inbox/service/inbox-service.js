var app = angular.module('acApp')
    .service('inboxService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var inboxService = {};

        inboxService.sendEmail = function (inboxModel) {
            return $http.post(baseUrl + '/api/inbox/SendEmail', inboxModel);
        };

        inboxService.getEmails = function (userId, companyId) {
            return $http.get(baseUrl + '/api/inbox/GetEmails?userId=' + userId + '&companyId=' + companyId);
        }

        inboxService.flagMail = function (updateIsFlag) {
            return $http.post(baseUrl + '/api/inbox/FlagEmail', updateIsFlag);
        }

        inboxService.readMail = function (updateIsFlag) {
            return $http.post(baseUrl + '/api/inbox/ReadEmail', updateIsFlag);
        }


        inboxService.deleteEmail = function (deletemail) {
            console.log(deletemail);
            return $http.post(baseUrl + '/api/inbox/DeleteEmails', deletemail);
        }

        inboxService.getEmailBySenderId = function (senderId) {
            return $http.get(baseUrl + '/api/inbox/GetEmailBySenderId?senderId=' + senderId);
        }
        return inboxService;

    }])