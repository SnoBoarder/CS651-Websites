<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayGoodStudents.aspx.cs" Inherits="DisplayGoodStudents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <%--DisplayGoodStudents.aspx--%>
    <asp:Label ID="Label1" runat="server" Text="Here are the GOOD students!"></asp:Label>
    <asp:DataGrid ID="dtgGoodStudents" runat="server" AutoGenerateColumns="False" Font-Bold="False"
        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
        ForeColor="Blue">
        <Columns>
            <asp:BoundColumn DataField="FirstName" HeaderText="Student Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Address" HeaderText="Address"></asp:BoundColumn>
            <asp:BoundColumn DataField="Grade" HeaderText="Grade"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid></div>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Home.aspx">Goto Home</asp:HyperLink>
    </form>
</body>
</html>

