'use strict';
var MainMenuCtrl = (function () {
    function MainMenuCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$location = $location;
        this.$http = $http;
        $scope.controller = this;
    }

    MainMenuCtrl.prototype.init = function () {
        var _this = this;
        _this.$scope.currentItem = "";
    }

    /*MainMenuCtrl.prototype.Services = function () {
        
    };*/

    MainMenuCtrl.prototype.UserList = function () {
        this.$scope.currentItem = "Пользователи";
        this.$location.path("/Users");
    };

    MainMenuCtrl.prototype.RequestList = function () {
        this.$scope.currentItem = "Заявки";
        this.$location.path("/Requests");
    };

    MainMenuCtrl.prototype.ClientList = function () {
        this.$scope.currentItem = "Клиенты";
        this.$location.path("/Clients");
    };

    return MainMenuCtrl;
})();

MainMenuCtrl.$inject = ['$scope', '$http', '$location'];
var app = angular.module("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

app.controller('MainMenuCtrl', MainMenuCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Users', {
            templateUrl: 'Users.html',
            controller: UserListCtrl
        }).when('/Requests', {
            templateUrl: 'Requests.html',
            controller: RequestListCtrl
        }).when('/Clients', {
            templateUrl: 'Clients.html',
            controller: ClientListCtrl
        });
    }
]);