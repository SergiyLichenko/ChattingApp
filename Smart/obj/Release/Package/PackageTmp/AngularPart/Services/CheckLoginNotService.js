'use strict';
app.factory('checkLoginNotService', ['$state', 'localStorageService',
    function ($state, localStorageService) {
        var getCurrentUsername = function () {
            if (localStorageService.get("authorizationData") === null)
                return null;
            return localStorageService.get("authorizationData").userName;
        }
        var checkLogin = function () {
            if (!angular.isUndefined(getCurrentUsername()) && getCurrentUsername() !== null) {
                $state.go('chats');
            }
        }
        return {
            checkLogin: checkLogin
        };
    }]);