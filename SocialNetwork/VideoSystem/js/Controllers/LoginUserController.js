/// <reference path="../libs/js/angular.js" />
/// <reference path="app.js" />
/// <reference path="../Services/authService.js" />

"use strict";

app.controller('LoginUserController', function ($scope, authService, $location) {
    $scope.login = function (userData) {
        authService.login(userData,
            function success() {
                console.log("Successfully Login");
                $location.path("/");
            },
            function error(err) {
                console.log(err);
            })
    }

});