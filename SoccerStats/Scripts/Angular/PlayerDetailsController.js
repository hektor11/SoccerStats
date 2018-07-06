(function () {
    var app = angular.module("PlayerList", []);

    var PlayerDetails = function ($scope, $http) {
        
        var onUserComplete = function (response) {
            $scope.players = response.data;
        };

        var onError = function (reason) {
            $scope.error = "Could not fetch the user";
        };

        var onTeamComplete = function (response) {
            $scope.teams = response.data;
        };

        $scope.init = function (teamurl, teamstandingurl) {
            $scope.teamurl = teamurl;
            $scope.teamstandingurl = teamstandingurl;
            $scope.sortOrder = "name";
            $http.get("/home/getplayerdata?teamurl=" + $scope.teamurl)
                .then(onUserComplete, onError);

            $http.get("/home/getteamstanding?teamstandingurl=" + $scope.teamstandingurl)
                .then(onTeamComplete, onError);

        };

        $scope.add = function (item, item2) {
            $http.get("/home/getteamdata?teamurl=" + item +"&compId=" + item2)
                .then(onUserComplete, onError);
        };

        $scope.message = "Angular is loaded"; 

    };

    app.controller("PlayerDetails", PlayerDetails);
}());