'use strict';

app.controller('JoinChatModalController',
    ['$scope', '$uibModalInstance', 'chats', 'localStorageService', 'chatService',
    function ($scope, $uibModalInstance, chats, localStorageService, chatService) {
        $scope.chats = chats;

        $scope.ok = function (selectedChat) {
            var currentUser = localStorageService.get('user');
            var contains = selectedChat.users.some(user => user.id === currentUser.id);

            if (!contains) {
                selectedChat.users.push(currentUser);
                self.busyPromise = chatService.update(selectedChat);
            }

            $uibModalInstance.close();
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }
    }]);