'use strict';

app.controller('LogInController',
    ['$scope', '$state', 'authService',
    function ($scope, $state, authService) {
        $scope.loginData = {};

        $scope.login = function () {
            $scope.busyPromise = authService.login($scope.loginData).then(function () {
                $state.go('chat');
            }, function (err) {
                $scope.message = err.error_description;
            });
        };
    }]);