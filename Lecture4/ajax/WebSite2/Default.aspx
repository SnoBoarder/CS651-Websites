<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        // website2: XMLHttpRequest
        var ajaxRequest;

        function initAjax()
        {
            try {
                ajaxRequest = new XMLHttpRequest();
            }
            catch (Error)
            {
                // IE 4 to IE 6
                ajaxRequest = new ActiveXObject("Microsoft.XMLHTTP");
            }
        }

        function handleInput()
        {
            var T1 = document.getElementById("TextBox1");
            var T2 = document.getElementById("TextBox2");
            var theURL = "Default2.aspx?nx=" + T1.value + "&ny=" + T2.value;

            // client communicate with the server without doing a postback (it's clear it's a postback cuz we're calling a different aspx file)
            ajaxRequest.open("GET", theURL);
            ajaxRequest.onreadystatechange = handleUpdate;
            ajaxRequest.send(); // sends the message to the channel that is waiting on the server w/o doing any kind of postback
        }

        function handleUpdate()
        {
            var ansDiv = document.getElementById("answer");
            if (ajaxRequest.readyState == 4)
            {
                ansDiv.innerHTML = ajaxRequest.responseText.split(" ")[0];
            }
        }
    </script>
</head>
<body onload="initAjax();">
    <form id="form1" runat="server">
    <div>
        <div>First number
            <asp:TextBox ID="TextBox1" onkeyup="handleInput();" runat="server"></asp:TextBox>
        </div>
        <div>Second number
            <asp:TextBox ID="TextBox2" onkeyup="handleInput();" runat="server" ></asp:TextBox>
        </div>
        <div id="answer"></div>
    </div>
    </form>
</body>
</html>
