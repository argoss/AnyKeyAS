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
        var _this = this;
        this.$scope.user = {};

        this.$http.get("/api/act/UserApi/GetUser", { params: { id: this.id } }).then(function (args) {
            _this.$scope.user = args.data;
            notifySuccess("Item were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get item.", e);
        }).finally(function () {

        });
    };

    UserEditCtrl.prototype.save = function () {
        this.$http.post("/api/UserApi", this.$scope.user).then(function () {
            this.$location.path("");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    UserEditCtrl.prototype.cancel = function () {
        this.$location.path("");
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