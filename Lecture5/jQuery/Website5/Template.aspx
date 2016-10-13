<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Template.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Templates</title>
   <script type="text/html" id="personTemplate">
       <div>
            <div style="float:left;"> ID : </div> <div>${UId} </div> 
            <div style="float:left"> Name : </div> <div>${Name} </div> 
            <div style="float:left"> Address : </div> <div>${Address} </div> <br />
       </div>
   </script>
<script src="http://code.jquery.com/jquery.min.js" type="text/javascript"></script>
<script src="scripts/temp.js" type="text/javascript"></script>
<script src="scripts/json2.js" type="text/javascript"></script>
   <script type="text/javascript" language="javascript">

       var personCount;
       $(document).ready(function() {
           PopulatePersons();
       });


   
 function PopulatePersons() {
     $.ajax({
         type: "POST",
         url: "Template.aspx/GetPersons",
         contentType: "application/json; charset=utf-8",
         data: "{}",
         dataType: "json",
         success: AjaxSucceeded,
         error: AjaxFailed
     }); 


        }
        function AjaxSucceeded(result) {
            DisplayChildren(result);
        }
        function AjaxFailed(result) {
            alert('no success');
        }

        function DisplayChildren(result) {

            var persons = eval(result.d);
            personCount = persons.length;
                $("#personTemplate").tmpl(persons).appendTo($("#divPerson"));
        }
        function AddPerson() {

            var inputs = new Object();
            inputs.count = ++personCount;

            $.ajax({
                type: "POST",
                url: "Template.aspx/AddPerson",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(inputs),
                dataType: "json",
                success: AjaxSucceeded,
                error: AjaxFailed
            }); 
        
        } 

   </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <table>
    <tr><td><input id="Button1" type="button" value="AddMorePerson" onclick="AddPerson();"/></td></tr>
    <tr><td>
    
        <div id="divPerson" style="font-family:Verdana; font-size:12px;">
            
        
        </div>
    </td></tr>
        
    </table>
       <asp:HiddenField ID="hfPersonData" runat="server" />      
    </div>
    </form>
</body>
</html>
