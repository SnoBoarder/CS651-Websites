using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        //Dictionary<string, string> fruit = new Dictionary<string, string>();
        //fruit.Add("Apple", "Dry");
        //list.Add(fruit);

        //Dictionary<string, string> fruit2 = new Dictionary<string, string>();
        //fruit2.Add("Orange", "Juicy");
        //list.Add(fruit2);

        //[][][]Website 4
        List<string> list = new List<string>();
        list.Add("Apple");
        list.Add("Pear");
        list.Add("Orange");
        list.Add("Cherry");

        //.NET 1.0
        //string juicy = list.Find(new Predicate<string>(FindInList));

        ////.NET 2.0
        //string juicy = list.Find(delegate(string value)
        //{
        //    return value == "Orange";
        //});

        ////.NET 3.5
        string juicy = list.Find(value => "Orange" == value);

        TextBox1.Text = juicy;
    }

    static bool FindInList(string value)
    {
        return value == "Orange";
    }
}
