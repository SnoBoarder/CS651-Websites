﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        // javascript to post back to server
        // when user types anything in the text box
        function OnKeyUp()
        {
            // the postBack function to call
            // this is generated by calling
            // GetPostBackEventReference in code-behind
            <%= postBackString %> // the "=" sign at the beginning means to execute
        }

        // function to put focus back to the textbox after each postback
        function focusTextBox()
        {
            document.getElementById("TextBox1").focus();
        }

        // function to put cursor at the end of the string in the text box
        function SetEnd(TB) {
            if (TB.createTextRange) {
                var FieldRange = TB.createTextRange();
                FieldRange.moveStart('character', TB.value.length);
                FieldRange.collapse();
                FieldRange.select();
            }
        }
    </script>
</head>
<body onload="focusTextBox()">
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Type text or Press &quot;Count Words&quot; to show number of words."></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Height="163px" TextMode="MultiLine"></asp:TextBox>
        </div>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Count Words" OnClick="OnButtonClick" Width="119px" Height="20px" />
        </p>

        <p>
            <asp:Label ID="Result" runat="server" Text="Label"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
