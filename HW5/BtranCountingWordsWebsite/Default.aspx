<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<!-- Developed by Brian Tran [btran89@bu.edu] -->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/max-width-800px.css" rel="stylesheet" type="text/css" />
    <link href="css/max-width-490px.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            // add event listener to keyup for the input textfield
            $("#input").keyup(function () {
                // Set up a string to POST parameters.

                var data = {};
                data.input = $("#input").val();

                // convert object into JSON string to pass to the endpoint
                var dataString = JSON.stringify(data);

                $.ajax({
                    type: "POST",
                    // NOTE: Don't need to explicitly reference a port since we'll be relative to the current domain.
                    url: "WebService.asmx/CountWordsFromInput",
                    contentType: "application/json; charset=utf-8",
                    dataType: "application/json; charset=utf-8",
                    data: dataString,
                    success: function (data) {
                        $('#output').val(JSON.parse(data).d);
                    },
                    error: function (msg) {
                        alert("Error: " + msg.responseText);
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <div id="header">
                <h1>Brian Tran's Word Counting</h1>
            </div>

            <div id="content">
                <h1>Input</h1>
                <asp:TextBox ID="input" CssClass="input" TextMode="MultiLine" Wrap="true" runat="server">Tell your story here.</asp:TextBox>
            </div>

            <div id="contentb">
                <h1>Output</h1>
                <asp:TextBox ID="output" CssClass="output" TextMode="MultiLine" Wrap="true" ReadOnly="true" runat="server">See your word counts here.</asp:TextBox>
            </div>

            <div id="footer">
                <p>Copyright Brian Tran</p>
            </div>
        </div>
    </form>
</body>
</html>
