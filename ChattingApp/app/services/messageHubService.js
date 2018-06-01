'use strict';

app.factory('messageHubService',
    ['$rootScope', '$q', 'Hub', 'localStorageService',
        function ($rootScope, $q, Hub, localStorageService) {
            var hub = {};

            var start = function () {
                hub = new Hub('messageHub', {
                    useSharedConnection: false,
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

            var requestServer = function (callback) {
                var deferred = $q.defer();
                callback().then(deferred.resolve, deferred.reject);

                return deferred.promise;
            }

            var post = function (message) {
                return requestServer(() => hub.onMessageCreateAsync(message));
            };

            var update = function (message) {
                var newMessage = optimizeMessage(message);
                return requestServer(() => hub.onMessageUpdateAsync(newMessage));
            };

            var $delete = function (message) {
                var newMessage = optimizeMessage(message);
                return requestServer(() => hub.onMessageDeleteAsync(newMessage));
            };

            return {
                start: start,
                post: post,
                update: update,
                delete: $delete
            };
        }
    ]);