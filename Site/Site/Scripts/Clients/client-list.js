'use strict';
var ClientListCtrl = (function () {
    function ClientListCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.controller = this;
    }

    ClientListCtrl.prototype.init = function (configuration) {
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

    ClientListCtrl.prototype.ClientEdit = function (id) {
        if (id == null)
            this.$location.path("/ClientEdit/");
        else
            this.$location.path("/ClientEdit/" + id);
    };

    return ClientListCtrl;
})();

ClientListCtrl.$inject = ['$scope', '$http', '$location'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

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
        });
    }
]);