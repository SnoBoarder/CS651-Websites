using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using Model;

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
            //throw new NotImplementedException();
            clsStudents objStudents = new clsStudents();
            objStudents.LoadStudents();
            context.Items.Add("Students", objStudents);
            context.Server.Transfer("./DisplayBadStudents.aspx");
        }
    }


    public class clsGoodStudentsHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //throw new NotImplementedException();
            clsStudents objStudents = new clsStudents();
            objStudents.LoadStudents();
            context.Items.Add("Students", objStudents);
            context.Server.Transfer("./DisplayGoodStudents.aspx");
        }
    }

}
