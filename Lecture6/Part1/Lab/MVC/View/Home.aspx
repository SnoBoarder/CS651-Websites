<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="View.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>My Students</h1>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ShowMeTheGoodStudents">List of good students!</asp:HyperLink>

            <br />
            <br />

            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ShowMeTheBadStudents">List of bad students!</asp:HyperLink>
        </div>
    </form>
</body>
</html>
