'use strict';
app.factory('authInterceptorService', ['localStorageService',
    function (localStorageService) {
        var request = function (config) {
            var authData = localStorageService.get('authorizationData');
            if (!authData) return config;

            config.headers = config.headers || {};
            config.headers.Authorization = 'Bearer ' + authData.token;
            return config;
        }

        return {
            request: request
        };
    }]);