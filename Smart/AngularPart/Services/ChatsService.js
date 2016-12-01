﻿'use strict';
app.factory('chatsService', ['$location', '$http', 'localStorageService', '$q', function ($location, $http, localStorageService, $q) {

    var chatsServiceFactory = {};

    chatsServiceFactory.getChats = function () {
        var deferred = $q.defer();
        var loginData = localStorageService.get("authorizationData");
        if (loginData != null) {
            var uuid = guid();
            $http.get("api/Chats/" + loginData.userName + "/" + uuid).then(function (results) {
                deferred.resolve(results);
            });
        } else ($location.path('/login'));
        return deferred.promise;
    };

    chatsServiceFactory.createChat = function (chat) {
        var deferred = $q.defer();
        $.post("api/Chats/", chat)
            .done(function (result) {
                deferred.resolve(result);
            });
        return deferred.promise;
    };

    chatsServiceFactory.deleteChat = function (chat) {
        var deferred = $q.defer();
        chat.img = null;
        $http.post("api/Chats/delete", chat).then(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    chatsServiceFactory.addUserToChat = function (chatId, userName) {
        var deferred = $q.defer();
        var obj = {};
        obj.chatId = chatId;
        obj.userName = userName;

        $.post("api/user/ToChat", obj)
            .done(function (result) {
                deferred.resolve(result);
            });
        return deferred.promise;
    };

    chatsServiceFactory.getMessagesForChat = function (chatId) {
        var deferred = $q.defer();

        var uuid = guid();
        $.get("api/Messages/" + chatId, uuid).then(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    chatsServiceFactory.quitChat = function (chatId, username) {
        var deferred = $q.defer();
        var uuid = guid();
        if (!username) {
            username = localStorageService.get("authorizationData").userName;
        }
        var request = {
            chatId: chatId,
            username: username
        };
        $.post("api/Chats/quit/", request).then(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    chatsServiceFactory.uploadFile = function (data, userName) {
        var deferred = $q.defer();

        $http.post("api/user/FileUpload/", JSON.stringify({ photo: data, userName: userName }))
            .success(function () {
                deferred.resolve();
            });
        return deferred.promise;
    }

    chatsServiceFactory.makeFavourite = function (id) {
        var deferred = $q.defer();

        $.post("api/Messages/" + id).done(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    

    chatsServiceFactory.getAllChats = function () {
        var deferred = $q.defer();
        $http.get("api/Chats/All?" + guid()).then(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    chatsServiceFactory.editChat = function (chat) {
        var deferred = $q.defer();
        $http.post("api/Chats/edit", chat).then(function (results) {
            deferred.resolve(results);
        });
        return deferred.promise;
    };

    function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
          s4() + '-' + s4() + s4() + s4();
    }

    //for code insertion part. Not used right now, but may be useful in case of usage of modal window
    var currCode;

    var setCode = function (code) {
        currCode = code;
    }

    var getCode = function () {
        return currCode;
    }

    chatsServiceFactory.setCode = setCode;
    chatsServiceFactory.getCode = getCode;

    return chatsServiceFactory;

}]);