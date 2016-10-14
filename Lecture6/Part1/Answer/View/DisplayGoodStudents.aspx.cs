using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

public partial class DisplayGoodStudents : System.Web.UI.Page
{
    clsStudents objStud;
    protected void Page_Load(object sender, EventArgs e)
    {
        objStud = (clsStudents)Context.Items["Students"];
        dtgGoodStudents.DataSource = objStud;
        dtgGoodStudents.DataBind();
    }
}