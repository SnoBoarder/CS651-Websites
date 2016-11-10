using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MidtermMVC.Controllers
{
    public class TranslatorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Format of the passed in JSON item
        /// </summary>
        public class Config
        {
            public string OriginalCharacter { get; set; }
            public string ReplacementCharacter { get; set; }
        }

        /// <summary>
        /// Decipher the passed in input based on the provided config
        /// </summary>
        /// <param name="input">Secret message that needs to be translated based on config</param>
        /// <param name="config">Config that tracks what original character needs to be replaced with</param>
        /// <returns>Translated message</returns>
        [HttpPost]
        public JsonResult Decipher(string input, List<Config> config)
        {
            if (input == null)
            {
                // input wasn't provided. skip out
                return Json(string.Empty);
            }

            if (config.Count == 0)
            {
                // no config was provided. skip out
                return Json(input);
            }

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
    }
}
