'use strict';
app.controller('ProfileModalController',
    ['$scope', 'userService', 'localStorageService', 'selectedUser', '$uibModalInstance',
    function ($scope, userService, localStorageService, selectedUser, $uibModalInstance) {
        $scope.selectedUser = angular.copy(selectedUser);
        $scope.currentUser = localStorageService.get('user');
        $scope.isReadonly = true;

        $scope.ok = function () {
            userService.update($scope.selectedUser).then(function () {
                $uibModalInstance.close();
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.processImage = function (image) {
            var reader = new FileReader();

            reader.addEventListener('load', function () {
                $scope.selectedUser.img = reader.result;
                $scope.$apply();
            }, false);

            reader.readAsDataURL(image);
        }
    }]);