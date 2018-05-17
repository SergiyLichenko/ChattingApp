'use strict';

app.controller('EditChatController', ['$scope', '$state', 'userService', 'localStorageService', '$uibModal',
    '$log', 'chatsService',
    function ($scope, $state, userService, localStorageService, $uibModal, $log, chatsService) {

        $scope.open = function (size, currentChat) {

            $scope.modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'AngularPart/Views/EditChat.html',
                controller: 'EditChatControllerInstance',
                size: size,
                windowClass: 'app-modal-window',
                resolve: {
                    currentChat: function () {
                        return currentChat;
                    }
                }
            });


            $scope.modalInstance.result.then(function (ctrl) {

            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    }]);

app.controller('EditChatControllerInstance',

    function ($scope, $state, chatsService, localStorageService, $uibModal,
        $uibModalInstance, FileUploader, currentChat, $timeout) {
        $scope.currentChat = angular.copy(currentChat);
        $scope.uploader = new FileUploader({
            queueLimit: 1
        });
        $scope.isReadonly = true;
        $scope.users = [];
        angular.forEach(currentChat.users, function (value, key) {
            $scope.users.push(value.userName);
        });
        $scope.selectedUser = $scope.users[0];

        $scope.quitChat = function (username) {
            var tempUser = username;
            chatsService.quitChat($scope.currentChat.id, username).then(function (result) {
                if (result) {
                    var index = $scope.users.indexOf(tempUser);
                    $scope.users.splice(index, 1);
                    if ($scope.users.length > 0) {
                        $scope.selectedUser = $scope.users[0];
                    }
                    $timeout(function () {
                        $scope.$applyAsync();
                    });
                };
            });
        }
        $scope.getCurrentUser = function() {
            return localStorageService.get("authorizationData").userName;
        }
        $scope.editChat = function (chat) {
            chatsService.editChat(chat);
        }
        $scope.ok = function (chat) {
            if ($scope.uploader.queue.length > 0) {
                chat.img = $scope.uploader.queue[0].image.src;
            }
            $scope.editChat(chat);
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
                templateUrl: 'AngularPart/Views/EditChat.html',
                controller: 'ProfileController',
                size: size
            });
            $scope.modalInstance.result.then(function (ctrl) {

            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    });