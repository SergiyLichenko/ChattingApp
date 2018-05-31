app.controller('ChatListController',
    ['$scope', '$timeout', 'chatService', 'localStorageService', 'userService', 'messageHubService', 'chatHubService',
        function ($scope, $timeout, chatService, localStorageService, userService, messageHubService, chatHubService) {

            $scope.$on('onUserUpdate', function (event, user) {
                $scope.currentUser = user;
                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatCreateAsync', function (event, chat) {
                $scope.currentUser.chats.push(chat);
                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatUpdateAsync', function (event, chat) {
                var existingChatIndex = $scope.currentUser.chats.findIndex(x => x.id === chat.id);
                if (existingChatIndex === -1)
                    $scope.currentUser.chats.push(chat);
                else
                    Object.assign($scope.currentUser.chats[existingChatIndex], chat);

                $timeout(function () { $scope.$apply(); });
            });

            $scope.$on('onChatDeleteAsync', function (event, chatId) {
                var existingChatIndex = $scope.currentUser.chats.findIndex(x => x.id === chatId);
                if (existingChatIndex === -1) return;

                $scope.currentUser.chats.splice(existingChatIndex, 1);
                if ($scope.currentUser.chats.length > 0)
                    $scope.selectChat($scope.currentUser.chats[0]);
                $timeout(function () { $scope.$apply(); });
            });

            $scope.selectChat = function (chat) {
                $scope.busyPromise = chatService.getById(chat.id).then(function (result) {
                    $scope.$emit('onSelectChat', result.data);
                });
            };

            var onLoad = function () {
                $scope.busyPromise = userService.getCurrent().then(function (result) {
                    localStorageService.set('user', result.data);
                    $scope.currentUser = result.data;

                    if (!$scope.currentUser.chats || $scope.currentUser.chats.length === 0) return;

                    $scope.selectChat($scope.currentUser.chats[0]);
                    $timeout(function () { $scope.$apply(); });
                });
                messageHubService.start();
                chatHubService.start();
            };

            onLoad();
        }]);