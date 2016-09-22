<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
        <div id="nameTag">
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
                <asp:Button ID="SubmitButton" runat="server" Text="Submit the Above Words" OnClick="Submit" />
                <br />
                <asp:TextBox ID="OutputField" CssClass="output" TextMode="MultiLine" Wrap="true" ReadOnly="true" runat="server">See your word counts here.</asp:TextBox>

            </div>
        </div>
    </form>

    <template id="nameTagTemplate">
        <style>
            /* The image class is bound here to practice using the shadow DOM. */
            .top {
                /* This used to be combined with .content and .bottom */
                background: #000000;
                position: absolute;
                margin-left: 50%;
                left: -400px;
                width: 700px;

                /* This used to be just .top */
                background: url("images/top.gif");
                background-repeat: no-repeat;
                background-position: bottom left;
                margin-top: 99px;
                height: 330px;
                font-size: 46px;
                line-height: 80px;
                font-family: Georgia, "Times New Roman", Times, serif;
                color: #00486C;
                padding-left: 120px;
                font-weight: bolder;
            }
        </style>
        <div class="top">
            <br />Brian's Website
        </div>
    </template>

    <script>
        // createShadowRoot is deprecated! use attachShadow
        var isChrome = !!window.chrome && !!window.chrome.webstore;

        if (isChrome)
        {
            // NOTE: This only works in chrome
            var root = document.querySelector('#nameTag').attachShadow({ mode: 'open' });
            var template = document.querySelector('#nameTagTemplate');
            var clone = document.importNode(template.content, true);
            root.appendChild(clone);
        }
    </script>
</body>
</html>
