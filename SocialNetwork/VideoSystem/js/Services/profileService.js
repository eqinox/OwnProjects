app.factory('profileService', function ($http, baseServiceUrl, userSession, authService) {
    function getDataAboutMe(success, error) {
        var request = {
            method: "GET",
            url: baseServiceUrl + "/api/me",
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);

    }

    function getFriends(success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/me/friends',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function approveFriendRequest(id, success, error) {
        var request = {
            method: 'PUT',
            url: baseServiceUrl + '/api/me/requests/' + id + '?status=approved',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function removeFriendRequest(id, success, error) {
        var request = {
            method: 'PUT',
            url: baseServiceUrl + '/api/me/requests/' + id + '?status=rejected',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function getFriendRequests(success, error) {
        var request = {
            method: "GET",
            url: baseServiceUrl + '/api/me/requests',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function searchUserByName(input, success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/users/search?searchTerm=' + input,
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function getUserPreviewData(user, success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/users/' + user + '/preview',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function getFriendsofFriend(user, success, error) {
        var request = {
            method: 'GET',
            url: baseServiceUrl + '/api/users/' + user + '/friends/preview',
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    function sendFriendRequest(user, success, error) {
        var request = {
            method: 'POST',
            url: baseServiceUrl + '/api/me/requests/' + user,
            headers: authService.getAuthHeaders()
        };

        $http(request).success(success).error(error);
    }

    return {
        getDataAboutMe: getDataAboutMe,
        getFriends: getFriends,
        approveFriendRequest: approveFriendRequest,
        removeFriendRequest: removeFriendRequest,
        getFriendRequests: getFriendRequests,
        searchUserByName: searchUserByName,
        getUserPreviewData: getUserPreviewData,
        getFriendsofFriend: getFriendsofFriend,
        sendFriendRequest: sendFriendRequest
    }
});