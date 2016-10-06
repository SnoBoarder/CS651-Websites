<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/max-width-800px.css" rel="stylesheet" type="text/css" />
    <link href="css/max-width-490px.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <script type="text/javascript">
        function ReceiveServerData(arg, context) {
            document.getElementById("output").value = arg;
        }
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
                <asp:TextBox ID="input" CssClass="input" TextMode="MultiLine" Wrap="true" runat="server" onkeyup="CallServer(document.getElementById('input').value);">Tell your story here.</asp:TextBox>
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
