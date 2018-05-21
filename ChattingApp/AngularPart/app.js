var app = angular.module('chattingApp',
    ['LocalStorageModule',
        'ngMaterial',
        'ui.bootstrap',
        'ui.router',
        'validation.match',
        'cgBusy',
        'SignalR',
        'ngFileUpload']);
//, '', 'ngSanitize',
//    '', '', '', '', '', 'ng.httpLoader']);

app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider
        .state('root',
            {
                url: '/',
                redirectTo: 'chat'
            })
        .state('login',
            {
                url: '/login',
                controller: 'LogInController',
                templateUrl: 'AngularPart/Views/logIn.html',
                navbarState: 'login'
            })
        .state('signup',
            {
                url: '/signup',
                controller: 'SignUpController',
                templateUrl: 'AngularPart/Views/signUp.html',
                navbarState: 'signup'
            })
        .state('chat',
            {
                url: '/chat',
                reloadOnSearch: true,
                controller: 'ChatListController',
                templateUrl: 'AngularPart/Views/chat.html',
                navbarState: 'chat'
            })
        .state('profile',
            {
                url: '/profile',
                controller: 'ProfileController',
                templateUrl: 'AngularPart/Views/profile.html',
                navbarState: 'profile'
            }).state('404', {
                url: '*path',
                templateUrl: 'AngularPart/Views/404.html'
            });
}]);

app.run(['$rootScope', '$state', function ($rootScope, $state) {
    $rootScope.$on('$stateChangeStart',
        function (event, next) {
            if (next.name === 'signup' || next.name === 'login') return;
            $state.go('login');
        });
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});