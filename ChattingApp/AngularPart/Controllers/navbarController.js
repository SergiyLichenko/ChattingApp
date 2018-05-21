'use strict';

app.controller('NavbarController',
    ['$scope', '$state', 'authService', 'localStorageService',
function ($scope, $state, authService, localStorageService) {
    $scope.logOut = function () {
        authService.logOut();
        $state.go('login');
    }

    $scope.isLoggedIn = function () {
        return !!localStorageService.get('authorizationData');
    }

    $scope.currentNavItem = 'logOut';
}]);