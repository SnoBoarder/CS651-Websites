<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <title></title>

    <script type="text/javascript">
        function ReceiveServerData(arg, context)
        {
            document.getElementById("TextBox2").value = arg;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Height="147px" onkeyup="CallServer(document.getElementById('TextBox1').value);" TextMode="MultiLine"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" Height="147px" TextMode="MultiLine"></asp:TextBox>
    </div>
    </form>
</body>
</html>
