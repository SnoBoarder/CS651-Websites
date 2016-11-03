using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {
    //GRIDVIEW
    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    // Webservice
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat=System.Web.Script.Services.ResponseFormat.Json, UseHttpGet=true)]   
    public object[] GetData()
    {
        string connectionstring = WebConfigurationManager.ConnectionStrings["AW2k"].ConnectionString;
        using (SqlConnection sqlCon = new SqlConnection(connectionstring))
        {
            string sql = "SELECT ProductID, Name, ProductNumber, ListPrice FROM Production.Product WHERE ProductSubcategoryID <= 3 ORDER BY Name";
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            sqlCon.Open();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            mySqlAdapter.Fill(myDataSet, "Products");
            //GridView1.DataSource = myDataSet;
            //GridView1.DataBind();

            //Website 5 untyped
            DataTable dt = myDataSet.Tables["Products"];
            ArrayList results = new ArrayList();
            ArrayList results2 = new ArrayList();
            Dictionary<string, string>[] res = new Dictionary<string, string>[dt.Rows.Count];
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Dictionary<string, object>{
                                {"ProductID", row[0].ToString()},
                                {"Name", row[1].ToString() }});
                results2.Add(row[0].ToString() + ", " + row[1].ToString());
            }
            Dictionary<string, object>[] array = results.ToArray(typeof(Dictionary<string, object>)) as Dictionary<string, object>[];
            //return array;
            return results2.ToArray();
        }             
    }
    
}

