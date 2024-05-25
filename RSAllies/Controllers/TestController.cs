using Microsoft.AspNetCore.Mvc;

namespace RSAllies.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Create-Question")]
        public IActionResult CreateQuestion()
        {
            return View("CreateQuestion");
        }

        [ActionName("English-Questions")]
        public IActionResult EnglishQuestion()
        {
            return View("EnglishQuestion");
        }
    }
}
