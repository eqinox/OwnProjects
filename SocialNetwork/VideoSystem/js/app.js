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
    $routeProvider.otherwise({redirectTo: '/'})
});

