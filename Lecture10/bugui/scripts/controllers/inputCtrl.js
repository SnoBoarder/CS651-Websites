'use strict';

var app = angular.module('app');

// input controller: provides a list of countries and some input values
app.controller('inputCtrl', function appCtrl($scope) {

    // data context
    $scope.ctx = {

        // data
        today: new Date(),
        pi: Math.PI,
        departureDate: new Date(),
        returnDate: new Date(),
        passengers: 1,
        mask: '>LL-AA-0000',

        // culture
        culture: 'en'
    };

    // invalidate all Wijmo controls
    // using a separate function to handle strange IE9 scope issues
    function invalidateAll() {
        wijmo.Control.invalidateAll();
    }

});
