<%@ WebService Language="C#" Class="PeopleIKnow" %>

using System;
using System.Collections;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class PeopleIKnow  : System.Web.Services.WebService {

    [WebMethod]
    public object[] GetPeople() {
        return new object[] {
            new {FirstName = "Dino", LastName = "Konstantopoulos", Photo = "dino_biz2.jpg"},
            new {FirstName = "Jason", LastName = "Campoli", Photo = "batman.gif"},
            new {FirstName = "Autumn", LastName = "Hurrier", Photo = "greenlantern.gif"},
            new {FirstName = "Javier", LastName = "Kielmanowicz", Photo = "spiderman.gif"},
            new {FirstName = "Kolm", LastName = "Suzanne", Photo = "superman.gif"},
            new {FirstName = "Mays", LastName = "Lindsay", Photo = "batman.gif"},
            new {FirstName = "Chris", LastName = "Tucker", Photo = "greenlantern.gif"}
        };
    }
    
}

