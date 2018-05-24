'use strict';
app.controller('ProfileModalController',
    ['$scope', '$uibModalInstance', '$rootScope', 'userService', 'localStorageService', 'selectedUser',
    function ($scope,$uibModalInstance, $rootScope, userService, localStorageService, selectedUser ) {
        $scope.selectedUser = angular.copy(selectedUser);
        $scope.selectedUser.oldPassword = null;
        $scope.selectedUser.password = null;
        $scope.selectedUser.confirmPassword = null;

        $scope.currentUser = localStorageService.get('user');
        $scope.isReadonly = true;

        $scope.ok = function () {
            $scope.profileBusyPromise = userService.update($scope.selectedUser).then(function () {
                $rootScope.$broadcast('onUserUpdate', $scope.selectedUser);
                $uibModalInstance.close();
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.close();
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