'use strict';
app.controller('ProfileModalController',
    ['$scope', '$uibModal', '$state', 'userService', 'localStorageService', 'selectedUser', '$uibModalInstance', '$timeout',
    function ($scope, $uibModal, $state, userService, localStorageService, selectedUser,
        $uibModalInstance, $timeout) {
        $scope.selectedUser = selectedUser;
        $scope.currentUser = localStorageService.get('user');
        $scope.isReadonly = true;

        $scope.ok = function () {
            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.processImage = function (image) {
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                $scope.selectedUser.img = reader.result;
                $scope.$apply();
            }, false);

            reader.readAsDataURL(image);
        }
    }]);