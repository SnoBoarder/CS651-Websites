'use strict';

var app = angular.module('app');

// gauge controller: provides data for the gauges
app.controller('gaugeCtrl', function appCtrl($scope, $sce, sparkSvc) {

	// data context
	$scope.ctx = {

		// simple gauge data
		gauge: {
			min: 0,
			max: 100,
			value: 75,
			step: 5,
			angles: [
				{ start: -45, sweep: 270 },
				{ start: 10, sweep: 340 },
				{ start: 0, sweep: 90 },
				{ start: 45, sweep: 90 }
			]
		},
		gauge2: {
		    min: 0,
		    max: 100,
		    value: 45,
		    step: 5,
		    angles: [
				{ start: -45, sweep: 270 },
				{ start: 10, sweep: 340 },
				{ start: 0, sweep: 90 },
				{ start: 45, sweep: 90 }
		    ]
		},
		gauge3: {
		    min: 0,
		    max: 100,
		    value: 15,
		    step: 5,
		    angles: [
				{ start: -45, sweep: 270 },
				{ start: 10, sweep: 340 },
				{ start: 0, sweep: 90 },
				{ start: 45, sweep: 90 }
		    ]
		},
		gauge4: {
		    min: 0,
		    max: 200,
		    value: 125,
		    step: 5,
		    angles: [
				{ start: -45, sweep: 270 },
				{ start: 10, sweep: 340 },
				{ start: 0, sweep: 90 },
				{ start: 45, sweep: 90 }
		    ]
		},
		gauge5: {
		    min: 0,
		    max: 1000,
		    value: 775,
		    step: 5,
		    angles: [
				{ start: -45, sweep: 270 },
				{ start: 10, sweep: 340 },
				{ start: 0, sweep: 90 },
				{ start: 45, sweep: 90 }
		    ]
		},

		// Committee and Teaching hours
		committeeHours: [
			{ name: 'Ethics', units: 'H', current: 8.3, target: 7.5, last12: [8.6,9.0,8.8,8.6,8.3,8.4,8.6,8.4,8.5,8.2,8.2,8.3] },
			{ name: 'Graduation', units: 'H', current: 8.1, target: 8, last12: [7.4,7.8,7.8,8.2,7.9,7.8,8.1,7.9,8.1,8.1,7.9,8.1] },
			{ name: 'Capital Campaign', units: 'H', current: 8.1, target: 7.5, last12: [7.8,7.7,7.7,7.9,8.3,8.1,7.7,8.1,7.9,8.2,8.4,8.1] },
			{ name: 'Dissertations', units: 'H', current: 6.8, target: 6.5, last12: [5.7,6.0,6.2,5.9,6.1,6.1,6.4,6.2,6.5,6.7,6.6,6.8] },
			{ name: 'Faculty Search', units: 'H', current: 6.4, target: 6.5, last12: [6.0,6.0,6.2,6.1,5.9,5.9,6.1,6.2,6.2,6.0,6.3,6.4] },
			{ name: 'Classroom Improvement', units: 'H', current: 4.1, target: 4, last12: [4.0,3.8,3.9,3.9,4.1,4.3,4.3,4.2,4.1,4.1,4.0,4.1] }
		],
		teachingHours: [
			{ name: 'CS651', units: 'H', current: 18.1, target: 18, last12: [17.4,17.8,17.8,18.2,17.19,17.8,18.1,17.9,18.1,18.1,17.9,18.1] },
			{ name: 'CS751', units: 'H', current: 18.1, target: 17.5, last12: [17.8,17.7,17.7,17.9,18.3,18.1,17.7,18.1,17.9,18.2,18.4,18.1] },
			{ name: 'CS755', units: 'H', current: 16.8, target: 16.5, last12: [15.7,16.0,16.2,15.9,16.1,16.1,16.4,16.2,16.5,16.7,16.6,16.8] },
			{ name: 'CS323', units: 'H', current: 16.4, target: 16.5, last12: [16.0,16.0,16.2,16.1,15.9,15.9,16.1,16.2,16.2,16.0,16.3,16.4] }
		]
	}

	// sparkline service
	$scope.getSparklines = function (data) {
		return $sce.trustAsHtml(sparkSvc.getSparklines(data, '100%', '1.5em'));
	};

	// get tooltip for an item
	$scope.getTooltip = function (item) {
		return wijmo.format('{name} {ia} <b>{delta:p0} {ab}</b> the target', {
			name: item.name,
			ia: (item.name[item.name.length - 1] == 's') ? 'are' : 'is',
			ab: (item.current > item.target) ? 'above' : 'below',
			delta: Math.abs(item.current / item.target - 1)
		});
	}
});
