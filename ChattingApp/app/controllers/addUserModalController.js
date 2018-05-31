'use strict';

app.controller('AddUserModalController',
    ['$scope', '$uibModalInstance', 'chatHubService', 'userService', 'selectedChat',
        function ($scope, $uibModalInstance, chatHubService, userService, selectedChat) {
            $scope.selectedChat = selectedChat;

            $scope.ok = function (selectedUser) {
                selectedChat.users.push(selectedUser);
                $scope.addUserBusyPromise = chatHubService.update(selectedChat)
                    .then($uibModalInstance.close);
            }

            $scope.cancel = function () {
                $uibModalInstance.close();
            }

            $scope.addUserBusyPromise = userService.getAll().then(result => {
                $scope.users = [];

                for (var user of result.data) {
                    var contains = $scope.selectedChat.users.some(x => x.id === user.id);
                    if (!contains) $scope.users.push(user);
                }
            });
        }]);