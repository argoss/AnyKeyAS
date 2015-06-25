﻿'use strict';
var UserCreateCtrl = (function () {
    function UserCreateCtrl($scope, $http, $location, $routeParams) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.id = $routeParams.id;
        $scope.controller = this;
    }

    UserCreateCtrl.prototype.init = function () {
        var _this = this;

        this.$http.get("/api/act/UserApi/GetCreateModel").then(function (args) {
            _this.$scope.user = args.data;
        }).catch(function (e) {
            notifyError("Unable to get model.", e);
        }).finally(function () {

        });

        /*this.$scope.user = {
            UserName: "",
            FirstName: "",
            Name: "",
            Patronymic: "",
            Password: "",
            ConfirmPassword: "",
            Position: "",
            Email: "",
            Phone: "",
            Roles: []
        };*/
    };

    UserCreateCtrl.prototype.addUser = function () {
        this.$http.post("/api/act/UserApi/Create", this.$scope.user).then(function () {
            this.$location.path("");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    UserCreateCtrl.prototype.cancel = function () {
        this.$location.path("");
    }

    return UserCreateCtrl;
})();

UserCreateCtrl.$inject = ['$scope', '$http', '$location', '$routeParams'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('UserCreateCtrl', UserCreateCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Users', {
            templateUrl: 'Users.html',
            controller: UserListCtrl
        })
        .otherwise({ templateUrl: 'Users.html', controller: UserListCtrl });
    }
]);