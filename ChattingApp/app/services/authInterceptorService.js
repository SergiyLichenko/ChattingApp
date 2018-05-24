'use strict';

app.factory('authInterceptorService',
    ['$injector', '$q', '$rootScope', 'localStorageService',
    function ($injector, $q, $rootScope, localStorageService) {
        var request = function (config) {
            var authData = localStorageService.get('authorizationData');
            if (!authData) return config;

            config.headers = config.headers || {};
            config.headers.authorization = 'Bearer ' + authData.token;
            return config;
        };

        var responseError = function (error) {
            if (error.status === 401)
                return $injector.get('$state').go('login');
            if (error.status === 500)
                $rootScope.$broadcast('onGlobalError', error.data);

            error.config = null;
            return $q.reject(error);
        };

        return {
            request: request,
            responseError: responseError
        };
    }]);