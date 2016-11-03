using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

// data tier
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectionstring = WebConfigurationManager.ConnectionStrings["AW2k"].ConnectionString;
        using (SqlConnection sqlCon = new SqlConnection(connectionstring))
        {
            // Website 5
            //string sql = "SELECT ProductID, Name, ProductNumber, ListPrice FROM SalesLT.Product WHERE ProductCategoryID = 6 ORDER BY Name";
            string sql = "SELECT ProductID, Name, ProductNumber, ListPrice FROM Production.Product WHERE ProductSubcategoryID <= 3 ORDER BY Name";
            SqlCommand cmd = new SqlCommand(sql, sqlCon);
            sqlCon.Open();
            SqlDataAdapter mySqlAdapter = new SqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            mySqlAdapter.Fill(myDataSet, "Products");

            // gridview isw a really cool control
            GridView1.DataSource = myDataSet;
            GridView1.DataBind();

	        // This is in case you want to return generic types or even bind with plain listboxes. 
	        // You see, DataBind() works with many controls!
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

            ListBox1.DataSource = results2.ToArray();
            ListBox1.DataBind();
        }
    }
}
