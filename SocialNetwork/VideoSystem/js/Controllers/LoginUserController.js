/// <reference path="../libs/js/angular.js" />
/// <reference path="app.js" />
/// <reference path="../Services/authService.js" />

"use strict";

app.controller('LoginUserController', function ($scope, authService, $location, notifyService) {
    $scope.login = function (userData) {
        authService.login(userData,
            function success() {
                notifyService.showInfo("Login successfull");
                $location.path("/");
            },
            function error(err) {
                notifyService.showError("Cannot login", err);
            });
    }

});