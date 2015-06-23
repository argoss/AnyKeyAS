'use strict';
var ClientEditCtrl = (function () {
    function ClientEditCtrl($scope, $http, $location, $routeParams) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
        $scope.id = $routeParams.id;
        $scope.controller = this;
    }

    ClientEditCtrl.prototype.init = function () {
        var _this = this;
        this.$scope.client = {};

        this.$http.get("/api/act/ClientApi/GetClient", { params: { id: this.id } }).then(function (args) {
            _this.$scope.client = args.data;
            notifySuccess("Item were loaded.");
        }).catch(function (e) {
            notifyError("Unable to get item.", e);
        }).finally(function () {
            
        });
    };

    ClientEditCtrl.prototype.save = function () {
        this.$http.post("/api/ClientApi", this.$scope.client).then(function () {
            this.$location.path("");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    ClientEditCtrl.prototype.cancel = function() {
        this.$location.path("");
    }

    return ClientEditCtrl;
})();

ClientEditCtrl.$inject = ['$scope', '$http', '$location', '$routeParams'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('ClientEditCtrl', ClientEditCtrl);

app.config([
    '$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/Clients', {
            templateUrl: 'Clients.html',
            controller: ClientListCtrl
        })
        .otherwise({ templateUrl: 'Clients.html', controller: ClientListCtrl });
    }
]);