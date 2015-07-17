'use strict';
var UserCreateCtrl = (function () {
    function UserCreateCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.controller = this;
    }

    UserCreateCtrl.prototype.init = function () {
        this.$scope.slideUp = false;
        var _this = this;
        this.$scope.roles = { }

        this.$http.get("/api/act/UserApi/GetRoleList").then(function (args) {
            _this.$scope.roles = args.data;
        }).catch(function (e) {
            notifyError("Unable to get roles.", e);
        });

        this.$scope.user = {
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
        };

        this.$scope.selectedRole = {};
    };

    UserCreateCtrl.prototype.save = function () {
        var _this = this;
        this.$http.post("/api/act/UserApi/Create", this.$scope.user).then(function () {
            _this.$location.path("");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        }).finally(function () {
            this.$location.path("");
        });
    };

    UserCreateCtrl.prototype.addRole = function (role) {
        var target = this.$scope.roles.filter(function (item) { return item == role; })[0];
        if (target == null)
            return;

        this.$scope.user.Roles.add(target);

        var removedIndex = this.$scope.items.indexOf(target);
        this.$scope.roles.splice(removedIndex, 1);
    }

    UserCreateCtrl.prototype.changePosition = function (user, position) {
        user.Position = position;
    }

    UserCreateCtrl.prototype.cancel = function () {
        this.$location.path("");
    }

    UserCreateCtrl.prototype.slideUpDown = function () {
        if (!this.$scope.slideUp) {
            $(".rolesBlock").slideUp();
            this.$scope.slideUp = true;
        } else {
            $(".rolesBlock").slideDown();
            this.$scope.slideUp = false;
        }
    }

    return UserCreateCtrl;
})();

UserCreateCtrl.$inject = ['$scope', '$http', '$location'];
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