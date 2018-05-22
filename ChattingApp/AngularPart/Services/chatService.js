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

        var create = function (chat) {
            return $http.post('api/chat/', chat);
        };

        var update = function (chat) {
            return $http.put('api/chat/', chat);
        };

        var $delete = function (id) {
            return $http.delete('api/chat', { data: id });
        };

        return {
            getAll: getAll,
            getById: getById,
            create: create,
            update: update,
            delete: $delete
        };
    }]);