var app = angular.module('app', ['wj', 'ngRoute', 'ui.calendar', 'angularMoment']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.

    // Profile
    when('/profile/intro', { templateUrl: 'partials/profile/intro.htm' }).
    when('/messages/email', { templateUrl: 'partials/messages/email.htm', controller: 'MessagesCtrl' }).
    when('/profile/info', { templateUrl: 'partials/profile/info.htm', controller: 'infoCtrl' }).

    // Colleges
    when('/colleges/instructors', { templateUrl: 'partials/colleges/instructors.htm', controller: 'basicCtrl' }).
    when('/colleges/classes', { templateUrl: 'partials/colleges/classes.htm', controller: 'basicCtrl' }).
    when('/colleges/roles', { templateUrl: 'partials/colleges/roles.htm', controller: 'basicCtrl' }).
    when('/colleges/system', { templateUrl: 'partials/colleges/system.htm', controller: 'basicCtrl' }).

    // Maintenance
    when('/maintenance/projectors', { templateUrl: 'partials/maintenance/projectors.htm', controller: 'basicCtrl' }).
    when('/maintenance/classrooms', { templateUrl: 'partials/maintenance/classrooms.htm', controller: 'gaugeCtrl' }).
    when('/maintenance/parking', { templateUrl: 'partials/maintenance/parking.htm', controller: 'basicCtrl' }).
    when('/maintenance/reminders', { templateUrl: 'partials/maintenance/reminders.htm', controller: 'inputCtrl' }).

    // Schedule
    when('/manuals/intro', { templateUrl: 'partials/manuals/intro.htm', controller: 'basicCtrl' }).
    when('/schedule/calendar', { templateUrl: 'partials/schedule/calendar.htm', controller: 'CalendarCtrl' }).
    when('/missions/kanban', { templateUrl: 'partials/missions/kanban.htm', controller: 'MissionsCtrl' }).

    // Gauge
    when('/reports/department', { templateUrl: 'partials/reports/department.htm', controller: 'gaugeCtrl' }).
    when('/reports/university', { templateUrl: 'partials/reports/university.htm', controller: 'gaugeCtrl' }).

    // default...
    when('/', { templateUrl: 'partials/profile/intro.htm', controller: 'basicCtrl' }).
    otherwise({ redirectTo: '/' });
}]);

