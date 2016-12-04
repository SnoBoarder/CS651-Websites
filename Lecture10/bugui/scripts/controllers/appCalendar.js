'use strict';

// This does not work!! Looks like [] dependencies need to be specified on the first script inclusion (in my case, it's app.js).
//var app = angular.module('app', ['ui.calendar', 'angularMoment']);
//var app = angular.module('app', ['wj', 'ngRoute', 'ui.calendar', 'angularMoment']);

var app = angular.module('app');

app.controller('CalendarCtrl', function ($scope, $filter, moment, uiCalendarConfig) {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var currentView = "month";


    //event source that pulls from google.com
    $scope.eventSource = {
        url: "http://www.google.com/calendar/feeds/usa__en%40holiday.calendar.google.com/public/basic",
        className: 'gcal-event',           // an option!
        currentTimezone: 'America/Chicago' // an option!
    };


    //This will call onLoad and you can assign the values the way you want to the calendar
    //here DataRetriever.jsp will give me array of JSON data generated from the database data
    /*
    $http.get('DataRetriever.jsp').success(function(data) {
        for(var i = 0; i < data.length; i++)
        {
            $scope.events[i] = {id:data[i].id, title: data[i].task,start: new Date(data[i].start), end: new Date(data[i].end),allDay: false};
        }
    });
    */

    //to explicitly add events to the calendar
    //you can add the events in following ways
    $scope.events = [
        { title: 'Learn Angular In One Day', start: new Date(y, m, d + 1, 19, 0), end: new Date(y, m, d + 1, 22, 30), allDay: false },
        { title: 'The Basics of MVC', start: new Date('Tue Feb 17 2015 09:00:00 GMT+0530 (IST)') },
        { title: 'The New Javascript', start: new Date('Tue Feb 17 2015 10:00:00 GMT+0530 (IST)'), end: new Date('Sat Feb 21 2015 17:00:00 GMT+0530 (IST)') },
        { id: 999, title: 'Server-side Javascript', start: new Date('Fri Feb 20 2015 09:00:00 GMT+0530 (IST)'), allDay: false },
        { id: 999, title: 'The Postback-based Web site', start: new Date(y, m, d + 4, 16, 0), allDay: false },
        { title: 'The Internet of Things', start: new Date(y, m, d + 1, 19, 0), end: new Date(y, m, d + 1, 22, 30), allDay: false },
        { title: 'The Kinect Device', start: new Date(y, m, 28), end: new Date(y, m, 29), url: 'http://www.kathimerini.com/' }
    ];


    //with this you can handle the events that generated by clicking the day(empty spot) in the calendar
    $scope.dayClick = function (date, allDay, jsEvent, view) {
        $scope.$apply(function () {
            $scope.alertMessage = ('Day Clicked ' + date);
        });
    };


    //with this you can handle the events that generated by dropping any event to different position in the calendar
    $scope.alertOnDrop = function (event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view) {
        $scope.$apply(function () {
            $scope.alertMessage = ('Event Droped to make dayDelta ' + dayDelta);
        });
    };


    //with this you can handle the events that generated by resizing any event to different position in the calendar
    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        $scope.$apply(function () {
            $scope.alertMessage = ('Event Resized to make dayDelta ' + minuteDelta);
        });
    };

    /*
    //this code will add new event and remove the event present on index
    //you can call it explicitly in any method
        $scope.events.push({
        title: 'New Task',
        start: new Date(y, m, 28),
        end: new Date(y, m, 29),
        className: ['newtask']
        });
         
    $scope.events.splice(index,1);*/

    /* add custom event*/
    //$scope.addEvent = function () {
    //    $scope.events.push({
    //        title: 'New  Sortie',
    //        start: new Date(y, m, 28),
    //        end: new Date(y, m, 29),
    //        className: ['openSesame']
    //    });
    //};
    //$scope.addEvent = function () {
    //    $scope.events.push({
    //        title: 'New  Sortie',
    //        start: new Date($('#takeoff').val()),
    //        end: new Date($('#land').val()),
    //        className: ['openSesame']
    //    });
    //};
    //$scope.addEvent = function () {
    //    $scope.events.push({
    //        title: 'New  Sortie',
    //        start: new Date('Sun Feb 22 2015 09:00:00 GMT+0530 (IST)'),
    //        end: new Date('Sun Feb 22 2015 11:00:00 GMT+0530 (IST)'),
    //        className: ['openSesame']
    //    });
    //};
    //$scope.addEvent = function () {
    //    $scope.events.push({
    //        title: 'New  Sortie',
    //        start: new Date('Feb 22 2015 09:00:00'),
    //        end: new Date('Feb 22 2015 11:00:00'),
    //        className: ['openSesame']
    //    });
    //};
    //$scope.addEvent = function () {
    //    $scope.events.push({
    //        title: 'New  Sortie',
    //        start: new Date('Feb 22 2015 09:00 AM'),
    //        end: new Date($('#land').val()),
    //        className: ['openSesame']
    //    });
    //};
    $scope.addEvent = function () {
        $scope.events.push({
            title: $('#sortie').val(),
            start: new Date($('#takeoff').val()),
            end: new Date($('#land').val()),
            className: ['openSesame']
        });
    };


    //with this you can handle the click on the events
    $scope.eventClick = function (event) {
        $scope.$apply(function () {
            $scope.alertMessage = (event.title + ' is clicked');
        });
    };


    //with this you can handle the events that generated by each page render process
    $scope.renderView = function (view) {
        var date = new Date(view.calendar.getDate());
        $scope.currentDate = date.toDateString();
        $scope.$apply(function () {
            $scope.alertMessage = ('Page render with date ' + $scope.currentDate);
        });
    };


    //with this you can handle the events that generated when we change the view i.e. Month, Week and Day
    $scope.changeView = function (view, calendar) {
        currentView = view;
        calendar.fullCalendar('changeView', view);
        $scope.$apply(function () {
            $scope.alertMessage = ('You are looking at ' + currentView);
        });
    };


    /* config object */
    /*
    $scope.uiConfig = {
        calendar:{
        height: 450,
        editable: true,
        header:{
            left: 'title',
            center: '',
            right: 'today prev,next'
        },
        dayClick: $scope.dayClick,
        eventDrop: $scope.alertOnDrop,
        eventResize: $scope.alertOnResize,
        eventClick: $scope.eventClick,
        viewRender: $scope.renderView
        }    
    };
    */

    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            header: {
                left: 'month basicWeek basicDay agendaWeek agendaDay',
                center: 'title',
                right: 'today prev,next'
            },
            dayClick: $scope.alertEventOnClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize
        }
    };

    /* event sources array*/
    $scope.eventSources = [$scope.events, $scope.eventSource, $scope.eventsF];

    /* mobiscroll-related */
    var curr = new Date().getFullYear();
    var opt = {
        'date': {
            preset: 'date',
            dateOrder: 'd Dmmyy',
            invalid: { daysOfWeek: [0, 6], daysOfMonth: ['5/1', '12/24', '12/25'] }
        },
        'datetime': {
            preset: 'datetime',
            minDate: new Date(2015, 1, 22, 9, 22),
            maxDate: new Date(2016, 1, 22, 15, 44),
            stepMinute: 5
        },
        'time': {
            preset: 'time'
        },
        'credit': {
            preset: 'date',
            dateOrder: 'mmyy',
            dateFormat: 'mm/yy',
            startYear: curr,
            endYear: curr + 10,
            width: 100
        },
        'btn': {
            preset: 'date',
            showOnFocus: false
        },
        'inline': {
            preset: 'date',
            display: 'inline'

        }
    }

    $('select').scroller({ preset: 'select' }).bind('change', function () {
        $('#test').val('').scroller('destroy').scroller($.extend(opt[$('#demo').val()], { theme: $('#theme').val(), mode: $('#mode').val(), lang: $('#language').val() }));
        $('#takeoff').val('').scroller('destroy').scroller($.extend(opt[$('#demo').val()], { theme: $('#theme').val(), mode: $('#mode').val(), lang: $('#language').val() }));
        $('#land').val('').scroller('destroy').scroller($.extend(opt[$('#demo').val()], { theme: $('#theme').val(), mode: $('#mode').val(), lang: $('#language').val() }));
        $('#demo').val() == 'btn' ? $('#buttons').show() : $('#buttons').hide();
    });

    $('#demo').trigger('change');

    $('#trigger').click(function () {
        $('#test').scroller('show');
        $('#takeoff').scroller('show');
        $('#land').scroller('show');
        return false;
    });

    $('#clear').click(function () {
        $('#test').val('');
        $('#takeoff').val('');
        $('#land').val('');
        return false;
    });
});