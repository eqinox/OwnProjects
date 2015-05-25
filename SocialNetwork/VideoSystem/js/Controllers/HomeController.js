/// <reference path="../../libs/js/angular.js" />
/// <reference path="../app.js" />
/// <reference path="../Services/authService.js" />

"use strict";


app.controller("HomeController", function ($scope, authService, $location) {
    
    


    authService.getDataAboutMe(function success(data) {
        $scope.user = data;
        $scope.isLogged = true;
        //console.log($scope.user);
    },
    function error(err) {
        $scope.isLogged = false;
        console.log(err);
    });


    $scope.logout = function () {
        authService.logout();
        $scope.isLogged = false;
    }

    $scope.getFriendRequests = 
        authService.getFriendRequests(
            function success(data) {
            $scope.friendRequests = data;
            console.log($scope.friendRequests);
            $scope.messageCount = $scope.friendRequests.length;

            $scope.showMessage = $scope.friendRequests.length == 0 ? false : true;
        },
        function error(err) {
            console.log(err);
        });

    

    $scope.search = function () {
        authService.searchUserByName($scope.input,
            function success(data) {
                console.log(data);
            },
            function error(err) {
                console.log(err);
            });
    }

    $scope.accept = function (id) {
        authService.approveFriendRequest(id,
            function success(success) {
                console.log("successfuly accepted");
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
                console.log(err);
                console.log("error");
            });
    }

    $scope.decline = function (id) {
        authService.removeFriendRequest(id,
            function success() {
                console.log("successfully declined");
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
                console.log(err);
            });
    }

    getFriends();
    




    function getFriends() {
        authService.getFriends(function success(data) {
            $scope.friends = data;
            console.log(data);
        },
        function error(err) {
            console.log(err);
        });
    }

});

