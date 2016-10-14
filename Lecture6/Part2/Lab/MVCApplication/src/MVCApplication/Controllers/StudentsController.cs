using Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApplication.Controllers
{
    public class StudentsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GoodStudents(string name, int numTimes = 1)
        {
            clsStudents allStudents = new clsStudents();
            clsStudents goodStudents = new clsStudents();
            allStudents.LoadStudents();

            foreach(clsStudent student in allStudents)
            {
                if (student.Grade > 2.0)
                {
                    goodStudents.Add(student);
                }
            }

            ViewData["Message"] = goodStudents;
            ViewData["Number"] = goodStudents.Count;

            return View();
        }

        public IActionResult BadStudents()
        {
            clsStudents allStudents = new clsStudents();
            clsStudents badStudents = new clsStudents();
            allStudents.LoadStudents();

            foreach (clsStudent student in allStudents)
            {
                if (student.Grade <= 2.0)
                {
                    badStudents.Add(student);
                }
            }

            ViewData["Message"] = badStudents;
            ViewData["Number"] = badStudents.Count;

            return View();
        }
    }
}
