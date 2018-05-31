'use strict';

app.controller('JoinChatModalController',
    ['$scope', '$uibModalInstance', 'localStorageService', 'chatHubService', 'chatService',
    function ($scope, $uibModalInstance, localStorageService, chatHubService, chatService) {
        var currentUser = localStorageService.get('user');

        $scope.ok = function (selectedChat) {
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
            $scope.chats = [];
            for (var chat of result.data) {
                if (!chat.users) chat.users = [];

                var contains = chat.users.some(x => x.id === currentUser.id);
                if (!contains) $scope.chats.push(chat);
            }
        });
    }]);