'use strict';

app.factory('messageService',
    ['$http',
    function ($http) {

        var translate = function (id) {
            return $http.get('api/message/translate/' + id);
        }

        return {
            translate: translate
        };
    }]);