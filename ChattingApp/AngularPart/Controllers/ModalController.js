app.controller('ModalController',
    ['$uibModal', 'chatService',
    function ($uibModal, chatService) {
        var self = this;

        var modalConfig = {
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            controllerAs: '$ctrl',
            size: 'lg'
        };

        self.openCreateChatModal = function () {
            modalConfig.templateUrl = 'AngularPart/Views/modalCreateChat.html';
            modalConfig.controller = 'CreateChatModalController';

            $uibModal.open(modalConfig);
        }

        self.openEditChatModal = function (currentChat) {
            modalConfig.templateUrl = 'AngularPart/Views/editChat.html';
            modalConfig.controller = 'EditChatModalController';
            modalConfig.resolve = {
                currentChat: function () {
                    return currentChat;
                }
            }

            $uibModal.open(modalConfig);
        }

        self.openJoinChatModal = function () {
            modalConfig.templateUrl = 'AngularPart/Views/modalJoinChat.html';
            modalConfig.controller = 'JoinChatModalController';
            modalConfig.resolve = {
                chats: function () {
                    return chatService.getAll().then(result => result.data);
                }
            }

            $uibModal.open(modalConfig);
        }

        self.openProfileModal = function (selectedUser) {
            modalConfig.templateUrl = 'AngularPart/Views/profile.html';
            modalConfig.controller = 'ProfileModalController';
            modalConfig.resolve = {
                selectedUser: function() {
                    return selectedUser;
                }
            }
            $uibModal.open(modalConfig);
        }
    }]);