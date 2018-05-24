'use strict';

app.factory('chatHubService',
    ['$rootScope', 'Hub', 'localStorageService',
        function ($rootScope, Hub, localStorageService) {
            var hub = {};

            var start = function () {
                hub = new Hub('chatHub', {
                    rootPath: 'api/chat-hub',
                    methods: ['onChatCreateAsync', 'onChatUpdateAsync', 'onChatDeleteAsync'],
                    queryParams: {
                        token: localStorageService.get('authorizationData').token
                    },
                    listeners: {
                        onChatCreateAsync: function (chat) {
                            $rootScope.$broadcast('onChatCreateAsync', chat);
                        },
                        onChatUpdateAsync: function (chat) {
                            $rootScope.$broadcast('onChatUpdateAsync', chat);
                        },
                        onChatDeleteAsync: function (id) {
                            $rootScope.$broadcast('onChatDeleteAsync', id);
                        }
                    }
                });
            };

            var optimizeChat = function (chat) {
                var newChat = angular.copy(chat);

                newChat.messages = null;
                if (!newChat.users) return newChat;

                for (var user of newChat.users) {
                    user.chats = null;
                    user.img = null;
                }

                return newChat;
            }

            var post = function (chat) {
                return hub.onChatCreateAsync(chat);
            };

            var update = function (chat) {
                var newChat = optimizeChat(chat);
                return hub.onChatUpdateAsync(newChat);
            };

            var $delete = function (id) {
                return hub.onChatDeleteAsync(id);
            };

            return {
                start: start,
                post: post,
                update: update,
                delete: $delete
            };
        }
    ]);