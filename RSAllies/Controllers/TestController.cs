using Microsoft.AspNetCore.Mvc;
using RSAllies.Models;
using RSAllies.Services;

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

        public IActionResult Results()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuestion(QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateQuestion", model);
            }

            // Upload the image
            var fileName = ImageUploadService.UploadImage(model.Image);

            // Add the question to the database



            return View("CreateQuestion", model);
        }
    }
}
