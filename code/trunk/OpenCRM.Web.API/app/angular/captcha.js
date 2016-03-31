﻿var app = angular.module('acApp')

.directive('simpleCaptcha', function () {
    return {
        restrict: 'E',
        scope: { valid: '=' },
        template: '<input ng-model="a.value" ng-show="a.input" style="width:2em; text-align: center;"><span ng-hide="a.input">{{a.value}}</span>&nbsp;{{operation}}&nbsp;<input ng-model="b.value" ng-show="b.input" style="width:2em; text-align: center;"><span ng-hide="b.input">{{b.value}}</span>&nbsp;=&nbsp;{{result}}',
        controller: function ($scope, $rootScope) {

            var show = Math.random() > 0.5;

            var value = function (max) {
                return Math.floor(max * Math.random());
            };

            var int = function (str) {
                return parseInt(str, 10);
            };

            $scope.a = {
                value: show ? undefined : 1 + value(4),
                input: show
            };
            $scope.b = {
                value: !show ? undefined : 1 + value(4),
                input: !show
            };
            $scope.operation = '+';

            $scope.result = 5 + value(5);

            var a = $scope.a;
            var b = $scope.b;
            var result = $scope.result;

            var checkValidity = function () {
                if (a.value && b.value) {
                    var calc = int(a.value) + int(b.value);
                    $scope.valid = calc == result;
                    $rootScope.$broadcast("myEvent", $scope.valid);
                   
                } else {
                    $scope.valid = false;
                }
               // $scope.$apply(); // needed to solve 2 cycle delay problem;
            };


            $scope.$watch('a.value', function () {
                checkValidity();
            });

            $scope.$watch('b.value', function () {
                checkValidity();
            });



        }
    };
});