'use strict';

app.factory('chatService',
    ['$http',
    function ($http) {
        var getAll = function () {
            return $http.get('api/chat/all');
        };

        var getById = function (id) {
            return $http.get('api/chat/' + id);
        };

        return {
            getAll: getAll,
            getById: getById
        };
    }]);