<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <%--<link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <%-- [][][] Website1 --%>

    <style type="text/css">
        h1 {
            font-size: 5.5em;
            margin-bottom: 0;
        }

        .emphasize {
            font-style: italic;
            color: red;
        }
    </style>

    <script type="text/javascript" src="Scripts\jquery-1.4.1.min.js"> 

    </script>
    <script type="text/javascript">
        function doEmph() {
            //alert("inside doEmph()"); 
            $("dt").addClass("emphasize");
        }
        $(document).ready(function () {
            $("dt").addClass("emphasize");
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <h1>Cities of the World</h1>
        <dl>
            <dt>Jerusalem</dt>
            <dd>very old</dd>
            <dt>Moscow</dt>
            <dd>land of Putin</dd>
            <dt>Sweden</dt>
            <dd>carefuk with Swords!</dd>
        </dl>

        <p>bla bla</p>
        <p>I am Gronk!</p>
    </form>
</body>
</html>

