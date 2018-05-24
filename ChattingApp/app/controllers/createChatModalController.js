'use strict';

app.controller('CreateChatModalController',
    ['$scope', '$rootScope', '$timeout', '$uibModalInstance', 'chatHubService',
    function ($scope, $rootScope, $timeout, $uibModalInstance, chatHubService) {
        $scope.ok = function () {
            var reader = new FileReader();
            var chat = { title: $scope.title };
            reader.addEventListener('load', function () {
                chat.img = reader.result;
                $scope.createChatBusyPromise = chatHubService.post(chat).then(function () {
                    $uibModalInstance.close();
                });
                $timeout(function () { $scope.$apply(); });
            }, false);
            reader.readAsDataURL($scope.file);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };
    }]);