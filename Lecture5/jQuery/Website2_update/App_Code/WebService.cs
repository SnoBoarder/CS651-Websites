using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
// [][][] Website2
//code-behind - using
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    // ~dk: Adding these attributes to the class is more correct,
    // but it also works without, so i got rid of them! :-)
    //[System.Runtime.Serialization.DataContractAttribute]
    //[System.Runtime.Serialization.DataContract]
    public class Person
    {
        //[System.Runtime.Serialization.DataMember]
        public string FirstName;

        //[System.Runtime.Serialization.DataMember]
        public string LastName;

        //[System.Runtime.Serialization.DataMember]
        public string Photo;
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string LoremIpsum(int id)
    {
        return "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string X2(int id)
    {
        //object o = new {FirstName = "Dino", LastName = "Konstantopoulos", Photo = "dino_biz2.jpg"};
        Person o = new Person{ FirstName = "Dino", LastName = "Konstantopoulos", Photo = "dino_biz2.jpg" };
        
        // Method #1
        // JSON serializer
        //your object is your actual object (may be collection) you want to serialize to json
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(o.GetType());
        //create a memory stream
        MemoryStream ms = new MemoryStream();
        //serialize the object to memory stream
        serializer.WriteObject(ms, o);
        //convert the serialized object to string
        string jsonString = Encoding.Default.GetString(ms.ToArray());
        //close the memory stream
        ms.Close();
        return jsonString;

        // Method #2 (this also works!)
        //JavaScriptSerializer ser = new JavaScriptSerializer();
        //var ret = ser.Serialize(o);
        //return ret;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string X3(int id)
    {
        Person[] os = new Person[] {
            new Person{FirstName = "Dino", LastName = "Konstantopoulos", Photo = "dino_biz2.jpg"},
            new Person{FirstName = "Bill", LastName = "Gates", Photo = null as string},
            new Person{FirstName = "Barack", LastName = "Obama", Photo = null as string},
            new Person{FirstName = "Hillary", LastName = "Clinton", Photo = null as string},
            new Person{FirstName = "John", LastName = "oehnerU", Photo = null as string},
            new Person{FirstName = "Katy", LastName = "Perry", Photo = null as string},
            new Person{FirstName = "Placebo", LastName = "", Photo = null as string},
            new Person{FirstName = "Jimmy", LastName = "Kimmel", Photo = null as string}
        };

        //your object is your actual object (may be collection) you want to serialize to json
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(os.GetType());
        //create a memory stream
        MemoryStream ms = new MemoryStream();
        //serialize the object to memory stream
        serializer.WriteObject(ms, os);
        //convert the serizlized object to string
        string jsonString = Encoding.Default.GetString(ms.ToArray());
        //close the memory stream
        ms.Close();
        return jsonString;
    }
}

