<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayBadStudents.aspx.cs" Inherits="View.DisplayBadStudents" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Here are the BAD students!"></asp:Label>
            <asp:DataGrid ID="dtgBadStudents" runat="server" AutoGenerateColumns="false" Font-Bold="false" Font-Italic="false" Font-Overline="false" Font-Strikeout="false" Font-Underline="false" ForeColor="Blue">
                <Columns>
                    <asp:BoundColumn DataField="FirstName" HeaderText="Student Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Address" HeaderText="Address"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Home.aspx">Go Back</asp:HyperLink>
        </div>
    </form>
</body>
</html>
