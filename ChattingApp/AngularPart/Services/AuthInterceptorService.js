'use strict';
app.factory('authInterceptorService', ['localStorageService', '$injector',
    function (localStorageService, $injector) {
        var request = function (config) {
            var authData = localStorageService.get('authorizationData');
            if (!authData) return config;

            config.headers = config.headers || {};
            config.headers.Authorization = 'Bearer ' + authData.token;
            return config;
        }

        var responseError = function (error) {
            if (error.status === 401)
                $injector.get('$state').go('login');
        }

        return {
            request: request,
            responseError: responseError
        };
    }]);