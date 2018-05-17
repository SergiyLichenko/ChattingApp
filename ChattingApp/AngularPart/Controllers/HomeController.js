'use strict';
app.controller('HomeController', ['$scope', 'authService', '$location', function ($scope, authService, $location) {
    $scope.redirectToChats = function() {
        $location.path("/chats");
    }
}]);