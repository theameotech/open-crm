
var app = angular.module('acApp').controller('draft-controller',
    ['$scope', 'userService', 'inboxService', '$rootScope', '$routeParams', '$interval',
function ($scope, userService, inboxService, $rootScope, $routeParams, $interval) {


   
    $scope.draftModel = {
        CompanyID: $rootScope.CompanyId,
        UserID: $rootScope.UserId,
    };


    $scope.DraftList = [];
    $scope.GetAllDraftEmails = function () {
        $scope.counter = 0;
        $scope.length = 0;
        $scope.draftvalue = 0;
        inboxService.getEmails($scope.draftModel.UserID, $scope.draftModel.CompanyID)
            .then(function (response) {
                $scope.length = response.data.length;
                angular.forEach(response.data, function (item) {
                    if (item.IsDraft == true) {
                        $scope.DraftList.push(item);
                        $scope.counter++;
                        console.log($scope.DraftList);
                    }

                });
                $scope.draftvalue = $scope.length - $scope.counter;
            });
    };

    $scope.GetAllDraftEmails();
}]);







