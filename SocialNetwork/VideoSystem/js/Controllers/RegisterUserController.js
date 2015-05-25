/// <autosync enabled="true" />
/// <reference path="../libs/js/angular.js" />
/// <reference path="requester.js" />
/// <reference path="app.js" />

app.controller("RegisterUserController", function ($scope, authService, $location) {

    $scope.register = function () {
        authService.register($scope.userData,
            function success() {
                $location.path('/login');
            },
            function error(err) {
                console.log(err);
            });
    }
})