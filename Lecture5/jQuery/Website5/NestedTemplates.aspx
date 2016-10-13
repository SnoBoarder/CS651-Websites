<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NestedTemplates.aspx.cs" Inherits="NestedTemplates" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nested Templates</title>
    <script src="http://code.jquery.com/jquery.min.js" type="text/javascript"></script>
<script src="scripts/temp.js"type="text/javascript"></script>
<script src="scripts/json2.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

       var personCount;
       $(document).ready(function() {
           PopulateEmployees();
       });



       function PopulateEmployees() {
           $.ajax({
               type: "POST",
               url: "NestedTemplates.aspx/GetEmployees",
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

           var emps = eval(result.d);
           $("#empTemplate").tmpl(emps).appendTo($("#divEmployeedetails"));
       }
       
       
     </script>
    <script type="text/html" id="empTemplate">
       <div >
            <div style="float:left;font-weight:bold;"> ID : </div> <div>${EmployeeId} </div> 
            <div style="float:left;font-weight:bold;"> Name : </div> <div>${Name} </div> 
            <div style="float:left;font-weight:bold;"> Age : </div> <div>${Age} </div> 
            <div style="font-weight:bold;">Addresses:</div>
            <div style="margin-left:10px;">{{tmpl($data) "#AddressTemplate"}}</div>
            <hr />
       </div>
   </script>
   <script id="AddressTemplate" type="text/html">
       {{each Adresses}}
            <div style="float:left;font-weight:bold;"> Street : </div> <div>${Street} </div> 
            <div style="float:left;font-weight:bold;"> AddressLine1 : </div> <div>${AddressLine1} </div> 
            <div style="float:left;font-weight:bold;"> AddressLine2 : </div> <div>${AddressLine2} </div>
            <div style="float:left;font-weight:bold;"> City : </div> <div>${City} </div> 
            <div style="float:left;font-weight:bold;"> Zip : </div> <div>${Zip} </div><br />
       {{/each}}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divEmployeedetails" style="font-family:Verdana; font-size:12px;">
        <h2>Employee Details</h2>       
            
        </div>
    </form>
</body>
</html>
