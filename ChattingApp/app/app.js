var app = angular.module('chattingApp',
    ['LocalStorageModule',
        'ngMaterial',
        'ui.bootstrap',
        'ui.router',
        'validation.match',
        'SignalR',
        'ngFileUpload']);

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
                templateUrl: 'app/views/logIn.html',
                navbarState: 'login'
            })
        .state('signup',
            {
                url: '/signup',
                controller: 'SignUpController',
                templateUrl: 'app/views/signUp.html',
                navbarState: 'signup'
            })
        .state('chat',
            {
                url: '/chat',
                reloadOnSearch: true,
                controller: 'ChatListController',
                templateUrl: 'app/views/chat.html',
                navbarState: 'chat'
            })
        .state('profile',
            {
                url: '/profile',
                controller: 'ProfileModalController',
                templateUrl: 'app/views/profile.html',
                navbarState: 'profile'
            })
        .state('404', {
            url: '*path',
            templateUrl: 'app/views/404.html'
        });
}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});