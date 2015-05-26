/// <reference path="../../libs/js/angular.js" />
/// <reference path="../app.js" />

app.factory("authService", function ($http, baseServiceUrl, userSession) {
    function register(userData, success, error) {
        var request = {
            method: "POST",
            url: baseServiceUrl + "/api/users/Register",
            data: userData
        };

        $http(request).success(function (data) {
            sessionStorage[userSession] = JSON.stringify(data);
            success(data);
        }).error(error);
    }

    function login(userData, success, error) {
        $http({
            method: "POST",
            url: baseServiceUrl + "/api/users/Login",
            data: userData
        })
        .success(function (data) {
            sessionStorage[userSession] = JSON.stringify(data);
            success(data);
        })
        .error(error);
    }

    return {
        register: register,
        login: login
    }
});