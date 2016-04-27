var app = angular.module('acApp')
    .filter('markdown', function () {
    return function (input) {
        if (input == null || input === undefined || input === '')
            return "";

        return input.substring(0, 80) + "...";
    };
})