/// <reference path="../../libs/js/angular.js" />
/// <reference path="../app.js" />

app.factory("authService", function ($http, baseServiceUrl, userSession) {
    

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
        login: login
    }
});