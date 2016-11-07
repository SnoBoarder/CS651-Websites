using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    public class Config
    {
        public string InputLetter { get; set; }
        public string SubstituteLetter { get; set; }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Deciphering(string input, string config)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        Config[] configValues = jsonSerializer.Deserialize<Config[]>(config);

        string output = input;

        foreach (Config c in configValues)
        {
            output = output.Replace(c.InputLetter, c.SubstituteLetter);
        }

        return jsonSerializer.Serialize(output);
    }

}
