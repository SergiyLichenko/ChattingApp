'use strict';
app.factory('userService', ['$http',
    function ($http) {
        var getCurrent = function() {
            return $http.get('api/user/current');
        };

        var getAll = function() {
            return $http.get('api/user/all');
        }

        var update = function(user) {
            return $http.put('api/user', user);
        };

        return {
            getCurrent: getCurrent,
            getAll: getAll,
            update: update
        };
    }]);