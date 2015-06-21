'use strict';
var UserListCtrl = (function () {
    function UserListCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.controller = this;
    }

    UserListCtrl.prototype.init = function (configuration) {
        var _this = this;
        this.urlList = configuration;
        this.$scope.items = [];

        this.$http.get(this.urlList).then(function (args) {
            _this.$scope.items = args.data.List;
            notifySuccess("Items were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get items.", e);
        }).finally(function () {

        });
    };

    UserListCtrl.prototype.UserEdit = function (id) {
        this.$scope.$parent.controller.$scope.currentItem = "Редактирование данных пользователя";
        if (id == null)
            this.$location.path("/UserEdit/");
        else
            this.$location.path("/UserEdit/" + id);
    };

    return UserListCtrl;
})();

UserListCtrl.$inject = ['$scope', '$http', '$location'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('UserListCtrl', UserListCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/UserEdit/:id', {
            templateUrl: 'UserEdit.html',
            controller: UserEditCtrl
        }).when('/UserEdit/', {
            templateUrl: 'UserEdit.html',
            controller: UserEditCtrl
        })
        .otherwise({ templateUrl: 'UserEdit.html', controller: UserEditCtrl });
    }
]);