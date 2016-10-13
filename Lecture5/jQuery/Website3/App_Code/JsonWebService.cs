using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for JsonWebService
/// </summary>
/// [][][] Website3
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class JsonWebService : System.Web.Services.WebService 
{

    public JsonWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GetProductsJson(string prefix) 
    {
        List<Product> products = new List<Product>();
        if (prefix.Trim().Equals(string.Empty, StringComparison.OrdinalIgnoreCase))
        {
            products = ProductFacade.GetAllProducts();
        }
        else
        {
            products = ProductFacade.GetProducts(prefix);
        }

        //your object is your actual object (may be collection) you want to serialize to json
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(products.GetType());
        //create a memory stream
        MemoryStream ms = new MemoryStream();
        //serialize the object to memory stream
        serializer.WriteObject(ms, products);
        //convert the serizlized object to string
        string jsonString = Encoding.Default.GetString(ms.ToArray());
        //close the memory stream
        ms.Close();
        return jsonString;
    }
}

