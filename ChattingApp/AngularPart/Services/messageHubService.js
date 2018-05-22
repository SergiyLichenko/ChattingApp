'use strict';

app.factory('messageHubService',
    ['$http', '$rootScope', 'Hub', 'localStorageService',
        function ($http, $rootScope, Hub, localStorageService) {
            var hub = new Hub('messageHub', {
                rootPath: 'api/message',
                methods: ['onMessageCreateAsync', 'onMessageUpdateAsync', 'onMessageDeleteAsync'],
                queryParams: {
                    token: localStorageService.get('authorizationData').token
                },
                listeners: {
                    onMessageCreateAsync: function (message) {
                        $rootScope.$broadcast('onMessageCreateAsync', message);
                    },
                    onMessageUpdateAsync: function(message) {
                        $rootScope.$broadcast('onMessageUpdateAsync', message);
                    },
                    onMessageDeleteAsync: function(message) {
                        $rootScope.$broadcast('onMessageDeleteAsync', message);
                    }
                }
            });

            var post = function (message) {
                hub.onMessageCreateAsync(message);
            }

            var update = function(message) {
                hub.onMessageUpdateAsync(message);
            }

            var $delete = function(message) {
                hub.onMessageDeleteAsync(message);
            }

            return {
                post: post,
                update: update,
                delete: $delete
            }
        }
    ]);