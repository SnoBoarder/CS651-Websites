<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript">
        function getdata() {
            WebService.GetData(onReturn, onTimeOut);
        }
        function onReturn(result) {
            resultsdiv.innerHTML = result[0];
            resultsdiv.innerHTML += result[1];
            resultsdiv.innerHTML += result[2];
        }        
        function onTimeOut(result) {
            alert("oops");
        } 
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="WebService.asmx" />
        </Services> 
        </asp:ScriptManager>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>        
            <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
            <input type="button" onclick="getdata()" value="download" />
        </ContentTemplate>
    </asp:UpdatePanel>
                
        <div>
            <h1>data from a database</h1>
            <div id="resultsdiv">
            </div>
        </div>    
    </form>
</body>
</html>
