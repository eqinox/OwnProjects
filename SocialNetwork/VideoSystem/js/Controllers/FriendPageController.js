app.controller('FriendPageController', function ($scope, profileService, $routeParams, $location, notifyService) {

    if ($routeParams.id != undefined) {

        profileService.getDataAboutMe(function success(data) {
            $scope.isLogged = true;
            $scope.user = data;
        },
        function error(err) {
            $scope.isLogged = false;
        });

        profileService.getUserPreviewData($routeParams.id,
            function success(data) {
                $scope.isFriend = data.isFriend;
                $scope.friend = data;
                profileService.getFriendsofFriend($routeParams.id,
                    function success(data) {
                        $scope.friends = data;
                        console.log(data);
                    },
                    function error(err) {
                        console.log(err);
                    });
            },
            function error(err) {
                $scope.isFriend = false;
                $location.path('/');
            });


    }
    else {
        profileService.getDataAboutMe(function success(data) {
            $scope.user = data;
            $scope.nameForHeader = data;
        },
        function error(err) {
            console.log(err);
        });

        profileService.getFriends(function success(data) {
            $scope.friends2   = data;
            $scope.isLogged = true;
            //console.log($scope.user);
        },
        function error(err) {
            $scope.isLogged = false;
            console.log(err);
        });

    }

    $scope.viewProfile = function (id) {
        $location.path('/friends/' + id);
    };

    $scope.sendFriendRequest = function (username) {
        profileService.sendFriendRequest(username,
            function success(data) {
                notifyService.showInfo('Request sended successfully');
            },
            function error(err) {
                notifyService.showError("Cannot send friend request", err);
            });
    };

});