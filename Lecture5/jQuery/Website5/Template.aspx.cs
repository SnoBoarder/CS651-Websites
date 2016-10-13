using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.Script.Serialization;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // hfPersonData.Value = GetPersons(string.Empty);
        
    }

    [WebMethod()]
    public static string GetPersons()
    {
        List<Person> persons = new List<Person>()
        {
            new Person { UId = 1, Name = "Bill", Address = "Gates"},
            new Person { UId = 2, Name = "Larry", Address = "Page" },
            new Person { UId = 3, Name = "Steve", Address = "Jobs"}
        };

        JavaScriptSerializer ser = new JavaScriptSerializer();
        // ser.Serialize(persons);
        return ser.Serialize(persons);

    }
    [WebMethod()]
    public static string AddPerson(string count)
    {
    
        List<Person> persons = new List<Person>()
        {
            new Person() { UId = Convert.ToInt32(count), Name = "New Name" + count, Address = "My New Address" + count }
        };

        JavaScriptSerializer ser = new JavaScriptSerializer();
        // ser.Serialize(persons);
        return ser.Serialize(persons);

    }

    

    public class Person
    {
        public int UId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    
}


