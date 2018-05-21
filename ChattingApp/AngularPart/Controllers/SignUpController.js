'use strict';
app.controller('SignUpController', ['$scope', '$state', 'authService',
    function ($scope, $state, authService) {
        $scope.registration = {};

        var getErrorMessage = function (response) {
            var errors = [];
            for (var key in response.data.ModelState)
                if (response.data.ModelState.hasOwnProperty(key))
                    for (var i = 0; i < response.data.ModelState[key].length; i++)
                        errors.push(response.data.ModelState[key][i]);
            return errors.join(' ');
        }

        $scope.signUp = function () {
            authService.signUp($scope.registration).then(function () {
                $state.go('login');
            }, function (response) {
                var erorMessage = getErrorMessage(response);
                $scope.message = 'Failed to register user due to:' + erorMessage;
            });
        };
    }]);