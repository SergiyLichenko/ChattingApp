'use strict';

app.factory('messageHubService',
    ['$rootScope', 'Hub', 'localStorageService',
        function ($rootScope, Hub, localStorageService) {
            var hub = {};

            var start = function () {
                hub = new Hub('messageHub', {
                    rootPath: 'api/message-hub',
                    methods: ['onMessageCreateAsync', 'onMessageUpdateAsync', 'onMessageDeleteAsync'],
                    queryParams: {
                        token: localStorageService.get('authorizationData').token
                    },
                    listeners: {
                        onMessageCreateAsync: function (message) {
                            $rootScope.$broadcast('onMessageCreateAsync', message);
                        },
                        onMessageUpdateAsync: function (message) {
                            $rootScope.$broadcast('onMessageUpdateAsync', message);
                        },
                        onMessageDeleteAsync: function (message) {
                            $rootScope.$broadcast('onMessageDeleteAsync', message);
                        }
                    }
                });
            }

            var optimizeMessage = function (message) {
                var newMessage = angular.copy(message);
                newMessage.author = {id: message.author.id};
                return newMessage;
            }

            var post = function (message) {
                return hub.onMessageCreateAsync(message);
            };

            var update = function (message) {
                var newMessage = optimizeMessage(message);
                return hub.onMessageUpdateAsync(newMessage);
            };

            var $delete = function (message) {
                var newMessage = optimizeMessage(message);
                return hub.onMessageDeleteAsync(newMessage);
            };

            return {
                start: start,
                post: post,
                update: update,
                delete: $delete
            };
        }
    ]);