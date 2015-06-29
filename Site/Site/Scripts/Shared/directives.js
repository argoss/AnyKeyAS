'use strict';
function datePickerController($scope) {
    $scope.format = 'yyyy.MM.dd';
    $scope.clear = function () {
        $scope.localModel = null;
        $scope.utcModel = null;
    };
    $scope.open = function ($event) {
        $scope.opened = true;
        $event.preventDefault();
        $event.stopPropagation();
    };
    $scope.changed = function () {
        $scope.fromDP();
    };
    $scope.fromDP = function () {
        if ($scope.localModel == null) {
            $scope.utcModel = null;
            return;
        }
        var utcDate = angular.copy($scope.localModel);
        utcDate.setTime($scope.localModel.getTime() - 60000 * $scope.localModel.getTimezoneOffset());
        $scope.utcModel = utcDate;
    };
    $scope.toDP = function () {
        if ($scope.utcModel == null) {
            $scope.localModel = null;
            return;
        }
        if (typeof ($scope.utcModel) == 'string')
            $scope.utcModel = new Date($scope.utcModel);
        var localDate = angular.copy($scope.utcModel);
        localDate.setTime($scope.utcModel.getTime() + 60000 * $scope.utcModel.getTimezoneOffset());
        localDate.setHours(0, 0, 0, 0);
        $scope.localModel = localDate;
    };
    $scope.$watch('utcModel', function () {
        $scope.toDP();
    });
}

datePickerController.$inject = ['$scope'];
function datePickerFactory() {
    return {
        restrict: 'A',
        scope: {
            utcModel: '=datePicker'
        },
        controller: datePickerController,
        template: '<input type="text" class="form-control" datepicker-popup="{{format}}" ng-model="localModel" is-open="opened" ng-change="changed()" close-text="Close" ng-click="open($event)" />'
    };
}

var app = angular.module("anyKey.mvc", ['ui.bootstrap']);

app.directive("datePicker", datePickerFactory);