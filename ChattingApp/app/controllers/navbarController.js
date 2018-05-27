'use strict';

app.controller('NavbarController',
    ['$scope', '$state', '$mdToast', 'authService',
        function ($scope, $state, $mdToast, authService) {
            $scope.logOut = function () {
                authService.logOut();
                $state.go('login');
            }

            $scope.isLoggedIn = authService.isLoggedIn;
            $scope.$on('onGlobalError', function (event, message) {
                var toast = $mdToast.simple().textContent(message)
                    .toastClass('toast')
                    .action('UNDO').highlightAction(true)
                    .hideDelay(3000).position('top right');

                $mdToast.show(toast);
            });

            $scope.$watch(() => $state.$current.name,
                function (newVal) {
                    switch (newVal) {
                        case 'login':
                            $scope.currentNavItem = 'logIn';
                            break;
                        case 'signup':
                            $scope.currentNavItem = 'signUp';
                            break;
                        default:
                            $scope.currentNavItem = 'logOut';
                    }
                });
        }]);