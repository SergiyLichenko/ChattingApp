﻿'use strict';
app.controller('LogInController',
    ['checkLoginNotService','$scope', '$state', 'authService', 'chatsHubService', 'webMessengerSettings', 'localStorageService','checkLoginService',
    function (checkLoginNotService,$scope, $state, authService, chatsHubService, webMessengerSettings, localStorageService, checkLoginService) {
        checkLoginService.checkLogin();
        checkLoginNotService.checkLogin();
        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.message = "";

        $scope.login = function () {

            authService.login($scope.loginData).then(function (response) {
                localStorageService.set("authorizationData", {
                    userName: $scope.loginData.userName
                });
                $state.go('chats');
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };

        $scope.authExternalProvider = function (provider) {

            var redirectUri = location.protocol + '//' + location.host + '/AngularPart/Views/authcomplete.html';

            var externalProviderUrl = webMessengerSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                        + "&response_type=token&client_id=" + webMessengerSettings.clientId
                                                                        + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function (fragment) {

            $scope.$apply(function () {

                if (fragment.haslocalaccount === 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    $location.path('/associate');

                } else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function (response) {

                        $location.path('/chats');

                    },
                        function (err) {
                            $scope.message = err.error_description;
                        });
                }

            });
        };
    }]);