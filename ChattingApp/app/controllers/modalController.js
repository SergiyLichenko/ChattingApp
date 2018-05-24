﻿'use strict';

app.controller('ModalController',
    ['$uibModal', 'chatService',
    function ($uibModal, chatService) {
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
            modalConfig.resolve = {
                chats: function () {
                    return chatService.getAll().then(result => {
                        var chats = result.data;
                        for(var chat of chats)
                            if (!chat.users) chat.users = [];

                        return chats;
                    });
                }
            }

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
            $uibModal.open(modalConfig);
        }
    }]);