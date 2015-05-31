/// <autosync enabled="true" />
/// <reference path="../libs/js/angular.js" />
/// <reference path="requester.js" />
/// <reference path="app.js" />

app.controller("RegisterUserController", function ($scope, authService, $location, notifyService) {

    $scope.register = function () {
        authService.register($scope.userData,
            function success() {
                notifyService.showInfo("Register successfull");
                $location.path('/login');
            },
            function error(err) {
                notifyService.showError("Cannot Register", err);
            });
    }
});