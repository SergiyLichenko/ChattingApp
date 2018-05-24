'use strict';

app.controller('JoinChatModalController',
    ['$scope', '$uibModalInstance', 'localStorageService', 'chatHubService', 'chatService',
    function ($scope, $uibModalInstance, localStorageService, chatHubService, chatService) {

        $scope.ok = function (selectedChat) {
            var currentUser = localStorageService.get('user');
            var contains = selectedChat.users.some(user => user.id === currentUser.id);

            if (contains) {
                $uibModalInstance.close();
                return;
            }
               
            selectedChat.users.push(currentUser);
            $scope.joinChatBusyPromise = chatHubService.update(selectedChat)
                .then($uibModalInstance.close);
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        }

        $scope.joinChatBusyPromise = chatService.getAll().then(result => {
            var chats = result.data;
            for (var chat of chats)
                if (!chat.users) chat.users = [];

            $scope.chats = chats;
        });
    }]);