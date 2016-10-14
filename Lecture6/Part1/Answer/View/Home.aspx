<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <%--Home.aspx--%>
    <h1>My politically correct student list</h1>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/ShowMeTheGoodStudents">Let's look at the good students!</asp:HyperLink><br />
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/ShowMeTheBadStudents">Show me the bad students!</asp:HyperLink>&nbsp;
    </form>
</body>
</html>
<%--  --%>