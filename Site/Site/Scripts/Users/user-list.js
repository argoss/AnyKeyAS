'use strict';
var UserListCtrl = (function () {
    function UserListCtrl($scope, $modal, $http, $location, $templateCache) {
        this.$scope = $scope;
        this.$http = $http;
        this.$modal = $modal;
        this.$location = $location;
        this.$templateCache = $templateCache;
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
    UserListCtrl.prototype.userEdit = function (id) {
        this.$scope.$parent.controller.$scope.currentItem = "Редактирование данных пользователя";
        this.$location.path("/UserEdit/" + id);
    };
    UserListCtrl.prototype.userCreate = function () {
        this.$scope.$parent.controller.$scope.currentItem = "Создание нового пользователя";
        this.$location.path("/UserAdd/");
    };
    UserListCtrl.prototype.showRemoveDialog = function (id) {
        this.$scope.currentItem = this.$scope.items.filter(function (item) {
            return item.Id == id;
        })[0];
        if (this.$scope.currentItem) {
            this.dialog = this.$modal.open({ template: this.$templateCache.get("userRemoveDialog.html"), scope: this.$scope });
        }
    };
    UserListCtrl.prototype.userRemove = function () {
        var _this = this;
        if (this.$scope.currentItem) {
            this.$http.delete("/api/UserApi", { params: { id: _this.$scope.currentItem.Id } }).then(function () {
                var removedIndex = _this.$scope.items.indexOf(_this.$scope.currentItem);
                _this.$scope.items.splice(removedIndex, 1);
                alert("Items were deleted.");
            }).catch(function (e) {
                notifyError("Unable to delete item.", e);
            }).finally(function () {
                _this.closeDialog();
            });
        }
    };
    UserListCtrl.prototype.closeDialog = function () {
        if (this.dialog != null) {
            this.dialog.close();
        }
        this.$scope.currentItem = null;
    };

    return UserListCtrl;
})();

UserListCtrl.$inject = ['$scope', '$modal', '$http', '$location', '$templateCache'];
var app = getOrCreateAngularModule("anykeyApp", ['ui.bootstrap', 'ngRoute']);

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
        }).when('/UserAdd/', {
            templateUrl: 'UserAdd.html',
            controller: UserCreateCtrl
        });
    }
]);