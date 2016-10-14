using Model;
using System;

namespace View
{
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
}