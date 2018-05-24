'use strict';

app.controller('NavbarController',
    ['$scope', '$state', '$mdToast', 'authService',
    function ($scope, $state, $mdToast, authService) {
        $scope.logOut = function () {
            authService.logOut();
            $state.go('login');
        }

        $scope.isLoggedIn = authService.isLoggedIn;
        $scope.currentNavItem = 'logOut';

        $scope.$on('onGlobalError', function (event, message) {
            var toast = $mdToast.simple().textContent(message)
                .toastClass('toast')
                .action('UNDO').highlightAction(true)
                .hideDelay(3000).position('top right');

            $mdToast.show(toast);
        });
    }]);