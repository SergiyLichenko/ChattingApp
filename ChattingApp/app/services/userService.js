'use strict';
app.factory('userService', ['$http',
    function ($http) {
        var getCurrent = function() {
            return $http.get('api/user/current').then(function(result) {
                return result.data;
            });
        };

        var update = function(user) {
            return $http.put('api/user', user);
        };

        return {
            getCurrent: getCurrent,
            update: update
        };
    }]);