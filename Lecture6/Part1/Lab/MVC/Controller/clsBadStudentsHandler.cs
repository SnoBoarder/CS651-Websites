using Model;
using System;
using System.Web;

namespace Controller
{
    public class clsBadStudentsHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            clsStudents objStudents = new clsStudents();
            objStudents.LoadStudents();
            objStudents.EvaluateStudentsByGPA(double.MinValue, 2.0);
            context.Items.Add("Students", objStudents);
            context.Server.Transfer("./DisplayBadStudents.aspx");
        }
    }
}
