/*'use strict';
var ServiceCtrl = (function () {
    function ServiceCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$location = $location;
        this.$http = $http;
        $scope.controller = this;
    }

    ServiceCtrl.prototype.init = function () {
        var _this = this;

    }

    return ServiceCtrl;
})();

ServiceCtrl.$inject = ['$scope', '$http', '$location'];
var app = angular.module("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

app.controller('ServiceCtrl', ServiceCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Users', {
            templateUrl: 'Users.html',
            controller: UserListCtrl
        });
    }
]);*/