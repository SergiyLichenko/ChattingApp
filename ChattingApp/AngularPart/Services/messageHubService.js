'use strict';

app.factory('messageHubService',
    ['$http', '$rootScope', 'Hub', 'localStorageService',
        function ($http, $rootScope, Hub, localStorageService) {
            var hub = new Hub('messageHub', {
                rootPath: 'api/message',
                methods: ['onMessageCreateAsync'],
                queryParams: {
                    token: localStorageService.get('authorizationData').token
                },
                listeners: {
                    onMessageCreateAsync: function (message) {
                        $rootScope.$broadcast('onMessageCreateAsync', message);
                    }
                }
            });

            var post = function (message) {
                hub.onMessageCreateAsync(message);
            }

            return {
                post: post
            }
        }
    ]);