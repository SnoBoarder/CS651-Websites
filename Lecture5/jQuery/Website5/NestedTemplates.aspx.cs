using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class NestedTemplates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod()]
    public static string GetEmployees()
    {
        List<Employee> persons = new List<Employee>()
        {
            new Employee { EmployeeId = 1001, Age=22, Name ="Bill", 
                Adresses = new List<Address>()},
            new Employee { EmployeeId = 1000, Age=22, Name ="Larry", 
                Adresses = new List<Address>()},
            new Employee { EmployeeId = 1002, Age=22, Name ="Steve", 
                Adresses = new List<Address>()}

        };
        persons[0].Adresses.Add(new Address() { Street = "Beacon Street", AddressLine1 = "Chestnut Hill", AddressLine2 = "Brighton", City = "Boston", Zip = "02467" });
        persons[0].Adresses.Add(new Address() { Street = "Green Street", AddressLine1 = "Coolidge Corner", AddressLine2 = "-", City = "Brookline", Zip = "02446" });

        persons[1].Adresses.Add(new Address() { Street = "Street No 2", AddressLine1 = "Pocket Gama", AddressLine2 = "Near Apollo Hospital", City = "Moscow", Zip = "201301" });
        persons[1].Adresses.Add(new Address() { Street = "1634", AddressLine1 = "Sector 15", AddressLine2 = "Near Nirulas", City = "Moscow", Zip = "201301" });

        persons[2].Adresses.Add(new Address() { Street = "Street 10", AddressLine1 = "Sector 18", AddressLine2 = "Kurosawa", City = "Tokyo", Zip = "201301" });
        persons[2].Adresses.Add(new Address() { Street = "Gol Marg", AddressLine1 = "New Era Colony", AddressLine2 = "Kannichiwa", City = "Tokyo", Zip = "221001" });
        
        JavaScriptSerializer ser = new JavaScriptSerializer();
        // ser.Serialize(persons);
        return ser.Serialize(persons);
    }


}

public class Employee
{

    public int EmployeeId { get; set; }
    public String Name { get; set; }
    public int Age { get; set; }
    public List<Address> Adresses { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public String AddressLine1 { get; set; }
    public String AddressLine2 { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
}
