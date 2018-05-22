'use strict';

app.controller('CreateChatModalController',
    ['$scope', '$uibModalInstance', 'chatService',
    function ($scope, $uibModalInstance, chatService) {
        $scope.ok = function () {
            var reader = new FileReader();
            reader.addEventListener('load', function () {
                chatService.create({
                    title: $scope.title,
                    img: reader.result
                }).then(function () {
                    $uibModalInstance.close(self);
                });
            }, false);
            reader.readAsDataURL($scope.file);
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };
    }]);