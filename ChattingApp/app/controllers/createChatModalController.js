'use strict';

app.controller('CreateChatModalController',
    ['$scope', '$rootScope', '$uibModalInstance', 'chatHubService',
    function ($scope, $rootScope, $uibModalInstance, chatHubService) {
        $scope.ok = function () {
            var reader = new FileReader();
            var chat = { title: $scope.title };
            reader.addEventListener('load', function () {
                chat.img = reader.result;
                chatHubService.post(chat).then(function () {
                    $uibModalInstance.close();
                });
            }, false);
            reader.readAsDataURL($scope.file);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };
    }]);