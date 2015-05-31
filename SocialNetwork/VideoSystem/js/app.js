var app = angular.module("SocialNetwork", ['ngRoute', 'ngResource']);


app.constant("baseServiceUrl", "http://softuni-social-network.azurewebsites.net");
app.constant("userSession", "currentUser");

app.config(function ($routeProvider) {


    $routeProvider.when('/', {
        templateUrl: 'views/home.html',
        controller: 'HomeController'
    });

    $routeProvider.when('/login', {
        templateUrl: 'views/login.html',
        controller: 'LoginUserController'
    });

    $routeProvider.when('/register', {
        templateUrl: 'views/register.html',
        controller: 'RegisterUserController'
    });

    $routeProvider.when('/friends', {
        templateUrl: 'views/partials/full-friend-page.html',
        controller: 'FriendPageController'
    });

    $routeProvider.when('/friends/:id', {
        templateUrl: 'views/friend-page.html',
        controller: 'FriendPageController'
    });

    $routeProvider.otherwise({redirectTo: '/'})
});

