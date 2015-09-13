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
        this.$scope.StatusInc = false;
        this.$scope.DateInc = false;

        this.$http.get(_this.apiCfg.RequestUrl).then(function (args) {
            _this.$scope.items = args.data.List;
            _this.$scope.requests = args.data.List;

            notifySuccess("Данные получены.");
        }).catch(function (e) {
            notifyError("Невозможно получить данные.", e);
        }).finally(function () {

        });
    };

    ServiceCtrl.prototype.filter = function (btn, filter) {
        var index = this.$scope.query.indexOf(filter);
        if (index == -1) {
            this.$scope.query.push(filter);
            $('#' + btn).attr('class', 'pagination pagination-lg active');
        } else {
            this.$scope.query.splice(index, 1);
            $('#' + btn).attr('class', 'pagination pagination-lg');
        }
        if (this.$scope.query.length < 1) {
            this.$scope.requests = this.$scope.items;
            $('#all').attr('class', 'pagination pagination-lg active');
            return;
        } else {
            $('#all').attr('class', 'pagination pagination-lg');
        }
            
        var _this = this;
        this.$scope.requests = this.$scope.items.filter(function (item) {
            return _this.$scope.query.indexOf(item.Status) > -1;
        });
    };

    ServiceCtrl.prototype.all = function () {
        $('.filter').find("li").attr('class', 'pagination pagination-lg');

        this.$scope.query = [];
        this.$scope.requests = this.$scope.items;
        $('#all').attr('class', 'pagination pagination-lg active');
    };

    ServiceCtrl.prototype.changeStatus = function (item, status) {
        item.Status = status;
        item.ModifyDate = new Date();
        var _this = this;
        this.$http.post("/api/act/ServiceApi/ChangeStatus", { id: item.Id, status: status }).then(function () {
            if (_this.$scope.query.length < 1)
                return;
            _this.$scope.requests = _this.$scope.items.filter(function (r) {
                return _this.$scope.query.indexOf(r.Status) > -1;
            });
            notifySuccess("Status change completed.");
        }).catch(function (e) {
            notifyError("Error saving model.", e);
        });
    };

    ServiceCtrl.prototype.sortByStatus = function() {
        if (this.$scope.StatusInc) {
            
            this.$scope.StatusInc = false;
        }
        else {

            this.$scope.StatusInc = true;
        }
    }

    ServiceCtrl.prototype.sortByData = function () {
        if (this.$scope.DateInc) {

            this.$scope.DateInc = false;
        }
        else {

            this.$scope.DateInc = true;
        }
    }

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