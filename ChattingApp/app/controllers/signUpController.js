'use strict';

app.controller('SignUpController',
    ['$scope', '$state', 'authService',
    function ($scope, $state, authService) {
        $scope.registration = {};

        var getErrorMessage = function (response) {
            var errors = [];
            for (var key in response.data.modelState)
                if (response.data.modelState.hasOwnProperty(key))
                    for (var error of response.data.modelState[key])
                        errors.push(error);
            return errors.join(' ');
        }

        $scope.signUp = function () {
            $scope.busyPromise = authService.signUp($scope.registration).then(function () {
                $state.go('login');
            }, function (response) {
                var erorMessage = getErrorMessage(response);
                $scope.message = 'Failed to register user due to: ' + erorMessage;
            });
        };
    }]);