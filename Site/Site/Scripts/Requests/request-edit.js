'use strict';
var RequestEditCtrl = (function () {
    function RequestEditCtrl($scope, $http, $location, $routeParams) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.id = $routeParams.id;
        $scope.controller = this;
    }

    RequestEditCtrl.prototype.init = function () {
        var _this = this;
        this.$scope.request = {};
        this.$scope.client = [];

        this.$http.get("/api/act/RequestApi/GetRequest", { params: { id: _this.$scope.id } }).then(function (args) {
            _this.$scope.request = args.data;
            notifySuccess("Items were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get items.", e);
        });

        this.$http.get("/api/act/ClientApi/GetClients").then(function (args) {
            _this.$scope.clients = args.data.List;
            notifySuccess("Items were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get items.", e);
        });
    };

    RequestEditCtrl.prototype.save = function () {
        var _this = this;
        this.$http.post("/api/RequestApi", this.$scope.request).catch(function (e) {
            notifyError("Error saving model.", e);
        }).finally(function () {
            _this.$location.path("");
        });
    };

    RequestEditCtrl.prototype.cancel = function () {
        this.$location.path("");
    }

    return RequestEditCtrl;
})();

RequestEditCtrl.$inject = ['$scope', '$http', '$location', '$routeParams'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('RequestEditCtrl', RequestEditCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Requests', {
            templateUrl: 'Requests.html',
            controller: RequestListCtrl
        })
        .otherwise({ templateUrl: 'Requests.html', controller: RequestListCtrl });
    }
]);