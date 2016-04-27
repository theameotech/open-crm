
var app = angular.module('acApp')
    .controller('forgotpassword-controller', ['$scope', 'userService', 'growl','$rootScope',
function ($scope, userService, growl, $rootScope) {


    //var userModel = {
    //    User: {},
    //    Roles: [],
    //    CompanyName: ""
    //};

    $scope.Userpass = {
        Password: "",
        UserName: ""
    };

    $scope.UpdatePassword = function () {
       
       
        userService.updatePassword($scope.Userpass)
                .then(function (response) {
                    if (response.data.Success) {
                        $scope.Message = response.data.Message;
                    }
                    else {
                        $scope.Error = response.data.Message;
                    }
                }, function (err) {
                    $scope.Error = "We are unable to create user at this time, Please try again later.";
                });
    }

    $scope.generate = function () {
        var chars = "abcdefghijklmnopqrstuvwxyz!@#$%^&*()-+<>ABCDEFGHIJKLMNOP1234567890";
        var pass = "";
        for (var x = 0; x < 10; x++) {
            var i = Math.floor(Math.random() * chars.length);
            pass += chars.charAt(i);
        }

        $scope.Userpass.Password = pass;
        //$scope.Password = pass;
        $scope.UpdatePassword();
        //myform.row_password.value = randomPassword(myform.length.value);
    }

    //};

}])



