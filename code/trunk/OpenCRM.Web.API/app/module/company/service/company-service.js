var app = angular.module('acApp')
    .service('companyService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var companyService = {};

        companyService.addCompany = function (companyModel) {
            return $http.post(baseUrl + '/api/company/AddCompany', companyModel);
        };

        return companyService;

    }])