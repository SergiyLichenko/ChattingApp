'use strict';

app.controller('EditChatModalController',
    ['$scope', '$uibModalInstance', 'chatHubService', 'localStorageService', 'currentChat',
    function ($scope, $uibModalInstance, chatHubService, localStorageService, currentChat) {
        $scope.isReadonly = true;
        $scope.currentChat = currentChat;
        $scope.currentUser = localStorageService.get('user');

        $scope.getAuthor = function () {
            var index = currentChat.users.findIndex(x => x.id === currentChat.authorId);
            return currentChat.users[index];
        }

        $scope.quitChat = function (user) {
            var index = currentChat.users.findIndex(x=> x.id === user.id);
            if (index !== -1) currentChat.users.splice(index, 1);
        }

        $scope.ok = function () {
            $scope.editChatBusyPromise = chatHubService.update($scope.currentChat).then(function () {
                $uibModalInstance.close();
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
        };

        $scope.processImage = function (image) {
            if (!image) return;
            var reader = new FileReader();

            reader.addEventListener('load', function () {
                $scope.currentChat.img = reader.result;
                $scope.$apply();
            }, false);

            reader.readAsDataURL(image);
        }
    }]);