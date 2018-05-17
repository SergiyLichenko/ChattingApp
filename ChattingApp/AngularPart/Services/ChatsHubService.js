'use strict';
app.factory('chatsHubService', ['$http', 'localStorageService','$q',
    function ($http, localStorageService, $q) {
    var chatsHub = {};
    chatsHub.messages = [];
    var signalRChatHub = {};

    chatsHub.start = function () {
        //signalRChatHub = $.connection.messageHub;

        //signalRChatHub.client.onMessage = chatsHub.onMessage;
        //signalRChatHub.client.onDeleteMessage = chatsHub.onDeleteMessage;
        //signalRChatHub.client.onUpdateMessage = chatsHub.onUpdateMessage;
        //signalRChatHub.client.onAddUserToChat = chatsHub.onAddUserToChat;
        //signalRChatHub.client.connected = function () {

        //}
    }

    chatsHub.setTokenCookie = function (token) {
        if (token) {
            document.cookie = "BearerToken=" + token + "; path=/";
        }
    }

    chatsHub.registerMe = function () {
        var loginData = localStorageService.get("authorizationData");

        signalRChatHub.server.registerMe(loginData.userName);
    }

    chatsHub.sendMessage = function (message) {
        signalRChatHub.server.sendMessage(message);
    };

    chatsHub.deleteMessage = function (message) {
        
        signalRChatHub.server.deleteMessage(message);
    };

    chatsHub.updateMessage = function (message) {
        message.user = {};
        message.user.userName = localStorageService.get("authorizationData").userName;

        signalRChatHub.server.updateMessage(message);
    };
    chatsHub.userToChat = function (chat, userName) {
        chat.img = null;
        signalRChatHub.server.userToChat(chat, userName);
    }
    chatsHub.onMessage = function (message) {
        chatsHub.messageCallback(message);
    };
    chatsHub.onDeleteMessage = function (message) {
        chatsHub.messageDeleteCallback(message);
    };
    chatsHub.onUpdateMessage = function (message) {
        chatsHub.messageUpdateCallback(message);
    };
    chatsHub.onAddUserToChat = function (user) {
        chatsHub.onAddUserToChatCallback(user);
    }
    return chatsHub;
}
])