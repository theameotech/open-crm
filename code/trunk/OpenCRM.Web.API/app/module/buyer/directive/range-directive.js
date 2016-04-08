app.directive('ionRange', function () {
    return {
        restrict: 'EA',
        scope: {
            minValue: '=minVal',
            maxValue: '=maxVal',
            from: '=fromVal',
            to: '=toVal',
            modelMin: '=',
            modelMax: '='
        },
        link: function (scope, elem, attr) {

           

            scope.onChange = function (sliderObj) {
                var fromNumber = sliderObj.fromNumber;
                var toNumber = sliderObj.toNumber;
                scope.modelMin = fromNumber;
                scope.modelMax = toNumber;
                if (!scope.$$phase)
                    scope.$apply();
            };

            scope.$watch('maxValue', function (val) {
                if (val > 0) {
                    var min = parseInt(scope.minValue);
                    var max = parseInt(scope.maxValue);
                    var from = parseInt(scope.from);
                    var to = parseInt(scope.to);
                    $(elem).ionRangeSlider({
                        min: min,
                        max: max,
                        from: from,
                        to: to,
                        type: 'double',
                        step: 1,
                        prefix: "",
                        prettify: false,
                        hasGrid: true,
                        onFinish: scope.onChange
                    });
                }
            });

        }
    }
});