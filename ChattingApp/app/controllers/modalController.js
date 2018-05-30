'use strict';

app.controller('ModalController',
    ['$uibModal', '$rootScope', '$timeout', 'userService',
        function ($uibModal, $rootScope, $timeout, userService) {
            var self = this;

            var modalConfig = {
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                size: 'lg'
            };

            self.openCreateChatModal = function () {
                modalConfig.templateUrl = 'app/views/modalCreateChat.html';
                modalConfig.controller = 'CreateChatModalController';

                $uibModal.open(modalConfig);
            }

            self.openEditChatModal = function (currentChat) {
                modalConfig.templateUrl = 'app/views/editChat.html';
                modalConfig.controller = 'EditChatModalController';
                modalConfig.resolve = {
                    currentChat: function () {
                        return currentChat;
                    }
                }

                $uibModal.open(modalConfig);
            }

            self.openJoinChatModal = function () {
                modalConfig.templateUrl = 'app/views/modalJoinChat.html';
                modalConfig.controller = 'JoinChatModalController';

                $uibModal.open(modalConfig);
            }

            self.openProfileModal = function (selectedUser) {
                modalConfig.templateUrl = 'app/views/profile.html';
                modalConfig.controller = 'ProfileModalController';
                modalConfig.resolve = {
                    selectedUser: function () {
                        return selectedUser;
                    }
                }
                var modalInstance = $uibModal.open(modalConfig);
                modalInstance.result.then(function (reload) {
                    if (!reload) return;

                    $rootScope.$$childHead.$$nextSibling.busyPromise = userService.getCurrent().then(function (user) {
                        $rootScope.$broadcast('onUserUpdate', user);
                    });
                    $timeout(function () { $rootScope.$$childHead.$$nextSibling.$apply(); });
                });
            }
        }]);