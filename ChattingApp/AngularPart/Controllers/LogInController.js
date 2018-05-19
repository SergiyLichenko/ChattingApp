'use strict';
app.controller('LogInController', ['$scope', '$state', 'authService', 'localStorageService',
    function ($scope, $state, authService, localStorageService) {
        $scope.loginData = {};

        $scope.login = function () {
            authService.login($scope.loginData).then(function () {
                localStorageService.set("authorizationData", {
                    userName: $scope.loginData.userName
                });
                $state.go('chats');
            }, function (err) {
                $scope.message = err.error_description;
            });
        };
    }]);