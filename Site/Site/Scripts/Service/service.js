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
        this.urlList = configuration;
        this.$scope.items = [];

        this.$http.get(this.urlList).then(function (args) {//"/DefaultActionApi/ClientApi/GetClients"
            _this.$scope.items = args.data.List;
            notifySuccess("Items were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get items.", e);
        }).finally(function () {

        });
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