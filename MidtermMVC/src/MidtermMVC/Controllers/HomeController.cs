using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MidtermMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public class DataModel
        {
            public string input { get; set; }
            public List<Config> config { get; set; }
        }

        public class Config
        {
            public string OriginalCharacter { get; set; }
            public string ReplacementCharacter { get; set; }
        }

        [HttpPost]
        public JsonResult Decipher(string input, List<Config> config)
        {
            // convert config to dictionary
            Dictionary<string, string> configDict = new Dictionary<string, string>();
            Config c;
            for (int i = 0; i < config.Count; ++i)
            {
                c = config[i];
                configDict[c.OriginalCharacter] = c.ReplacementCharacter;
            }

            // handle output
            StringBuilder sb = new StringBuilder();

            char[] chars = input.ToCharArray();
            string inputChar;
            for (int i = 0; i < chars.Length; ++i)
            {
                inputChar = chars[i].ToString();
                sb.Append(configDict.ContainsKey(inputChar) ? configDict[inputChar] : inputChar);
            }

            return Json(sb.ToString());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
