using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

public partial class DisplayBadStudents : System.Web.UI.Page
{
    clsStudents objStud;
    protected void Page_Load(object sender, EventArgs e)
    {
        objStud = (clsStudents)Context.Items["Students"];
        dtgBadStudents.DataSource = objStud;
        dtgBadStudents.DataBind();
    }
}