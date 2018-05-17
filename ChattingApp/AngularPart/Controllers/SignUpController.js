'use strict';
app.controller('SignUpController', ['$scope', '$state', '$timeout', 'authService',
    function ( $scope, $state, $timeout, authService) {
     
        $scope.savedSuccessfully = false;
        $scope.message = "";

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $state.go('login');
            }, 2000);
        }

        $scope.registration = {
            userName: "",
            password: "",
            confirmPassword: "",
            email: ""
        };

        $scope.signUp = function () {

            authService.saveRegistration($scope.registration).then(function (response) {

                $scope.savedSuccessfully = true;
                $scope.message = "user has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();

            },
             function (response) {
                 var errors = [];
                 for (var key in response.data.modelState) {
                     for (var i = 0; i < response.data.modelState[key].length; i++) {
                         errors.push(response.data.modelState[key][i]);
                     }
                 }
                 $scope.message = "Failed to register user due to:" + errors.join(' ');
             });
        };
    }]);