var Users;
(function (Users) {
    'use strict';
    var UserCtrl = (function () {
        function UserCtrl($scope, $http) {
            this.$scope = $scope;
            this.$http = $http;
            $scope.controller = this;
        }

        UserCtrl.prototype.init = function (configuration) {
            var _this = this;
            this.urlList = configuration.UrlList;
            this.$scope.items = [];

            this.$http.get(this.urlList).then(function (args) {
                _this.$scope.items = args.data.List;
                notify("Items were loaded.");
            }).catch(function (e) {
                notify("Unable to get items: ");
            }).finally(function () {
                
            });
        };

        return UserCtrl;
    })();

    Users.UserCtrl = UserCtrl;
    UserCtrl.$inject = ['$scope', '$http'];
    var app = angular.module("anykeyApp", ['ui.bootstrap', 'ngRoute']);
    
    function configFunction($httpProvider) {
        $httpProvider.defaults.cache = false;
    }

    Users.configFunction = configFunction;

    configFunction.$inject = ['$httpProvider'];

    app.config(configFunction);
    app.controller('UserCtrl', UserCtrl);
})(Users || (Users = {}));