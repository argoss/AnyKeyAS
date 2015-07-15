'use strict';
var ServiceCtrl = (function () {
    function ServiceCtrl($scope, $modal, $http, $location, $templateCache) {
        this.$scope = $scope;
        this.$http = $http;
        this.$modal = $modal;
        this.$location = $location;
        this.$templateCache = $templateCache;
        $scope.controller = this;
    }

    ServiceCtrl.prototype.init = function (configuration) {
        var _this = this;
        this.apiCfg = configuration;
        this.$scope.items = [];
        this.$scope.requests = [];
        this.$scope.query = [];

        this.$http.get(_this.apiCfg.RequestUrl).then(function (args) {//"/DefaultActionApi/ClientApi/GetClients"
            _this.$scope.items = args.data.List;
            _this.$scope.requests = args.data.List;
            notifySuccess("Items were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get items.", e);
        }).finally(function () {

        });
    };

    ServiceCtrl.prototype.filter = function(filter) {
        var status = this.$scope.query.filter(function (item) {
            return item == filter;
        })[0];
        if (status == null) {
            this.$scope.query.add(status);
        } else {
            this.$scope.query.remove(status);
        }

        this.$scope.requests = this.$scope.requests.filter(function (item) {            
            return this.$scope.query.filter(function (q) { return q == item.Status; }).length > 0;
        });
    };

    ServiceCtrl.prototype.date2IsoStr = function (date) {
        return date2IsoStr(date);
    };

    return ServiceCtrl;
})();

ServiceCtrl.$inject = ['$scope', '$modal', '$http', '$location', '$templateCache'];
var app = getOrCreateAngularModule("anykeyApp", ['ui.bootstrap', 'ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('ServiceCtrl', ServiceCtrl);

/*app.config([
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
]);*/