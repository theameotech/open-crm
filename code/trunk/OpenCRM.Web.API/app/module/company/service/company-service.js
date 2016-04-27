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
            
            return $http.get(baseUrl + '/api/company/BlockCompany?companyId=' + companyId);
        };



        companyService.unblockCompany = function (companyId) {

            return $http.get(baseUrl + '/api/company/UnblockCompany?companyId=' + companyId);
        };

        companyService.getAllCompany = function () {
            return $http.get(baseUrl + '/api/company/GetAllCompany');
        };
        companyService.getCompanyByCompanyId = function (companyId) {
            return $http.get(baseUrl + '/api/company/GetCompanyByCompanyId?companyId=' + companyId);
        };

        companyService.login = function (userModel) {
            return $http.post(baseUrl + '/api/company/login', userModel);
        };

        companyService.IsVerify = function (companyId) {
            return $http.get(baseUrl + '/api/company/IsVerify?companyId=' + companyId);
        }

        return companyService;

    }])