var app = angular.module("ChattingApp",
    ["LocalStorageModule",
        "ui.bootstrap",
        "ui.router",
        "validation.match",
        "cgBusy",
        "angularFileUpload"]);
//, "", 'ngSanitize',
//    '', '', '', '', '', 'ng.httpLoader']);

app.run(["$templateCache",
  function ($templateCache) {
      $templateCache.put("background.html");
  }
]);
app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider
        .state('login', {
            url: '/login',
            controller: 'LogInController',
            templateUrl: 'AngularPart/Views/LogIn.html',
            navbarState: 'login'
        })
        .state('home', {
            url: '/home',
            controller: 'HomeController',
            templateUrl: 'AngularPart/Views/Home.html',
            navbarState: 'home'
        })
        .state('signup', {
            url: '/signup',
            controller: 'SignUpController',
            templateUrl: 'AngularPart/Views/SignUp.html',
            navbarState: 'signup'
        })
        .state('chats', {
            url: '/chats',
            reloadOnSearch: true,
            controller: 'ChatsController',
            templateUrl: 'AngularPart/Views/Chats.html',
            navbarState: 'chats'
        })
        .state('associate', {
            url: '/associate',
            controller: 'AssociateController',
            templateUrl: 'AngularPart/Views/Association.html',
            navbarState: 'associate'
        })
         .state('profile', {
             url: '/profile',
             controller: 'ProfileController',
             templateUrl: 'AngularPart/Views/Profile.html',
             navbarState: 'profile'
         })
        .state("otherwise",
        {
            url: "*path",
            controller: 'HomeController',
            templateUrl: 'AngularPart/Views/Home.html',
            navbarState: 'home'
        });
}]);

app.constant('webMessengerSettings', {
    apiServiceBaseUri: '',
    clientId: 'SMARTMessenger'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('AuthInterceptorService');
});

app.run(["authService", function (authService) {
    authService.fillAuthData();
}]);