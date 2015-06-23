'use strict';
var RequestAddCtrl = (function () {
    function RequestAddCtrl($scope, $http) {
        this.$scope = $scope;
        this.$http = $http;
        $scope.controller = this;
    }

    RequestAddCtrl.prototype.init = function () {
        var _this = this;
        this.$scope.request = {};
    };

    RequestAddCtrl.prototype.save = function () {
        this.$http.post("/api/RequestApi", this.$scope.client).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    RequestAddCtrl.prototype.cancel = function () {
        this.$location.path("");
    }

    return RequestAddCtrl;
})();

RequestAddCtrl.$inject = ['$scope', '$http'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

function configFunction($httpProvider) {
    $httpProvider.defaults.cache = false;
}

configFunction.$inject = ['$httpProvider'];

app.config(configFunction);
app.controller('RequestAddCtrl', RequestAddCtrl);