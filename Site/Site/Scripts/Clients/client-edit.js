'use strict';
var ClientEditCtrl = (function () {
    function ClientEditCtrl($scope, $http, $routeParams) {
        this.$scope = $scope;
        this.$http = $http;
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

    ClientEditCtrl.prototype.Save = function () {

    };

    return ClientEditCtrl;
})();

ClientEditCtrl.$inject = ['$scope', '$http', '$routeParams'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('ClientEditCtrl', ClientEditCtrl);