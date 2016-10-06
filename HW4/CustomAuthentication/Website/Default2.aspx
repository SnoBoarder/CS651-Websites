<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    function fooledya() {
        alert("Your machine was commandeered by the Joker :-)");
    }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Button1" type="button" value="Click here for a sugary snack.." onclick="return fooledya();" />
    </div>
    </form>
</body>
</html>
