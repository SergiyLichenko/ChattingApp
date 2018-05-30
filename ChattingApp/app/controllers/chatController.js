'use strict';

app.controller('ChatController',
    ['$rootScope', '$scope', '$timeout', 'localStorageService', 'messageHubService', 'chatHubService', 'messageService',
        function ($rootScope, $scope, $timeout, localStorageService, messageHubService, chatHubService, messageService) {
            $scope.messageText = '';
            $scope.open = false;

            $scope.$on('onUserUpdate', function (event, user) {
                if ($scope.currentUser) $scope.currentUser = user;
                if (!$scope.selectedChat) return;

                for (var message of $scope.selectedChat.messages)
                    if (message.author.id === user.id)
                        Object.assign(message.author, user);

                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatCreateAsync', function (event, chat) {
                if ($scope.selectedChat) return;
                $scope.selectedChat = chat;
                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatUpdateAsync', function (event, chat) {
                if (!$scope.selectedChat) $scope.selectedChat = chat;
                if (chat.id !== $scope.selectedChat.id) return;

                Object.assign($scope.selectedChat, chat);
                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatDeleteAsync', function (event, chatId) {
                if (!$scope.selectedChat || chatId !== $scope.selectedChat.id) return;
                $scope.selectedChat = null;
                $timeout(function () { $scope.$apply(); });
            });

            $rootScope.$on('onSelectChat', function (event, selectedChat) {
                $scope.selectedChat = selectedChat;
                $timeout(function () { $scope.$apply(); });
            });

            $rootScope.$on('onMessageCreateAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;
                $scope.messageText = '';

                Object.assign($scope.selectedChat, message.chat);
                if ($scope.selectedChat.messages.findIndex(x => x.id === message.id) === -1)
                    $scope.selectedChat.messages.push(message);
                if (message.author.id === $scope.currentUser.id)
                    $rootScope.$broadcast('onUserUpdate', message.author);

                $timeout(function () { $scope.$apply(); });
            });

            $rootScope.$on('onMessageUpdateAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;

                var existingMessageIndex = $scope.selectedChat.messages.findIndex(x => x.id === message.id);
                if (existingMessageIndex === -1) return;

                var existingMessage = $scope.selectedChat.messages[existingMessageIndex];
                Object.assign(existingMessage, message);
                $timeout(function () { $scope.$apply(); });
            });

            $rootScope.$on('onMessageDeleteAsync', function (event, message) {
                if ($scope.selectedChat.id !== message.chat.id) return;

                var existingMessageIndex = $scope.selectedChat.messages.findIndex(x => x.id === message.id);
                if (existingMessageIndex === -1) return;
                $scope.selectedChat.messages.splice(existingMessageIndex, 1);
                $timeout(function () { $scope.$apply(); });
            });

            $scope.sendMessage = function (messageText) {
                if (messageText === '') return;

                var message = {
                    text: messageText,
                    chat: { id: $scope.selectedChat.id },
                    author: { id: $scope.currentUser.id }
                };
                $scope.chatBusyPromise = messageHubService.post(message);
            };

            $scope.updateMessage = function (message) {
                message.isEditable = false;
                message.chat = { id: $scope.selectedChat.id };
                $scope.chatBusyPromise = messageHubService.update(message);
            }

            $scope.deleteMessage = function (message) {
                message.chat = { id: $scope.selectedChat.id };
                $scope.chatBusyPromise = messageHubService.delete(message);
            }

            $scope.translate = function (message) {
                $scope.chatBusyPromise = messageService.translate(message.id).then(
                    function (result) { message.translations = result.data; });
            }

            $scope.quitChat = function (chat) {
                var index = chat.users.findIndex(x => x.id === $scope.currentUser.id);
                if (index !== -1) chat.users.splice(index, 1);

                $scope.$parent.busyPromise = chatHubService.update(chat);
            }

            $scope.deleteChat = function (chatId) {
                $scope.$parent.busyPromise = chatHubService.delete(chatId);
            }

            $scope.onKeyPress = function (event) {
                if (event.shiftKey) return;

                if (event.which === 32) {
                    $scope.messageText += ' ';
                    event.preventDefault();
                } else if (event.which === 13) {
                    $scope.sendMessage($scope.messageText);
                    event.preventDefault();
                }
            }
        }
    ]);