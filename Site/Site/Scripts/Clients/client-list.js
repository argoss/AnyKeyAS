'use strict';
var ClientCtrl = (function () {
    function ClientCtrl($scope, $http) {
        this.$scope = $scope;
        this.$http = $http;
        $scope.controller = this;
    }

    ClientCtrl.prototype.init = function (configuration) {
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

    return ClientCtrl;
})();

ClientCtrl.$inject = ['$scope', '$http'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('ClientCtrl', ClientCtrl);