'use strict';

app.controller('JoinChatModalController',
    ['$scope', '$uibModalInstance', 'chats', 'localStorageService', 'chatHubService',
    function ($scope, $uibModalInstance, chats, localStorageService, chatHubService) {
        $scope.chats = chats;

        $scope.ok = function (selectedChat) {
            var currentUser = localStorageService.get('user');
            var contains = selectedChat.users.some(user => user.id === currentUser.id);

            if (!contains) {
                selectedChat.users.push(currentUser);
                self.busyPromise = chatHubService.update(selectedChat);
            }

            $uibModalInstance.close();
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    }]);