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

    function logout() {
        delete sessionStorage[userSession];
    }

    function getAuthHeaders() {
        var headers = {};
        if (sessionStorage[userSession]) {
            var currentUser = JSON.parse(sessionStorage[userSession]);

            if (currentUser) {
                headers['Authorization'] = 'Bearer ' + currentUser.access_token;
            }
        }

        return headers;
    }

    function getDataAboutMe(success, error) {
        var request = {
            method: "GET",
            url: baseServiceUrl + "/api/me",
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);

    }

    function getFriendRequests(success, error) {
        var request = {
            method: "GET",
            url: baseServiceUrl + '/api/me/requests',
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function searchUserByName(input, success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/users/search?searchTerm=' + input,
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function approveFriendRequest(id, success, error) {
        var request = {
            method: 'PUT',
            url: baseServiceUrl + '/api/me/requests/' + id + '?status=approved',
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function removeFriendRequest(id, success, error) {
        var request = {
            method: 'PUT',
            url: baseServiceUrl + '/api/me/requests/' + id + '?status=rejected',
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function getFriends(success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/me/friends',
            headers: getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    return {
        register: register,
        login: login,
        logout: logout,
        getDataAboutMe: getDataAboutMe,
        getFriendRequests: getFriendRequests,
        searchUserByName: searchUserByName,
        approveFriendRequest: approveFriendRequest,
        removeFriendRequest: removeFriendRequest,
        getFriends: getFriends
    }
});