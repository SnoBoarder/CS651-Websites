using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIModel.Controllers
{
    using WebAPIApplication.Model;
    
    [Route("api/[controller]")]
    public class BugsController : Controller
    {
        public IBugsRepository _bugsRepository = new BugsRepository();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET api/bugs
        [HttpGet]
        public string Get()
        {
            string bugs = string.Empty;

            IEnumerable<Bug> bugsRepo = _bugsRepository.GetBugsRepo();

            foreach (var b in bugsRepo)
            {
                bugs += ", " +
                    "id = " + b.id.ToString() +
                    ", title = " + b.description + 
                    ", state = " + b.state;
            }

            return bugs;
        }

        // GET api/bugs/5
        [HttpGetAttribute("{id}")]
        public string Get(int id)
        {
            var bug = _bugsRepository.GetBugsRepo().First(b => b.id == id);
            return "id = " + bug.id.ToString() +
                ", title = " + bug.title +
                ", description = " + bug.description +
                ", state = " + bug.state;
        }
    }
}
