'use strict';

app.controller('CreateChatModalController',
    ['$scope', '$rootScope', '$timeout', '$uibModalInstance', 'chatHubService',
        function ($scope, $rootScope, $timeout, $uibModalInstance, chatHubService) {
            $scope.ok = function () {
                var chat = { title: $scope.title, img: $scope.image };
                $scope.createChatBusyPromise = chatHubService.post(chat).then(function () {
                    $uibModalInstance.close();
                });
            };

            $scope.cancel = function () {
                $uibModalInstance.close();
            };

            $scope.processImage = function (selectedImage) {
                if (!selectedImage) return;
                var reader = new FileReader();

                reader.addEventListener('load', function () {
                    $scope.image = reader.result;
                    $scope.$apply();
                }, false);

                reader.readAsDataURL(selectedImage);
            }
        }]);