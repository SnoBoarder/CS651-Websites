<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Brian's Website</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="image"></div>
        <div class="top">
            <br />
            Brian's Website
        </div>
        <div class="bottom">
            <br />
            &copy; Brian Tran, 2016
        </div>
        <div class="content">
            <div class="contentb">
                <h2>Counting Words</h2>
                <asp:TextBox ID="InputField" CssClass="input" TextMode="MultiLine" Wrap="true" runat="server">Tell your story here.</asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Submit" />

                <br />
                <asp:TextBox ID="OutputField" CssClass="output" TextMode="MultiLine" Wrap="true" ReadOnly="true" runat="server">See your word counts here.</asp:TextBox>

            </div>
        </div>
    </form>

    <template id="nameTagTemplate">
    </template>

    <!-- Step 2 apply shadow DOM -->
    <script>
        var shadow = document.querySelector('#nameTag').createShadowRoot();
        var template = document.querySelector('#nameTagTemplate');
        var clone = document.importNode(template.content, true);
        shadow.appendChild(clone);
    </script>
</body>
</html>
