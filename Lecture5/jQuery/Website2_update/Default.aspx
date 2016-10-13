<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lecture 6: JQuery</title>
    <script language="JavaScript">
        document.write("This page created by Dino K. Last update:" + document.lastModified);
        document.bgColor = "black"
        document.fgColor = "#336699"
        //        var answer = window.confirm("Are you sure you want to quit?")
        //        if (answer == true)
        //            window.location = "http://www.google.com"
        //        var username = window.prompt("please enter your user name")
        //        window.alert("Is your name really " + username + "? That's funny!");  
    </script>
    <%--jQuery --%>
    <%-- [][][] Website2 --%>
    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>

    <script type="text/javascript">
        function stripe() {
            $('#third').toggleClass('striped');
        }
        function fade() {
            $("div").fadeIn('slow');
        }
        function highlight() {
            $('p.striped').toggleClass("highlight");
            $('<div class="div1">This div is dynamically inserted</div>').insertAfter('div.menudiv');
        }
        function flashit() {
            flash('#comment-list');
        }

        function flash(selector) {
            //            $(selector).fadeOut('slow').fadeIn('show');
            //            $(selector).fadeOut('fast').fadeIn('show');
            $(selector).hide().fadeIn('show');
        }
        function parseResult(persons) {
            var lists = '';
            for (var i = 1; i <= persons.length; i++) {
                if (lists == '') {
                    lists = persons[i].FirstName;
                }
                else {
                    lists = lists + " <br />" + persons[i].FirstName;
                }
            }
            return lists;
        }
        $(function () {
            $("#comment_submit").click(function () {
                // Set up a string to POST parameters.  
                // You can create the JSON string as well.

                //var dataString = "id=1";
                var dataString = "{'id':1}";

                //url: "/WebSite2/WebService.asmx/X2",
                //http://localhost:58339
                $.ajax({
                    type: "POST",
                    url: "http://localhost:3977/WebService.asmx/X2",
                    contentType: "application/json; charset=utf-8",
                    data: dataString,
                    //dataType: "json",
                    dataType: "application/json; charset=utf-8",
                    beforeSend: function (XMLHttpRequest) {
                        $('#loading-panel').empty().html('<img src="pleasewait.gif" />');
                        //alert("b4..");
                    },
                    success: function (data) {
                        var junk2 = JSON.parse(data);
                        //alert(junk2.d);
                        $('#comment-list').append('<li>' + junk2.d + '</li>');
                        flashit();
                        $('#loading-panel').hide();
                        //alert("success: " + msg.text);
                    },
                    error: function (msg) {
                        $('#comment-list').append(msg);
                        alert("oops: " + msg);
                    },
                    complete: function (XMLHttpRequest, textStatus) {
                        $('#loading-panel').empty();
                        //alert("complete!");
                    }
                });
            });
        });
    </script>

    <%--css--%>
    <style>
        p.striped {
            background-color: red;
        }

        p.highlight {
            background-color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--some DOM elements--%>
            <h2>MET CS651 Lecture 7</h2>
            <h2>Select a paragraph</h2>
            <div>
                <p>This is paragraph 1.</p>
                <p>This is paragraph 2.</p>
                <p id="third">This is paragraph 3.</p>
                <p>This is paragraph 4.</p>
            </div>
            <div class="menudiv" id="menu">
                <div id="item1">
                    <div id="item-1.1">
                        <div id="item-1.2">
                            <div id="item-1.2.1" />
                        </div>
                    </div>
                </div>

                <%--buttons--%>
                <input type="button" value="Stripe" onclick="stripe()"> </input>
                <input type="button" value="Google" onclick="window.open('http://www.google.com', 'win1', 'width=200,height=200')" />
                <input type="button" value="Fade Div" onclick="fade()" />
                <input type="button" value="Change Stripe" onclick="highlight()"> </input>

                <%--labels--%>
                <div style="display: none">
                    This div was hidden.
                </div>
                <div style="display: none; border: solid 1px orange">
                    This too.
                </div>
                <div id="loading-panel"></div>
                <hr />
                <input id="comment_submit" type="button" value="AJAX and JQuery!" />
                <div id="comment-list"></div>
            </div>
    </form>
</body>
</html>
