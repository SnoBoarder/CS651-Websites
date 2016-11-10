using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MidtermMVC.Controllers
{
    public class FrequencyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// organized list of most frequent letters based on the following URL:
        /// https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
        /// </summary>
        /// <returns>Letter frequency string array</returns>
        [HttpGet]
        public JsonResult Letter()
        {
            return Json(new string[] { "e", "t", "a", "o", "i", "n", "s", "h", "r", "d", "l", "c", "u", "m", "w", "f", "g", "y", "p", "b", "v", "k", "j", "x", "q", "z" });
        }

        /// <summary>
        /// organized list of most frequent bigrams based on the following URL:
        /// https://en.wikipedia.org/wiki/Bigram
        /// </summary>
        /// <returns>Bigram frequency string array</returns>
        [HttpGet]
        public JsonResult Bigram()
        {
            return Json(new string[] { "th", "he", "in", "er", "an", "re", "nd", "at", "on", "nt", "ha", "es", "st", "en", "ed", "to", "it", "ou", "ea", "hi", "is", "or", "ti", "as", "te", "et", "ng", "of", "al", "de", "se", "le", "sa", "si", "ar", "ve", "ra", "ld", "ur" });
        }

        /// <summary>
        /// organized list of most frequent trigrams based on the following URL:
        /// https://en.wikipedia.org/wiki/Trigram
        /// </summary>
        /// <returns>Trigram frequency string array</returns>
        [HttpGet]
        public JsonResult Trigram()
        {
            return Json(new string[] { "the", "and", "tha", "ent", "ing", "ion", "tio", "for", "nde", "has", "nce", "edt", "tis", "oft", "sth", "men" });
        }
    }
}
