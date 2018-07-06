(function () {
    var app = angular.module("TeamStanding", []);

    var TeamStanding = function ($scope, $http) {

        var onUserComplete = function (response) {
            $scope.players = response.data;
        };

        var onError = function (reason) {
            $scope.error = "Could not fetch the user";
        };

        $scope.init = function (teamstandingurl) {
            $scope.teamstandingurl = teamstandingurl;
            $http.get("/home/getteamstanding?teamstandingurl=" + $scope.teamstandingurl)
                .then(onUserComplete, onError);
        };

        $scope.message = "Angular is loaded";

    };

    app.controller("TeamStanding", TeamStanding);
}());