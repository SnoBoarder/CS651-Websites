using Model;
using System;
using System.Web;

namespace Controller
{
    public class clsGoodStudentsHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            clsStudents objStudents = new clsStudents();
            objStudents.LoadStudents();
            objStudents.EvaluateStudentsByGPA(2.0, double.MaxValue);
            context.Items.Add("Students", objStudents);
            context.Server.Transfer("./DisplayGoodStudents.aspx");
        }
    }
}
