'use strict';
var UserEditCtrl = (function () {
    function UserEditCtrl($scope, $http, $location, $routeParams) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.id = $routeParams.id;
        $scope.controller = this;
    }

    UserEditCtrl.prototype.init = function () {
        this.$scope.slideUp = false;

        var _this = this;
        this.$scope.user = {};

        this.$http.get("/api/act/UserApi/GetUser", { params: { id: _this.$scope.id } }).then(function (args) {
            _this.$scope.user = args.data;
            notifySuccess("Данные получены.");
        }).catch(function (e) {
            notifyError("Невозможно получить данные.", e);
        }).finally(function () {

        });
    };

    UserEditCtrl.prototype.changePosition = function (user, position) {
        user.Position = position;
    }

    UserEditCtrl.prototype.saveUser = function () {
        var _this = this;
        this.$http.post("/api/act/UserApi/Edit", this.$scope.user).then(function () {
            _this.$location.path("");
            notifySuccess("Item saved successfully.");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    UserEditCtrl.prototype.cancel = function () {
        this.$location.path("");
    }

    UserEditCtrl.prototype.slideUpDown = function () {
        if (!this.$scope.slideUp) {
            $(".rolesBlock").slideUp();
            this.$scope.slideUp = true;
        } else {
            $(".rolesBlock").slideDown();
            this.$scope.slideUp = false;
        }
    }

    return UserEditCtrl;
})();

UserEditCtrl.$inject = ['$scope', '$http', '$location', '$routeParams'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('UserEditCtrl', UserEditCtrl);

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