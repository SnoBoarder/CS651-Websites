<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MET CS 651 - Lecture 6</title>

    <script src="jquery-1.3.1.js" type="text/javascript"></script>

    <script type="text/javascript">
        function getProducts(txtBoxId) {
            $('#divResults').hide();
            $('#divLoading').show();
            try {
                if (txtBoxId == null) {
                    $.ajax({
                        type: "POST",
                        url: "/Website3/jsonwebservice.asmx/GetProductsJson",
                        data: "{'prefix':''}",
                        contentType: "application/json; charset=utf-8",
                        success: ajaxCallSucceed,
                        dataType: "json",
                        failure: ajaxCallFailed
                    });
                    //JsonWebServiceWithJQuery/jsonwebservice.asmx?op=GetProducts
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "/Website3/jsonwebservice.asmx/GetProductsJson",
                        data: "{'prefix':'" + $('#' + txtBoxId).val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: ajaxCallSucceed,
                        dataType: "json",
                        failure: ajaxCallFailed
                    });
                }
            }
            catch (e) {
                alert('failed to call web service. Error: ' + e);
                $('#divLoading').hide();
            }
        }
        function ajaxCallSucceed(response) {
            //alert("success");
            $('#divLoading').hide();
            var products = eval('(' + response.d + ')');
            parseResult(products);
        }
        function ajaxCallFailed(error) {
            //alert("failed");
            $('#divLoading').hide();
            alert('error: ' + error);
            $('#divResults').hide();
        }
        function parseResult(products) {
            //alert("parsing..");
            var lists = '';
            for (var i = 0; i < products.length; i++) {
                if (lists == '') {
                    lists = products[i].ProductName;
                }
                else {
                    lists = lists + " <br />" + products[i].ProductName;
                }
            }
            $('#divResults').html(lists);
            $('#divResults').show();
        }
        function ff() {
            $.ajax({
                type: "POST",
                url: "/YourWebService.asmx/MethodName",
                data: "{'prefix':''}",
                contentType: "application/json; charset=utf-8",
                success: functionToCallWhenSucceed,
                dataType: "json",
                failure: functionToCallWhenFailed
            });
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <h2>MET CS 651 - Lecture 6: JQuery</h2>
        <br />
        <table cellspacing="5">
            <tr valign="top" style="height: 20px;">
                <td>
                    <asp:Label ID="lblPrefix" runat="server" Text="Prefix"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPrefix" runat="server" />
                </td>
                <td style="width: 400px;" rowspan="2">
                    Results:<br />
                    <div id="divLoading" style="display: none;">
                        Loading.....</div>
                    <div id="divResults" style="border: solid 1px green; padding: 3px; display: none;">
                    </div>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                    <input type="button" value="Get Products by prefix" onclick='getProducts("<%=txtPrefix.ClientID %>");' /><br />
                    <input type="button" value="Get All Products" onclick="getProducts();" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
