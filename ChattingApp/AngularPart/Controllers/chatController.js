'use strict';

app.controller('ChatController',
    ['$rootScope', '$scope', '$timeout', 'localStorageService', 'messageHubService', 'chatService',
        function ($rootScope, $scope, $timeout, localStorageService, messageHubService, chatService) {
            $scope.currentUser = localStorageService.get('user');

            var getAuthor = function (chat, authorId) {
                var authorIndex = $scope.selectedChat.users.findIndex(x => x.id === authorId);
                if (authorIndex === -1) return null;
                return $scope.selectedChat.users[authorIndex];
            }

            $rootScope.$on('onSelectChat', function (event, selectedChat) {
                $scope.selectedChat = selectedChat;
                for (var message of $scope.selectedChat.messages)
                    message.author = getAuthor($scope.selectedChat, message.authorId);
                $timeout(function() {
                    $scope.$apply();
                });
            });

            $rootScope.$on('onMessageCreateAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;
                $scope.messageText = '';

                message.author = getAuthor($scope.selectedChat, message.authorId);
                $scope.selectedChat.messages.push(message);
                $scope.$apply();
            });

            $rootScope.$on('onMessageUpdateAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;

                var existingMessageIndex = $scope.selectedChat.messages.findIndex(x => x.id === message.id);
                if (existingMessageIndex === -1) return;

                var existingMessage = $scope.selectedChat.messages[existingMessageIndex];
                Object.assign(existingMessage, message);
                $scope.$apply();
            });

            $rootScope.$on('onMessageDeleteAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;

                var existingMessageIndex = $scope.selectedChat.messages.findIndex(x => x.id === message.id);
                if (existingMessageIndex === -1) return;
                $scope.selectedChat.messages.splice(existingMessageIndex, 1);
                $scope.$apply();
            });

            $scope.sendMessage = function (messageText) {
                var message = {
                    text: messageText,
                    chat: {
                        id: $scope.selectedChat.id
                    },
                    authorId: $scope.currentUser.id
                };
                messageHubService.post(message);
            };

            $scope.updateMessage = function (message) {
                message.chat = {
                    id: $scope.selectedChat.id
                };
                messageHubService.update(message);
            }

            $scope.deleteMessage = function (message) {
                message.chat = {
                    id: $scope.selectedChat.id
                };
                messageHubService.delete(message);
            }

            $scope.quitChat = function (chat) {
                var index = chat.users.findIndex(x=>x.id === $scope.currentUser.id);
                if (index !== -1) chat.users.splice(index, 1);

                $scope.busyPromise = chatService.update(chat);
            }

            $scope.deleteChat = function(chatId) {
                $scope.busyPromise = chatService.delete(chatId);
            }

            $scope.onKeyPress = function (event) {
                if (event.which === 13)
                    $scope.sendMessage($scope.messageText);
            }
        }
    ]);