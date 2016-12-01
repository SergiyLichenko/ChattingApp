'use strict';
app.controller('IndexController', ['$scope', '$state', 'authService', function ($scope, $state, authService) {

    $scope.authentication = authService.authentication;

}]);