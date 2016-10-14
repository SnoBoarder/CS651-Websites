using Model;
using System;

namespace View
{
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
}