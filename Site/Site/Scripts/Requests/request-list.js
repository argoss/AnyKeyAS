'use strict';
var RequestListCtrl = (function () {
    function RequestListCtrl($scope, $http, $location) {
        this.$scope = $scope;
        this.$http = $http;
        this.$location = $location;
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

    RequestListCtrl.prototype.AddRequest = function () {
        this.$scope.$parent.controller.$scope.currentItem = "Добавление заявки.";
        this.$location.path("/RequestAdd/");
    };

    return RequestListCtrl;
})();

RequestListCtrl.$inject = ['$scope', '$http', '$location'];
var app = getOrCreateAngularModule("anykeyApp", ['ngRoute']);

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