/// <reference path="../scripts/angular.js" />

var App = angular.module('App', ['ngRoute', 'ui.bootstrap']);

App.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/Reports', {
                templateUrl: 'Templates/reports.html',
                controller: 'ReportsController'
            })
            .when('/MS', {
                templateUrl: 'Templates/maintainstudents.html',
                controller: 'StudentController'
            })
            .when('/MC', {
                templateUrl: 'Templates/maintainclasses.html',
                controller: 'ClassController'
            })
            .when('/Register', {
                templateUrl: 'Templates/register.html',
                controller: 'RegisterController'
            })
            .when('/Login', {
                templateUrl: 'Templates/login.html',
                controller: 'LoginController'
            })
            .when('/AR', {
                templateUrl: 'Templates/ar.html',
                controller: 'ARController'
            })
            .otherwise({
                redirectTo: '/AR'
            })
    }]
);

App.directive("compareTo", function () {
    return {
        require: "ngModel",
        scope: {
            confirmPassword: "=compareTo"
        },
        link: function (scope, element, attributes, modelVal) {

            modelVal.$validators.compareTo = function (val) {
                return val == scope.confirmPassword;
            };

            scope.$watch("confirmPassword", function () {
                modelVal.$validate();
            });
        }
    };
});

App.controller("LoginController", LoginController);
App.controller("RegisterController", RegisterController);
App.controller("ARController", ARController);
App.controller("ClassController", ClassController);
App.controller("StudentController", StudentController);
App.controller("ReportsController", ReportsController);
