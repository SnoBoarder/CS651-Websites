'use strict';

var app = angular.module('app');

app.controller('MessagesCtrl', function ($scope, $filter) {

    $scope.isPopupVisible = false;
    $scope.isComposePopupVisible = false;
    $scope.isTestModalVisible = false;
    $scope.composeEmail = {};
    $scope.activeTab = "inbox";

    //$('a[rel*=leanModal]').leanModal({ top: 200, closeButton: ".modal_close" });

    //$("#myModal").on("show", function () {    // wire up the OK button to dismiss the modal when shown
    //    $("#myModal a.btn").on("click", function (e) {
    //        console.log("button pressed");   // just as an example...
    //        $("#myModal").modal('hide');     // dismiss the dialog
    //    });
    //});

    $("#myModal").on("hide", function () {    // remove the event listeners when the dialog is dismissed
        $("#myModal a.btn").off("click");
    });

    $("#myModal").on("hidden", function () {  // remove the actual elements from the DOM when fully hidden
        $("#myModal").remove();
    });

    //$("#myModal").modal({                    // wire up the actual modal functionality and show the dialog
    //    "backdrop": "static",
    //    "keyboard": true,
    //    "show": true                     // ensure the modal is shown immediately
    //});

    $("#listedEmail").on("hide", function () {    
        $("#listedEmail a.btn").off("click");
    });

    $("#listedEmail").on("hidden", function () {  
        $("#listedEmail").remove();
    });


    $("#newEmail").on("hide", function () {    
        $("#newEmail a.btn").off("click");
    });

    $("#newEmail").on("hidden", function () {  
        $("#newEmail").remove();
    });

    
    $scope.sendEmail = function () {
        $scope.isComposePopupVisible = false;
        //alert($scope.composeEmail.to
        //    + " " + $scope.composeEmail.subject
        //    + " " + $scope.composeEmail.body);

        //$scope.sentEmails.push($scope.composeEmail); // OLD
        $scope.sentEmails.splice(0, 0, $scope.composeEmail); // NEW

        $scope.composeEmail.date = new Date();
    };

    $scope.forward = function () {
        // hide the view details popup
        $scope.isPopupVisible = false;
        // create an empty composeEmail object the compose email popup is bound to
        $scope.composeEmail = {};
        // copy the data from selectedEmail into composeEmail
        angular.copy($scope.selectedEmail, $scope.composeEmail);

        // edit the body to prefix it with a line and the original email information
        $scope.composeEmail.body =
            "\n-------------------------------\n"
            + "from: " + $scope.composeEmail.from + "\n"
            + "sent: " + $scope.composeEmail.date + "\n"
            + "to: " + $scope.composeEmail.to + "\n"
            + "subject: " + $scope.composeEmail.subject + "\n"
            + $scope.composeEmail.body;

        // prefix the subject with “RE:”
        $scope.composeEmail.subject = "FW: " + $scope.composeEmail.subject;
        // the email is going to the person who sent it to us
        // so populate the to with from
        $scope.composeEmail.to = $scope.composeEmail.from;
        // it’s coming from us
        $scope.composeEmail.from = "me";
        // show the compose email popup
        $scope.isComposePopupVisible = true;

    };

    $scope.reply = function () {
        // hide the view details popup
        $scope.isPopupVisible = false;
        // create an empty composeEmail object the compose email popup is bound to
        $scope.composeEmail = {};
        // copy the data from selectedEmail into composeEmail
        angular.copy($scope.selectedEmail, $scope.composeEmail);

        // edit the body to prefix it with a line and the original email information
        $scope.composeEmail.body =
            "\n-------------------------------\n"
            + "from: " + $scope.composeEmail.from + "\n"
            + "sent: " + $scope.composeEmail.date + "\n"
            + "to: " + $scope.composeEmail.to + "\n"
            + "subject: " + $scope.composeEmail.subject + "\n"
            + $scope.composeEmail.body;

        // prefix the subject with “RE:”
        $scope.composeEmail.subject = "RE: " + $scope.composeEmail.subject;
        // the email is going to the person who sent it to us
        // so populate the to with from
        $scope.composeEmail.to = $scope.composeEmail.from;
        // it’s coming from us
        $scope.composeEmail.from = "me";
        // show the compose email popup
        $scope.isComposePopupVisible = true;
    };

    $scope.showPopup = function (email) {
        $scope.isPopupVisible = true;
        $scope.selectedEmail = email;

        $("#listedEmail").modal({
            "backdrop": "static",
            "keyboard": true,
            "show": true
        });
    };

    $scope.showTestModal = function () {
        $scope.isTestModalVisible = true;

        $("#myModal").modal({                    // wire up the actual modal functionality and show the dialog
            "backdrop": "static",
            "keyboard": true,
            "show": true                     // ensure the modal is shown immediately
        });
    };

    $scope.closeTestModal = function () {
        $scope.isTestModalVisible = false;
    };

    $scope.closePopup = function () {
        $scope.isPopupVisible = false;
    };

    $scope.showComposePopup = function (email) {
        $scope.composeEmail = {};
        $scope.isComposePopupVisible = true;
        $scope.selectedEmail = email;

        $("#newEmail").modal({
            "backdrop": "static",
            "keyboard": true,
            "show": true
        });
    };

    $scope.closeComposePopup = function () {
        $scope.isComposePopupVisible = false;
    };

    $scope.emails = [
    {
        from: 'Hocking',
        subject: 'You gave me a bad grade!',
        date: 'Jan 1',
        body: 'Dear Professor, you ruined my GPA. Thanks a lot :-('
    },
    {
        from: 'McSherry',
        subject: 'I loved your class',
        date: 'Feb 15',
        body: 'Dear Professor, I loved your class. Now I know what Web development is all about.'
    },
    {
        from: 'Valentine',
        subject: 'You suck',
        date: 'Feb 22',
        body: 'I did not learn how to write the next Candy Crush. Boring!'
    }
    ];
});