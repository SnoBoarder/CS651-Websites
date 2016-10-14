using Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCApplication.Controllers
{
    public class StudentsController : Controller
    {
        /// <summary>
        /// The Home View should come up as a default for the entire Web
        /// site(if you browse to http://localhost:50833/Students ), or if you
        /// browse specifically to the Home Controller:
        /// http://localhost:50833/Students/Home
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Home()
        {
            return RedirectToAction("Index", "Home");
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
