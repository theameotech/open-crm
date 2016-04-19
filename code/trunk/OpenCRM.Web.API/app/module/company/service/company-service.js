var app = angular.module('acApp')
    .service('companyService', ['$http', 'baseUrl', function ($http, baseUrl) {

        var companyService = {};

        companyService.addCompany = function (companyModel) {
            return $http.post(baseUrl + '/api/company/AddCompany', companyModel);
        };
        companyService.getCompanyById = function (userId) {
            return $http.get(baseUrl + '/api/company/GetCompanieById?userId='+userId);
        };

        companyService.blockCompany = function (companyId) {
            alert(companyId);
            return $http.get(baseUrl + '/api/company/BlockCompany?companyId=' + companyId);
        };


        companyService.getAllCompany = function (userId) {
            return $http.get(baseUrl + '/api/company/GetAllCompany');
        };

        return companyService;

    }])