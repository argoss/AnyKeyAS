'use strict';
var RequestListCtrl = (function () {
    function RequestListCtrl($scope, $modal, $http, $location, $templateCache) {
        this.$scope = $scope;
        this.$http = $http;
        this.$modal = $modal;
        this.$location = $location;
        this.$templateCache = $templateCache;
        $scope.controller = this;
    }

    RequestListCtrl.prototype.init = function (configuration) {
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

    RequestListCtrl.prototype.addRequest = function () {
        this.$scope.$parent.controller.$scope.currentItem = "Добавление заявки.";
        this.$location.path("/RequestAdd/");
    };

    RequestListCtrl.prototype.showRemoveDialog = function (id) {
        this.$scope.currentItem = this.$scope.items.filter(function (item) {
            return item.Id == id;
        })[0];
        if (this.$scope.currentItem) {
            this.dialog = this.$modal.open({ template: this.$templateCache.get("requestRemoveDialog.html"), scope: this.$scope });
        }
    };

    RequestListCtrl.prototype.requestRemove = function () {
        if (this.$scope.currentItem) {
            this.$http.delete(this.$scope.currentItem.Id).then(function () {
                var removedIndex = this.$scope.items.indexOf(this.$scope.currentItem);
                this.$scope.items.splice(removedIndex, 1);
                notifySuccess("Items were deleted.");
            }).catch(function (e) {
                notifyError("Unable to delete item.", e);
            }).finally(function () {
                this.closeDialog();
            });
        }
    };

    RequestListCtrl.prototype.closeDialog = function () {
        if (this.dialog != null) {
            this.dialog.close();
        }
        this.$scope.currentItem = null;
    };

    return RequestListCtrl;
})();

RequestListCtrl.$inject = ['$scope', '$modal', '$http', '$location', '$templateCache'];
var app = getOrCreateAngularModule("anykeyApp", ['ui.bootstrap', 'ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('RequestListCtrl', RequestListCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/RequestAdd/', {
            templateUrl: 'RequestAdd.html',
            controller: RequestListCtrl
        })
        .otherwise({ templateUrl: 'RequestAdd.html', controller: RequestListCtrl });
    }
]);