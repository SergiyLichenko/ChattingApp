app.controller('ChatListController',
    ['$scope', 'chatService', 'localStorageService',
        function ($scope, chatService, localStorageService) {

            $scope.currentUser = localStorageService.get('user');

            $scope.selectChat = function (chat) {
                $scope.messagesBusyPromise = chatService.getById(chat.id).then(function (result) {
                    $scope.$emit('onSelectChat', result.data);
                });
            };

            if ($scope.currentUser.chats.length > 0)
                $scope.selectChat($scope.currentUser.chats[0]);
        }]);