'use strict';
app.factory('userService', ['$location', '$http', 'localStorageService', '$q',
    function ($location, $http, localStorageService, $q) {

        var userServiceFactory = {};

        userServiceFactory.getCurrent = function () {
            return $http.get('api/user/current').then(function (result) {
                return result.data;
            });
        }






        userServiceFactory.getUserByName = function (name) {
            var deffered = $q.defer();
            if (name != null) {
                var uuid = guid();
                $http.get('api/user/' + name + '/' + uuid).then(function (result) {
                    deffered.resolve(result);
                });
            } else ($location.path('/login'));
            return deffered.promise;
        }

        userServiceFactory.updateUser = function (newUser, oldUser, oldPassword) {
            var deffered = $q.defer();
            var request = {
                oldUser: oldUser,
                newUser: newUser,
                oldPassword: oldPassword
            };
            if (name != null) {
                var uuid = guid();
                $http.post('api/user/edit/', request).then(function (result) {
                    localStorageService.set('authorizationData', {
                        userName: result.data.userName
                    });
                    deffered.resolve(result);
                });
            } else ($location.path('/login'));
            return deffered.promise;
        };

        return userServiceFactory;

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                  .toString(16)
                  .substring(1);
            }
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
              s4() + '-' + s4() + s4() + s4();
        }
    }]);