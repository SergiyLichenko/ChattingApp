'use strict';

app.controller('CreateChatModalController',
    ['$scope', '$uibModalInstance', 'chatService',
    function ($scope, $uibModalInstance, chatService) {
        var self = this;

        self.ok = function () {
            var reader = new FileReader();
            reader.addEventListener("load", function () {
                chatService.create({
                    title: $scope.chatTitle,
                    img: reader.result
                }).then(function () {
                    $uibModalInstance.close(self);
                });
            }, false);
            reader.readAsDataURL($scope.file);
        };

        self.cancel = function () {
            $uibModalInstance.dismiss();
        };
    }]);