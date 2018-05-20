'use strict';
app.controller('ChatController', ['$templateCache', '$state',
    '$scope', '$sce', 'chatHubService', 'userService', 'chatService',
    'localStorageService', '$timeout',  'authService', 'chats',
    function ($templateCache, $state, $scope, $sce, chatHubService, userService,
        chatService, localStorageService, $timeout, authService, chats) {

        










        $scope.currentChat = {};
        $scope.currentMessageText = "";

        $scope.logOut = function () {
            authService.logOut();
            $state.go('login');
        }

        $scope.getCurrentUsername = function () {
            if (localStorageService.get("authorizationData") === null)
                return null;
            return localStorageService.get("authorizationData").userName;
        }


       
    
        chatHubService.start();


        var selectFirst = function () {
            if ($scope.chats.length > 0) {
                $scope.currentChat.chat = $scope.chats[0];
                $scope.getMessagesForChat($scope.currentChat.chat);
            } else {
                $scope.currentChat.messages = null;
                $scope.currentChat.chat = null;
                $scope.currentChat.userImages = null;
                $scope.currentChat.usersCount = $scope.currentChat.countAll = 0;
            }
        }
        $scope.templateUrl = $templateCache.get('background.html');
        


        $scope.sendMessage = function () {
            var message = {
                text: $scope.currentMessageText,
                chat: {
                    id: $scope.currentChat.chat.id
                },
                user: {
                    userName: localStorageService.get("authorizationData").userName
                }
            };
            chatHubService.sendMessage(message);
            $scope.currentMessageText = "";
        };
        $scope.getUserPhoto = function (id) {
            var length = 0;
            for (var ii in $scope.currentChat.userImages) {
                if ($scope.currentChat.userImages.hasOwnProperty(ii)) length++;
            }
            if ($scope.currentChat.userImages != null && length > 0) {
                for (var i = 0; i < length; i++) {
                    var key = Object.keys($scope.currentChat.userImages)[i];
                    if (key === id) {
                        return $scope.currentChat.userImages[key];
                    };
                };
            };
        };
        $scope.getCurrentUsernamePhoto = function () {
            var currentUsername = $scope.getCurrentUsername();
            var keepGoing = true;
            var id = null;
            if ($scope.currentChat.messages == null || $scope.currentChat.messages.length === 0)
                return null;
            angular.forEach($scope.currentChat.messages, function (item) {
                if (item.user.userName === currentUsername && keepGoing) {
                    id = item.user.id;
                };
            });
            keepGoing = true;
            var image = null;
            angular.forEach($scope.currentChat.userImages, function (key, value) {
                if (id.length > 0 && value === id && keepGoing) {
                    image = key;
                };
            });
            return image;
        }

        $scope.deleteChat = function (chat) {
            chatService.deleteChat(chat).then(function (obj) {
                $scope.chatsLoad();
            });
        }
        $scope.createChat = function (chat) {
            chatService.createChat(chat).then(function (obj) {
                $scope.chatsLoad();
            });
        }
        $scope.addUserToChat = function (chat, username) {
            chat.img = null;
            chatHubService.userToChat(chat, username);
            $scope.chatsLoad();
        }
        $scope.chatsLoad = function () {
            chatService.getChats().then(function (result) {
                $scope.chats = result.data;
                selectFirst();
            });
        };
        //$.connection.hub.start()
        //    .done(function (data) {
        //        if (data && data.token) {
        //            chatHubService.setTokenCookie(data.token);
        //            chatHubService.registerMe();
        //        }
        //    });

        $scope.getMessagesForChat = function (chat) {
            $scope.currentChat.chat = chat;
            $scope.messagesBusyPromise = chatService.getMessagesForChat(chat.id)
                .then(function (result) {
                    if (result.messages.length > 0 &&
                        result.messages[0].chat.id === $scope.currentChat.chat.id) {
                        $scope.currentChat.messages = result.messages;
                        $scope.currentChat.usersCount = result.usersCount;
                        $scope.currentChat.countAll = result.countAll;

                        if ($scope.currentChat.userImages != null) {
                            angular.forEach(result.userImages, function (key, value) {
                                $scope.currentChat.userImages[value] = key;
                            });
                        }
                        else {
                            $scope.currentChat.userImages = result.userImages;
                        }
                    } else {
                        $scope.currentChat.messages = [];
                        $scope.currentChat.usersCount = 1;
                        $scope.currentChat.countAll = 0;
                        if (angular.isUndefined($scope.currentChat.userImages)) {
                            $scope.currentChat.userImages = {};
                        }
                    }
                });
        };

        $scope.deleteMessage = function (message) {
            for (var i = 0; i < $scope.currentChat.messages.length; i++) {
                if ($scope.currentChat.messages[i].id === message.id) {
                    if ($scope.currentChat.messages[i].user.userName === localStorageService.get("authorizationData").userName) {
                        chatHubService.deleteMessage(message);
                    }
                }
            }
        };
        $scope.updateMessage = function (tempMessage) {
            var message = angular.copy(tempMessage);
            if (message.user.userName === localStorageService.get("authorizationData").userName) {
                message.chat = {
                    id: $scope.currentChat.id
                }
                chatHubService.updateMessage(message);
            }
        }
        $scope.quitChat = function (chatid) {
            $scope.busyPromise = chatService.quitChat(chatid).then(function (result) {
                if (result) {
                    $scope.chatsLoad();
                }

            });
        }


        chatHubService.messageCallback = function (message) {
            if ($scope.currentChat.messages == null || angular.isUndefined($scope.currentChat.messages)) createDefaultMessages();
            $scope.currentChat.messages.push(message);
            $scope.currentChat.countAll++;
            $timeout(function () {
                $scope.$applyAsync();
            });
        }
        function createDefaultMessages() {
            $scope.currentChat.messages = [];
            $scope.currentChat.userImages = [];
            $scope.currentChat.countAll = 0;
            $scope.currentChat.usersCount = 1;
        };
        chatHubService.messageDeleteCallback = function (message) {
            if ($scope.currentChat.messages == null || angular.isUndefined($scope.currentChat.messages)) createDefaultMessages();
            for (var i = 0; i < $scope.currentChat.messages.length; i++) {
                if ($scope.currentChat.messages[i].id === message.id) {
                    $scope.currentChat.messages.splice(i, 1);  //removes 1 element at position i 
                    break;
                }
            }
            $scope.currentChat.countAll--;
            $timeout(function () {
                $scope.$applyAsync();
            });
        }
        chatHubService.messageUpdateCallback = function (message) {
            if ($scope.currentChat.messages == null || angular.isUndefined($scope.currentChat.messages)) createDefaultMessages();
            angular.forEach($scope.currentChat.messages, function (item) {
                if (item.id === message.id) {
                    item.text = message.text;
                };
            });

            $timeout(function () {
                $scope.$applyAsync();
            });
        }
        chatHubService.onAddUserToChatCallback = function (user) {
            if ($scope.currentChat.messages == null || angular.isUndefined($scope.currentChat.messages)) createDefaultMessages();

            $scope.currentChat.userImages[user.id] = user.img;
            $scope.currentChat.usersCount++;
            if (user.userName === $scope.getCurrentUsername()) {
                $scope.chatsLoad();
                return;
            };
            $scope.currentChat.chat.users.push(user);
        }
        $scope.getClass = function (index) {
            if ($scope.currentChat.messages[index].user.userName === localStorageService.get("authorizationData").userName)
                return "i";
            else
                return "friend-with-a-SVAGina";
        }


        $scope.makeFavourite = function (message) {
            chatService.makeFavourite(message.id);
        }

        var onLoad = function() {
            if (chats.length) {
                $scope.currentChat.chat = chats[0];
                $scope.getMessagesForChat($scope.currentChat.chat);
            }
        }

        onLoad();
    }
]);