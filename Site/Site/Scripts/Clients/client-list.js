'use strict';
var ClientListCtrl = (function () {
    function ClientListCtrl($scope, $modal, $http, $location, $templateCache) {
        this.$scope = $scope;
        this.$http = $http;
        this.$modal = $modal;
        this.$location = $location;
        this.$templateCache = $templateCache;
        $scope.controller = this;
    }

    ClientListCtrl.prototype.init = function (configuration) {
        var _this = this;
        this.urlList = configuration;
        this.$scope.items = [];

        this.$http.get(this.urlList).then(function (args) {//"/DefaultActionApi/ClientApi/GetClients"
            _this.$scope.items = args.data.List;
            notifySuccess("Данные получены.");
        }).catch(function (e) {
            notifyError("Невозможно получить данные.", e);
        }).finally(function () {

        });
    };

    ClientListCtrl.prototype.clientEdit = function (id) {
        this.$scope.$parent.controller.$scope.currentItem = "Редактирование клиента";
        if (id == null)
            this.$location.path("/ClientEdit/");
        else
            this.$location.path("/ClientEdit/" + id);
    };

    ClientListCtrl.prototype.showRemoveDialog = function (id) {
        this.$scope.currentItem = this.$scope.items.filter(function (item) {
            return item.Id == id;
        })[0];
        if (this.$scope.currentItem) {
            this.dialog = this.$modal.open({ template: this.$templateCache.get("clientRemoveDialog.html"), scope: this.$scope });
        }
    };

    ClientListCtrl.prototype.clientRemove = function () {
        var _this = this;
        if (this.$scope.currentItem) {
            this.$http.delete("/api/ClientApi", { params: { id: _this.$scope.currentItem.Id } }).then(function () {
                var removedIndex = _this.$scope.items.indexOf(_this.$scope.currentItem);
                _this.$scope.items.splice(removedIndex, 1);
                notifySuccess("Items were deleted.");
            }).catch(function (e) {
                notifyError("Unable to delete item.", e);
            }).finally(function () {
                _this.closeDialog();
            });
        }
    };

    ClientListCtrl.prototype.closeDialog = function () {
        if (this.dialog != null) {
            this.dialog.close();
        }
        this.$scope.currentItem = null;
    };

    return ClientListCtrl;
})();

ClientListCtrl.$inject = ['$scope', '$modal', '$http', '$location', '$templateCache'];
var app = getOrCreateAngularModule("anykeyApp", ['ui.bootstrap', 'ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('ClientListCtrl', ClientListCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/ClientEdit/:id', {
            templateUrl: 'ClientEdit.html',
            controller: ClientEditCtrl
        }).when('/ClientEdit/', {
            templateUrl: 'ClientEdit.html',
            controller: ClientEditCtrl
        })
        .otherwise({ templateUrl: 'Clients.html', controller: ClientListCtrl });
    }
]);