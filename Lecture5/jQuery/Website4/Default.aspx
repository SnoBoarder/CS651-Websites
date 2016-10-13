<%@ Page language="c#" Inherits="AJAXJQuerySample._Default" CodeFile="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Default</title>
    <script type="text/javascript" src="jquery-1.1.js"></script>
    <script type="text/javascript" src="form.js"></script>
    <script type="text/javascript">
    
		$(function() {
			//this code is executed when the page's onload event fires
			$("a#runSample1").click(function() {
				$.get("GetServerTime.aspx", function(response) {
					alert(response);
				});
			});
			
			$("a#editName").click(function() {
				$.get("ChangeName.aspx", function(response) {
					$("div#divEdit").html(response).show();
					$("div#divView").hide();
					
					var options = {
						method: 'POST',
						url: 'ChangeName.aspx',
						after: function(response) {
							$("div#divView").html(response).show();
							$("div#divEdit").empty().hide();
							$("a#editName").show();
						}
					};
					//bind to form’s onsubmit event
					$("form#ChangeName").ajaxForm(options);
					
					//hide the "edit" link
					$("a#editName").hide();
					//wire up the cancel link
					$("a#lnkCancel").click(function() {
						$("div#divView").show();
						$("div#divEdit").empty().hide();
						$("a#editName").show();
					});
				});
			});
		});
    </script>
  </head>
  <body>
	
    <form id="Form1" method="post">
    
    <h1>Using jQuery for AJAX in ASP.NET</h1>
    
    <a href="#" id="runSample1">Get Server Time</a>
    
    <br/><br/>
    
    <div style="width:350px">
		<div style="background:#CCC">&nbsp;<a href="#" id="editName">Edit</a></div>
		<div id="divView"><asp:literal id="litName" runat="server"/></div>
		<div id="divEdit" style="display:none"></div>
    </div>

    </form>
	
  </body>
</html>
