using Microsoft.AspNetCore.Mvc;
using RSAllies.AzureStorage;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class TestController(ApiClient apiClient) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Analysis()
        {
            return View();
        }

        [ActionName("Create-Question")]
        public IActionResult CreateQuestion()
        {
            return View("CreateQuestion");
        }

        [ActionName("English-Questions")]
        public async Task<IActionResult> EnglishQuestion()
        {
            var result = await apiClient.GetEnglishQuestions();

            if (result.IsSuccess)
            {
                var questions = result.Value;
                return View("EnglishQuestion", questions);
            }

            return View("EnglishQuestion");
        }

        public IActionResult Results()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateQuestion", model);
            }

            // Upload the image
            var fileName = await StorageService.UploadFileAsync(Guid.NewGuid(), model.Image.OpenReadStream());

            // Add the question to the database
            var question = new CreateQuestionDto(
                    model.Scenario, 
                    fileName, 
                    model.Question, 
                    model.IsEnglish, 
                    new List<CreateChoiceDto> 
                    {
                        new CreateChoiceDto(model.ChoiceA, model.IsAnswerA),
                        new CreateChoiceDto(model.ChoiceB, model.IsAnswerB),
                        new CreateChoiceDto(model.ChoiceC, model.IsAnswerC),
                        new CreateChoiceDto(model.ChoiceD, model.IsAnswerD)
                    }
                );

            var result = await apiClient.CreateQuestion(question);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }


            return View("CreateQuestion", model);
        }
    }
}
