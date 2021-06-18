/// <reference path="angular.js" />

var App = angular.module("App", ['ngRoute']);

App.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider
            .when('/Register', {
                templateUrl: 'Templates/register.html',
                controller: 'RegisterController'
            })
            .when('/Home', {
                templateUrl: 'Templates/home.html',
                controller: 'HomeController'
            })
            .otherwise({
            redirectTo: '/Home'})
    }]
);

App.controller("RegisterController", function ($scope) {
    $scope.message = "Register View";
});
App.controller("HomeController", function ($scope) {
    $scope.message = "Home View";
});