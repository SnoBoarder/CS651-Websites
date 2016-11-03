using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;  
using System.Xml.Linq;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    //[][][] Website5
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //
        // Website 3: 1. .NET Objects
        //

        ////Part 1: .NET Objects
        //List<Person> people = new List<Person>{
        //    new Person { FirstName="Dino", LastName="Konstantopoulos", Age=42 },
        //    new Person { FirstName="Dill", LastName="Gates", Age=62 },
        //    new Person { FirstName="Roger", LastName="Federer", Age=32 },
        //    new Person { FirstName="Katy", LastName="Perry", Age=22 }
        //};
        //IEnumerable<Person> queryResult;
        //queryResult = people.Where(z => z.FirstName.StartsWith("D"));
        //foreach (var p in queryResult)
        //{
        //    ListBox1.Items.Add(p.FirstName + ", " + p.LastName);
        //}
        //double averageAge = people.Average(p => p.Age);
        //int maxAge = people.Max(p => p.Age);

        //
        // 2. XML Documents (weak typing)
        //

        //Part 2: XML documents (weak typing)
        //string filename = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "App_Data\\people.xml";
        //XDocument people = XDocument.Load(filename);
        //IEnumerable<XElement> queryResult;
        //queryResult = people.Descendants("person").Where(p => p.Element("FirstName").Value.StartsWith("D"));
        //foreach (XElement p in queryResult)
        //{
        //    ListBox1.Items.Add(p.Element("FirstName").Value + ", " + p.Element("LastName").Value);
        //}

        //
        // 2. XML Documents (strong typing)
        //



    }
}
