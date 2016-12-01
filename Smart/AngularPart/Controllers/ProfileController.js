'use strict';
app.controller('ProfileController', ['$scope', '$state', 'userService', 'localStorageService', '$uibModal',
    '$log', 'chatsService',
    function ($scope, $state, userService, localStorageService, $uibModal, $log, chatsService) {

        $scope.open = function (size, username) {
            $scope.username = username;
            $scope.modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'AngularPart/Views/Profile.html',
                controller: 'ProfileControllerInstance',
                size: size,
                resolve: {
                    username: function () {
                        return username;
                    }
                }
            });

            $scope.modalInstance.result.then(function (ctrl) {
                ctrl.$parent.user.img = ctrl.$parent.uploader.queue.length > 0 ?
                    ctrl.$parent.uploader.queue[0].image.src : ctrl.$parent.user.img;
               ctrl.$parent.busyPromise = userService.updateUser(ctrl.$parent.user, ctrl.$parent.oldUser, ctrl.$parent.user.oldPassword)
                .then(function () {
                    $scope.$parent.chatsLoad();
                });
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    }]);

app.controller('ProfileControllerInstance',

    function ($scope, $state, userService, localStorageService, $uibModal,
        $uibModalInstance, FileUploader, username, $timeout) {
        $scope.getUser = function () {
            userService.getUserByName(username).
            then(function (result) {
                $scope.user = result.data;
                $scope.oldUser = angular.copy(result.data);
                $timeout(function () {

                    $scope.$applyAsync();
                });
            });
        }
    

        $scope.getCurrentUsername = function () {
            return localStorageService.get("authorizationData").userName;
        }
       
        $scope.getUser();
        $scope.isReadonly = true;
        $scope.uploader = new FileUploader({
            queueLimit: 1
        });


        $scope.ok = function () {
            $uibModalInstance.close(this);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.open = function (size) {
            $scope.modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'AngularPart/Views/Profile.html',
                controller: 'ProfileController',
                size: size,
            });
            $scope.getUser();
            $scope.oldPassword = "";
            $scope.modalInstance.result.then(function (ctrl) {

            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    });