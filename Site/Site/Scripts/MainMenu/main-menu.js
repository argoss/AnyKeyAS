var MainMenu;
(function (MainMenu) {
    'use strict';
    var MainMenuCtrl = (function ($scope) {
        function MainMenuCtrl($scope, $http, $location) {
            this.$scope = $scope;
            this.$location = $location;
            this.$http = $http;
            $scope.controller = this;
        }

        MainMenuCtrl.prototype.init = function () {
            var _this = this;

        }

        MainMenuCtrl.prototype.UserList = function () {
            this.$location.path("Users/");
        };

        MainMenuCtrl.prototype.RequestList = function () {

        };

        MainMenuCtrl.prototype.ClientList = function () {

        };

        return MainMenuCtrl;
    })();

    MainMenu.MainMenuCtrl = MainMenuCtrl;
    MainMenuCtrl.$inject = ['$scope', '$http', '$location'];
    var app = angular.module("anykeyApp", ['ngRoute']);

    function configFunction($httpProvider) {
        $httpProvider.defaults.cache = false;
    }

    app.controller('MainMenuCtrl', MainMenuCtrl);

    /*app.config([
        '$routeProvider',
        function ($routeProvider) {
            $routeProvider.when('/Users', {
                templateUrl: 'Users.html',
                controller: Users.UserCtrl
            }).when('/Requests', {
                templateUrl: 'Requests.html',
                controller: Requests.RequestCtrl
            }).when('/Clients', {
                templateUrl: 'Clients.html',
                controller: Clients.ClientCtrl
            });
            ;
        }
    ]);*/
})(MainMenu || (MainMenu = {}));