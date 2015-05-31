/// <reference path="../../libs/js/angular.js" />
/// <reference path="../app.js" />
/// <reference path="../Services/authService.js" />

"use strict";


app.controller("HomeController", function ($scope, authService, $location, profileService, notifyService) {
    
    $scope.showNormalHomeView = true;

    profileService.getDataAboutMe(function success(data) {
        $scope.user = data;
        $scope.isLogged = true;
    },
    function error(err) {
        $scope.isLogged = false;
    });


    $scope.logout = function () {
        notifyService.showInfo("Successfully Logout");
        authService.logout();
        $scope.isLogged = false;
    }

    $scope.getFriendRequests = 
        profileService.getFriendRequests(
            function success(data) {
            $scope.friendRequests = data;
            console.log($scope.friendRequests);
            $scope.messageCount = $scope.friendRequests.length;

            $scope.showMessage = $scope.friendRequests.length == 0 ? false : true;
        },
        function error(err) {
            console.log(err);
        });

    

    $scope.search = function (value) {
        profileService.searchUserByName(value,
            function success(data) {
                console.log(data);
                $scope.searchFriends = data;
                $scope.showSearchFriends = true;
            },
            function error(err) {
                $scope.showSearchFriends = false;
                console.log(err);
            });
    }

    $scope.viewProfile = function (username) {
        profileService.getUserPreviewData(username,
            function success(data) {
                console.log(data);
            },
            function error(err) {
                console.log(err);
            });
        console.log(username);
    }

    $scope.accept = function (id) {
        profileService.approveFriendRequest(id,
            function success(success) {
                notifyService.showInfo("Successfully accepted");
                getFriends();
                authService.getFriendRequests(
                    function success(data) {
                        $scope.friendRequests = data;
                        $scope.messageCount = $scope.friendRequests.length;
                        $scope.showMessage = $scope.friendRequests.length == 0 ? false : true;
                    },
                    function error(err) {
                        console.log(err);
                    });
            },
            function error(err) {
                notifyService.showError("Cannot Accept", err);
            });
    }

    $scope.decline = function (id) {
        profileService.removeFriendRequest(id,
            function success() {
                notifyService.showInfo("Successfully declined");
                authService.getFriendRequests(
                    function success(data) {
                        $scope.friendRequests = data;
                        $scope.messageCount = $scope.friendRequests.length;
                        $scope.showMessage = $scope.friendRequests.length == 0 ? false : true;
                    },
                    function error(err) {
                        notifyService.showError("Cannot decline", err);
                    });
            },
            function error(err) {
                console.log(err);
            });
    }

    $scope.viewProfile = function (id) {
        $location.path('/friends/' + id);
    }

    getFriends();

    function getFriends() {
        profileService.getFriends(function success(data) {
            $scope.friends = data;
            console.log(data);
        },
        function error(err) {
            console.log(err);
        });
    }

    $scope.showFullFriend = function () {
        $scope.showNormalHomeView = false;
    }

});

