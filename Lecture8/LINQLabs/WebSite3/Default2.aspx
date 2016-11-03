<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- 8: Ajax db access
    // 1. SqlDataSource
    // 2. Configure
    // 3. ScriptManager
    // 4. UpdatePanel
    // SELECT AddressLine1, PostalCode FROM Address WHERE PostalCode = @PostalCode
    // 90210 -->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AdventureWorks2000ConnectionString %>" 
            SelectCommand="SELECT AddressLine1, PostalCode FROM Address WHERE PostalCode = @PostalCode">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="48042" Name="PostalCode" 
                    QueryStringField="Zip" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" 
    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="AddressLine1" 
            HeaderText="AddressLine1" SortExpression="AddressLine1" />
                        <asp:BoundField DataField="PostalCode" 
            HeaderText="PostalCode" SortExpression="PostalCode" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
