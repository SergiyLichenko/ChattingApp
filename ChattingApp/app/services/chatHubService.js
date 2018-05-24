'use strict';

app.factory('chatHubService',
    ['$rootScope', '$q', 'Hub', 'localStorageService',
        function ($rootScope, $q, Hub, localStorageService) {
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

            var requestServer = function (callback) {
                var deferred = $q.defer();
                callback().then(deferred.resolve, deferred.reject);

                return deferred.promise;
            }

            var post = function (chat) {
                return requestServer(() => hub.onChatCreateAsync(chat));
            };

            var update = function (chat) {
                var newChat = optimizeChat(chat);
                return requestServer(() => hub.onChatUpdateAsync(newChat));
            };

            var $delete = function (id) {
                return requestServer(() => hub.onChatDeleteAsync(id));
            };

            return {
                start: start,
                post: post,
                update: update,
                delete: $delete
            };
        }
    ]);