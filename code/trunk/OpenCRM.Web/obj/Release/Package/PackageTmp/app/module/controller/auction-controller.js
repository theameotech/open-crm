
var app = angular.module('acApp')
    .controller('auctionController', ['$scope', 'auctionService', function ($scope, auctionService) {

        $scope.FileContent = "";
        $scope.AuctionName = "";

        function handleFileSelect(evt) {
            var files = evt.target.files; // FileList object

            // Loop through the FileList and render image files as thumbnails.
            for (var i = 0, f; f = files[i]; i++) {

               
                var reader = new FileReader();

                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (e) {
                        var files = document.getElementById('bidsfile').files;
                        if (!files.length) {
                            alert('Please select a file!');
                            return;
                        }

                        var file = files[0];
                        var start = 0;
                        var stop = file.size - 1;

                        var reader = new FileReader();

                        // If we use onloadend, we need to check the readyState.
                        reader.onload = function (evt) {
                            if (evt.target.readyState == FileReader.DONE) { // DONE == 2
                                $scope.FileContent = evt.target.result;
                            }
                        };

                        var blob = file.slice(start, stop + 1);
                        reader.readAsBinaryString(blob);
                    };
                })(f);

                // Read in the image file as a data URL.
                reader.readAsDataURL(f);
            }
        }

        document.getElementById('bidsfile')
            .addEventListener('change', handleFileSelect, false);

        $scope.CreateAuction = function () {
            var auction = {
                AuctionName: $scope.AuctionName,
                File: $scope.FileContent,
                AuctionDate: $scope.dt
            };

            auctionService.postBids(auction);

        };


        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();


        // Disable weekend selection
        $scope.disabled = function (date, mode) {
            return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
        };

        $scope.toggleMin = function () {
            $scope.minDate = $scope.minDate ? null : new Date();
        };

        $scope.toggleMin();
        $scope.maxDate = new Date(2020, 5, 22);

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };






        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };

    }])



