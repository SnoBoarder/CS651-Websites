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
        //    new Person { FirstName="Pino", LastName="Konstantopoulos", Age=42 },
        //    new Person { FirstName="Neha", LastName="Abrol", Age=21 },
        //    new Person { FirstName="Ryan", LastName="Johnson", Age=21 },
        //    new Person { FirstName="Prakash", LastName="Lekhak", Age=21 },
        //    new Person { FirstName="Bart", LastName="Mburu", Age=21 },
        //    new Person { FirstName="Jorge", LastName="Rodriguez", Age=21 },
        //    new Person { FirstName="Chih-Yung", LastName="Wu", Age=21 },
        //    new Person { FirstName="Mohammed", LastName="Mohammed", Age=21 }
        //};

        //IEnumerable<Person> queryResult;

        //queryResult = people.Where(p => p.FirstName.StartsWith("P"));

        //foreach (Person p in queryResult)
        //{
        //    ListBox1.Items.Add(p.FirstName + ", " + p.LastName);
        //}
        //double averageAge = people.Average(p => p.Age);
        //int maxAge = people.Max(p => p.Age);


        ////
        //// 2. XML Documents (weak typing)
        ////

        ////Part 2: XML documents (weak typing)
        string filename = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "App_Data\\people.xml";
        XDocument people = XDocument.Load(filename);
        //IEnumerable<XElement> queryResult;
        //queryResult = people.Descendants("person").Where(p => p.Element("FirstName").Value.StartsWith("P"));
        //foreach (XElement p in queryResult)
        //{
        //    ListBox1.Items.Add(p.Element("FirstName").Value + ", " + p.Element("LastName").Value);
        //}

        ////
        //// 2. XML Documents (strong typing)
        ////

        ////Part 2: XML documents (strong typing)
        //IEnumerable<Person> queryResult;
        //queryResult = people.Descendants("person")
        //    .Where(p => p.Element("FirstName").Value.StartsWith("P"))
        //    .Select(p => new Person
        //    {
        //        FirstName = p.Element("FirstName").Value,
        //        LastName = p.Element("LastName").Value,
        //        Age = Convert.ToInt32(p.Attribute("age").Value)
        //    });

        //foreach (Person p in queryResult)
        //{
        //    ListBox1.Items.Add(p.FirstName + ", " + p.LastName);
        //}

        ////
        //// 3. Databases
        ////

        ////Part 3: Databases
        string connectionstring = WebConfigurationManager.ConnectionStrings["Aw2k"].ConnectionString;
        DataClasses2DataContext aw = new DataClasses2DataContext(connectionstring);

        //IEnumerable<Employee> queryResult;
        //queryResult = aw.Employees.Where(p => p.FirstName.StartsWith("M"));
        //foreach (Employee p in queryResult)
        //{
        //    ListBox1.Items.Add(p.FirstName + ", " + p.LastName);
        //}

        IEnumerable<Product> queryResult;
        queryResult = aw.Products.Where(p => p.Name.StartsWith("P"));
        foreach (Product p in queryResult)
        {
            ListBox1.Items.Add(p.Name + ", " + p.ProductID.ToString());
        }

        ////
        //// 4. Update the darn database!!
        ////

        ////Part4: Update the database!
        //Employee unlucky = aw.Employees.First(p => p.FirstName.StartsWith("M"));
        //ListBox1.Items.Add(unlucky.FirstName + ", " + unlucky.LastName + " (" + unlucky.Title + ") was just fired!");
        //unlucky.Title = "Fired";
        //aw.SubmitChanges();

    }
}
