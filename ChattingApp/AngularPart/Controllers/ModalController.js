app.controller('MenuController',
    ['$uibModal',
    function ($uibModal) {
        var self = this;
        var modalConfig = {
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            controllerAs: '$ctrl',
            size: 'lg'
            //resolve: {
            //    userName: function() {
            //        return $ctrl.userName;
            //    }
            //}
        };
        self.openCreateChatModal = function () {
            modalConfig.templateUrl = 'AngularPart/Views/modalCreateChat.html';
            modalConfig.controller = 'CreateChatModalController';

            var modalInstance = $uibModal.open(modalConfig);

            modalInstance.result.then(function (ctrl) {

                $scope.$parent.chats = $scope.$parent.createChat({
                    title: ctrl.title,
                    authorName: ctrl.userName,
                    img: ctrl.uploader.queue[0].image.src
                });
            });
        }
    }]);

app.controller('CreateChatModalController', ['$scope', 'chatHubService', '$uibModalInstance', 'userService', 'chatService',
    function ($scope, chatHubService, $uibModalInstance, userService, chatService, Upload) {
        var self = this;

        self.addUserToChat = function (chat) {
            self.chat = chat;
            self.ok();
        };
        self.getUserInfo = function (name) {
            var data = userService.getUserByName(name);
            self.email = data.email;
        }
        self.ok = function () {
            var reader = new FileReader();
            reader.addEventListener("load", function () {
                chatService.createChat({
                    title: $scope.chatTitle,
                    img: reader.result
                }).then(function() {
                    $uibModalInstance.close(self);
                });
            }, false);
            reader.readAsDataURL($scope.file);
        };

        self.cancel = function () {
            $uibModalInstance.dismiss();
        };


    }]);








app.controller('ModalController', ['$scope', '$uibModal', '$log', 'userService',
    function ($scope, $uibModal, $log) {
        var $ctrl = this;
        $ctrl.userName = $scope.$parent.getCurrentUsername();
        $ctrl.animationsEnabled = true;

        $ctrl.open = function (size, type) {
            var modalInstance = null;
            if (type === 'join') {
                modalInstance = $uibModal.open({
                    animation: $ctrl.animationsEnabled,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'AngularPart/Views/ModalJoinChat.html',
                    controller: 'ModalInstanceCtrl',
                    controllerAs: '$ctrl',
                    size: size,
                    resolve: {
                        userName: function () {
                            return $ctrl.userName;
                        }
                    }
                });
                modalInstance.result.then(function (ctrl) {
                    $scope.$parent.addUserToChat(ctrl.chat, ctrl.userName);
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            }
            if (type === 'create') {
                
            }


        };

        $ctrl.openComponentModal = function () {
            var modalInstance = $uibModal.open({
                animation: $ctrl.animationsEnabled,
                component: 'modalComponent',
                resolve: {
                    code: function () {
                        return $ctrl.code;
                    }
                }
            });

            modalInstance.result.then(function (userName) {
                $ctrl.userName = userName;
            }, function () {
                $log.info('modal-component dismissed at: ' + new Date());
            });
        };

        $ctrl.toggleAnimation = function () {
            $ctrl.animationsEnabled = !$ctrl.animationsEnabled;
        };
    }]);

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

app.controller('ModalInstanceCtrl', ['chatHubService', '$uibModalInstance', 'userName', 'userService', 'chatService', 'FileUploader',
    function (chatHubService, $uibModalInstance, userName, userService, chatService, FileUploader) {
        var $ctrl = this;
        $ctrl.userName = userName;
        //$.connection.hub.start()
        //    .done(function (data) {
        //        if (data && data.token) {
        //            chatHubService.setTokenCookie(data.token);
        //            chatHubService.registerMe();
        //        }
        //    });
        //$ctrl.busyPromise = chatService.getAllChats().then(function (result) {
        //    $ctrl.chats = result.data;
        //});
        $ctrl.uploader = new FileUploader({
            queueLimit: 1
        });
        $ctrl.addUserToChat = function (chat) {
            $ctrl.chat = chat;
            $ctrl.ok();
        };
        $ctrl.getUserInfo = function (name) {
            var data = userService.getUserByName(name);
            $ctrl.email = data.email;
        }
        $ctrl.ok = function () {
            $uibModalInstance.close($ctrl);
        };

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


    }]);

app.component('modalComponent', {
    templateUrl: 'Modal.html',
    bindings: {
        resolve: '<',
        close: '&',
        dismiss: '&'
    },
    controller: function () {
        var $ctrl = this;

        $ctrl.$onInit = function () {
            $ctrl.userName = $ctrl.resolve.userName;
        };

        $ctrl.ok = function () {
            $ctrl.close({ $value: $ctrl.userName });
        };

        $ctrl.cancel = function () {
            $ctrl.dismiss({ $value: 'cancel' });
        };
    }
});
// Please note that the close and dismiss bindings are from $uibModalInstance.
