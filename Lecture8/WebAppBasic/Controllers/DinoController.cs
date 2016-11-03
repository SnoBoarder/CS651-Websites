using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppBasic.Controllers
{
    public class DinoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<string> studentList = new List<string>();
            studentList.Add("Bill");
            studentList.Add("Steve");
            studentList.Add("Ram");

            ViewData["students"] = studentList;
            
            return View();
        }
    }
}
