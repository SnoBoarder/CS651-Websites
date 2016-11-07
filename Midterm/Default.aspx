<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/jsgrid.min.css" />
    <link type="text/css" rel="stylesheet" href="css/jsgrid-theme.min.css" />

    <script type="text/javascript" src="js/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="js/jsgrid.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="Input" TextMode="MultiLine" Wrap="true" runat="server"></asp:TextBox>
            <asp:TextBox ID="Output" TextMode="MultiLine" Wrap="true" ReadOnly="true" runat="server"></asp:TextBox>
            <div id="jsGrid"></div>
            <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Submit" />
        </div>

        <script>
            var clients = [
                //{ "InputLetter": "e", "SubstituteLetter": "f" },
            ];
            
            var onGridUpdate = function (args) {
                //alert(args.grid.data[0].InputLetter + "|" + args.grid.data[0].SubstituteLetter);

                var data = {
                    input: $("#Input").val(),
                    config: JSON.stringify(args.grid.data)
                };

                var dataString = JSON.stringify(data);

                $.ajax({
                    type: "POST",
                    url: "WebService.asmx/Deciphering",
                    contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    data: dataString,
                    success: function (data) {
                        var str = JSON.parse(data).d.replace(/"/g, "");
                        $('#Output').val(str);
                    },
                    error: function (msg) {
                        alert("Error: " + msg.responseText);
                    }
                });
            };

            $("#jsGrid").jsGrid({
                width: "100%",
                height: "400px",

                inserting: true,
                editing: true,
                sorting: true,
                paging: true,

                data: clients,

                onItemInserted: onGridUpdate,
                onItemDeleted: onGridUpdate,
                onItemUpdated: onGridUpdate,

                fields: [{
                    name: "InputLetter",
                    type: "text",
                    rangeLength: 1,
                    validate: [
                        "required",
                        {
                            validator: "rangeLength",
                            message: function (value, item) {
                                return "Must have only 1 character.";
                            },
                            param: [1, 1]
                        }
                    ]
                },
                {
                    name: "SubstituteLetter",
                    type: "text",
                    validate: [
                        "required",
                        {
                            validator: "rangeLength",
                            message: function (value, item) {
                                return "Must have only 1 character.";
                            },
                            param: [1, 1]
                        }
                    ]
                },
                {
                    type: "control"
                }]
            });
        </script>
    </form>
</body>
</html>
