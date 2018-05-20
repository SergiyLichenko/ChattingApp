'use strict';

app.controller('JoinChatModalController',
    ['$uibModalInstance', 'chats', 'localStorageService', 'chatService',
    function ($uibModalInstance, chats, localStorageService, chatService) {
        var self = this;
        self.chats = chats;

        self.ok = function (selectedChat) {
            var currentUser = localStorageService.get('user');
            var contains = selectedChat.users.some(user => user.id === currentUser.id);

            if (!contains) {
                selectedChat.users.push(currentUser);
                self.busyPromise = chatService.update(selectedChat);
            }

            $uibModalInstance.close();
        }

        self.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }]);