(function () {
    //create module   
    var app = angular.module('myApp', ['ngRoute']);
    app.controller('productsAnguCtrl', function ($scope) {
        $scope.Message = "Welcome Pratik";
    });
}
)();