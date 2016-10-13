Fixing the bug issue was quite infuriating.. 

On the server side, I added:
contentType: "application/json; charset=utf-8",
dataType: "application/json; charset=utf-8",

Turns out both are needed! That got me return data on the client side, but it was in XML, not JSON!

Turns out jQuery actually never encodes the request into JSON, but a query string instead, causing asp.net to ignore the header and respond with XML (read http://encosia.com/3-mistakes-to-avoid-when-using-jquery-with-aspnet-ajax/)

So i encoded the input parameter in a JSON-specific way: 
var dataString = "{'id':1}";

instead of the original:
var dataString = "id=1";

Server-side:
No mods, but observe that both serializations (DataContractJsonSerializer and JavaScriptSerializer) seem to work fine :-)

cheers,
-dino