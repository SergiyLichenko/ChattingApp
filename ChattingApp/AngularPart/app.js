var app = angular.module("ChattingApp",
    ["LocalStorageModule",
        "ui.bootstrap",
        "ui.router",
        "validation.match",
        "cgBusy",
        "angularFileUpload"]);
//, "", 'ngSanitize',
//    '', '', '', '', '', 'ng.httpLoader']);

app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider
        .state('login', {
            url: '/login',
            controller: 'LogInController',
            templateUrl: 'AngularPart/Views/logIn.html',
            navbarState: 'login'
        })
        .state('home', {
            url: '/home',
            controller: 'HomeController',
            templateUrl: 'AngularPart/Views/home.html',
            navbarState: 'home'
        })
        .state('signup', {
            url: '/signup',
            controller: 'SignUpController',
            templateUrl: 'AngularPart/Views/signUp.html',
            navbarState: 'signup'
        })
        .state('chats', {
            url: '/chats',
            reloadOnSearch: true,
            controller: 'ChatController',
            templateUrl: 'AngularPart/Views/chat.html',
            navbarState: 'chat'
        })
        .state('associate', {
            url: '/associate',
            controller: 'AssociateController',
            templateUrl: 'AngularPart/Views/association.html',
            navbarState: 'associate'
        })
         .state('profile', {
             url: '/profile',
             controller: 'ProfileController',
             templateUrl: 'AngularPart/Views/profile.html',
             navbarState: 'profile'
         })
        .state("otherwise",
        {
            url: "*path",
            controller: 'HomeController',
            templateUrl: 'AngularPart/Views/home.html',
            navbarState: 'home'
        });
}]);

app.constant('webMessengerSettings', {
    apiServiceBaseUri: '',
    clientId: 'SMARTMessenger'
});

app.run(["$rootScope", "$state", function ($rootScope, $state) {
    $rootScope.$on('$stateChangeStart',
        function (event, next) {
            if (next.name === 'signup' || next.name === 'login') return;
            $state.go('login');
        });
}]);

app.run(["$templateCache",
    function ($templateCache) {
        $templateCache.put("background.html");
    }
]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});