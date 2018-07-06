(function () {
    var app = angular.module("PlayerList", []);

    var PlayerDetails = function ($scope, $http) {
        
        var onUserComplete = function (response) {
            $scope.players = response.data;
        };

        var onError = function (reason) {
            $scope.error = "Could not fetch the user";
        };

        $scope.init = function (teamurl) {
            $scope.teamurl = teamurl;
            $scope.sortOrder = "name";  
            $http.get("/home/getplayerdata?teamurl=" + $scope.teamurl)
                .then(onUserComplete, onError);
        };

        $scope.add = function (item) {
            $http.get("/home/getteamdata?teamurl=" + item)
                .then(onUserComplete, onError);
        };

        $scope.message = "Angular is loaded"; 

    };

    app.controller("PlayerDetails", PlayerDetails);
}());