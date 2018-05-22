'use strict';

app.factory('authService',
    ['$http', '$q', 'localStorageService', 'userService',
    function ($http, $q, localStorageService,  userService) {
        var logOut = function () {
            localStorageService.remove('authorizationData');
            localStorageService.remove('user');
        };

        var onLoginSuccess = function (response, deferred, loginData) {
            if (response.status !== 200) return;

            localStorageService.set('authorizationData', {
                token: response.data.access_token,
                userName: loginData.userName
            });

            userService.getCurrent().then(function(user) {
                localStorageService.set('user', user);
                deferred.resolve(response.data);
            });
        }

        var login = function (loginData) {

            var data = 'grant_type=password&username=' + loginData.userName + '&password=' +
                loginData.password + '&client_id=' + loginData.userName;;

            var deferred = $q.defer();

            $http.post('/token', data,
                { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (response) {
                onLoginSuccess(response, deferred, loginData);
            }, function (err) {
                deferred.reject(err.data);
            });
            return deferred.promise;
        };

        var signUp = function (registration) {
            return $http.post('api/account/signup', registration);
        };

        return {
            signUp: signUp,
            login: login,
            logOut: logOut
        };
    }]);