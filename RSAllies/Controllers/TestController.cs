﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSAllies.Analytics;
using RSAllies.AzureStorage;
using RSAllies.Contracts.Requests;
using RSAllies.Contracts.Responses;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class TestController(ApiClient apiClient, SessionService sessionService, ApiService apiService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Manage()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            var result = await apiClient.GetAllQuestions();

            if (result.IsSuccess)
            {
                return View(result.Value);
            }

            return View();
        }

        public async Task<IActionResult> EditQuestion(string questionId)
        {
            var result = await apiClient.GetQuestion(questionId);

            if (result.IsSuccess)
            {
                return View(result.Value);
            }

            return RedirectToAction("Manage", "Test");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(string questionId, string oldImage = "")
        {
            var form = Request.Form;

            var scenario = form["Scenario"];
            var question = form["QuestionText"];
            var isEnglish = form["IsEnglish"];
            var imageFile = Request.Form.Files["Image"];
            var choiceA = form["Choice-1"];
            var choiceB = form["Choice-2"];
            var choiceC = form["Choice-3"];
            var choiceD = form["Choice-4"];
            bool isAnswerA = false;
            bool isAnswerB = false;
            bool isAnswerC = false;
            bool isAnswerD = false;

            int i = 1; // start
            int max = 4; // end

            while (i <= max)
            {
                var choice = form[$"Choice-{i}"];
                var isAnswer = form[$"IsAnswer-{i}"];

                if (isAnswer == "on")
                {
                    if (i == 1)
                    {
                        isAnswerA = true;
                    }
                    else if (i == 2)
                    {
                        isAnswerB = true;
                    }
                    else if (i == 3)
                    {
                        isAnswerC = true;
                    }
                    else if (i == 4)
                    {
                        isAnswerD = true;
                    }
                }

                i++;
            }

            string imageUrl = string.Empty;

            if (imageFile is not null)
            {
                imageUrl = StorageService.UploadFileAsync(Guid.NewGuid(), imageFile.OpenReadStream()).Result;
            }
            else
            {
                imageUrl = oldImage;
            }

            var questionDto = new CreateQuestionDto(
                scenario,
                imageUrl,
                question!,
                isEnglish == "on",
                new List<CreateChoiceDto>
                {
                    new CreateChoiceDto(choiceA!, isAnswerA),
                    new CreateChoiceDto(choiceB!, isAnswerB),
                    new CreateChoiceDto(choiceC!, isAnswerC),
                    new CreateChoiceDto(choiceD!, isAnswerD)
                }
            );

            await apiClient.EditQuestion(questionId, questionDto);

            return RedirectToAction("Manage", "Test");
        }

        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            var result = await apiClient.DeleteQuestion(questionId);

            return RedirectToAction("Manage", "Test");
        }

        [ActionName("Create-Question")]
        public IActionResult CreateQuestion()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            return View("CreateQuestion");
        }

        [ActionName("English-Questions")]
        public async Task<IActionResult> EnglishQuestion()
        {
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await apiClient.GetEnglishQuestions();

            if (result.IsSuccess)
            {
                var questions = result.Value;
                return View("EnglishQuestion", questions);
            }

            return View("EnglishQuestion");
        }

		[ActionName("Swahili-Questions")]
		public async Task<IActionResult> SwahiliQuestions()
		{
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await apiClient.GetSwahiliQuestions();

			if (result.IsSuccess)
			{
				var questions = result.Value;
				return View("SwahiliQuestions", questions);
			}

			return View("SwahiliQuestions");
		}

		public async Task<IActionResult> Results()
        {
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = sessionService.GetUserId();

            var result = await apiClient.GetUserScore(userId);

            if (result.IsSuccess)
            {
                return View(result.Value);
            }

            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateQuestion", model);
            }

            string fileName = string.Empty;

            if (model.Image is not null)
            {
                // Upload the image
                fileName = await StorageService.UploadFileAsync(Guid.NewGuid(), model.Image.OpenReadStream());
            }

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
                return RedirectToAction("Create-Question");
            }


            return View("CreateQuestion", model);
        }



        public async Task<IActionResult> Analysis()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            await SeedScores();
            await SeedTestAgeGroupAnalysis();
            await SeedTestGenderData();
            await TestPassRetakeAnalysis();

            var response = await apiService.GetTestRetakeAnalysis();

            if (response.IsSuccess)
            {
                ViewBag.RetakeCount = response.Value;
            }
            else
            {
                ViewBag.RetakeCount = "No test retakes";
            }

            return View();
        }

        public async Task SeedScores()
        {
            var response = await apiService.GetScoresAnalysis();

            var data = response.Value;

            int[] scores = new int[] { 0, 0, 0, 0, 0 };

            foreach (var item in data)
            {
                if (item.ScoreRange == "0-5")
                {
                    scores[0] = item.Count;
                }
                else if (item.ScoreRange == "6-10")
                {
                    scores[1] = item.Count;
                }
                else if (item.ScoreRange == "11-15")
                {
                    scores[2] = item.Count;
                }
                else if (item.ScoreRange == "16-20")
                {
                    scores[3] = item.Count;
                }
                else if (item.ScoreRange == "21-25")
                {
                    scores[4] = item.Count;
                }
            }

            ViewBag.Scores = scores;
        }

        public async Task SeedTestAgeGroupAnalysis()
        {
            var response = await apiService.GetTestAgeGroupAnalysis();

            int[] TotalResponses = new int[] { 0, 0, 0, 0, 0, 0 };
            int[] CorrectResponses = new int[] { 0, 0, 0, 0, 0, 0 };
            int[] IncorrectResponses = new int[] { 0, 0, 0, 0, 0, 0 };

            if (response.IsSuccess)
            {
                var data = response.Value;

                foreach (var item in data)
                {
                    if (item.AgeGroup == "18-25")
                    {
                        TotalResponses[0] = item.TotalResponses;
                        CorrectResponses[0] = item.CorrectResponses;
                        IncorrectResponses[0] = item.IncorrectResponses;
                    }
                    else if (item.AgeGroup == "26-33")
                    {
                        TotalResponses[1] = item.TotalResponses;
                        CorrectResponses[1] = item.CorrectResponses;
                        IncorrectResponses[1] = item.IncorrectResponses;
                    }
                    else if (item.AgeGroup == "34-41")
                    {
                        TotalResponses[2] = item.TotalResponses;
                        CorrectResponses[2] = item.CorrectResponses;
                        IncorrectResponses[2] = item.IncorrectResponses;
                    }
                    else if (item.AgeGroup == "42-49")
                    {
                        TotalResponses[3] = item.TotalResponses;
                        CorrectResponses[3] = item.CorrectResponses;
                        IncorrectResponses[3] = item.IncorrectResponses;
                    }
                    else if (item.AgeGroup == "50-55")
                    {
                        TotalResponses[4] = item.TotalResponses;
                        CorrectResponses[4] = item.CorrectResponses;
                        IncorrectResponses[4] = item.IncorrectResponses;
                    }
                    else if (item.AgeGroup == "55+")
                    {
                        TotalResponses[5] = item.TotalResponses;
                        CorrectResponses[5] = item.CorrectResponses;
                        IncorrectResponses[5] = item.IncorrectResponses;
                    }
                }

            }

            ViewBag.TotalResponses = JsonConvert.SerializeObject(TotalResponses);
            ViewBag.CorrectResponses = JsonConvert.SerializeObject(CorrectResponses);
            ViewBag.IncorrectResponses = JsonConvert.SerializeObject(IncorrectResponses);

        }

        public async Task SeedTestGenderData()
        {
            var response1 = await apiService.GetGenderTestAnalysis();

            int[] maleData = new int[] { 0, 0 };
            int[] femaleData = new int[] { 0, 0 };

            if (response1.IsSuccess)
            {
                var dataset = response1.Value;

                foreach (var data in dataset)
                {
                    if (data.GenderType == "Male")
                    {
                        maleData[0] = data.Passed;
                    }
                    else
                    {
                        femaleData[0] = data.Passed;
                    }
                }

            }

            var response2 = await apiService.GetTestRetakeGenderAnalysis();

            if (response2.IsSuccess)
            {
                var dataset = response2.Value;

                foreach (var data in dataset)
                {
                    if (data.Gender == "Male")
                    {
                        maleData[1] = data.RetakeCount;
                    }
                    else
                    {
                        femaleData[1] = data.RetakeCount;
                    }
                }
            }

            ViewBag.MaleData = JsonConvert.SerializeObject(maleData);
            ViewBag.FemaleData = JsonConvert.SerializeObject(femaleData);

        }

        public async Task TestPassRetakeAnalysis()
        {
            var response1 = await apiService.GetTestPassAgeGroupAnalysis();

            int[] Passes = new int[] { 0, 0, 0, 0, 0, 0 };
            int[] Retakes = new int[] { 0, 0, 0, 0, 0, 0 };

            if (response1.IsSuccess)
            {
                var dataset = response1.Value;

                foreach (var data in dataset)
                {
                    if (data.AgeGroup == "18-25")
                    {
                        Passes[0] = data.Count;
                    }
                    else if (data.AgeGroup == "26-33")
                    {
                        Passes[1] = data.Count;
                    }
                    else if (data.AgeGroup == "34-41")
                    {
                        Passes[2] = data.Count;
                    }
                    else if (data.AgeGroup == "42-49")
                    {
                        Passes[3] = data.Count;
                    }
                    else if (data.AgeGroup == "50-55")
                    {
                        Passes[4] = data.Count;
                    }
                    else if (data.AgeGroup == "55+")
                    {
                        Passes[5] = data.Count;
                    }
                }
            }

            var response2 = await apiService.GetTestRetakeAgeGroupAnalysis();

            if (response2.IsSuccess)
            {
                var dataset = response2.Value;

                foreach (var data in dataset)
                {
                    if (data.AgeGroup == "18-25")
                    {
                        Retakes[0] = data.RetakeCount;
                    }
                    else if (data.AgeGroup == "26-33")
                    {
                        Retakes[1] = data.RetakeCount;
                    }
                    else if (data.AgeGroup == "34-41")
                    {
                        Retakes[2] = data.RetakeCount;
                    }
                    else if (data.AgeGroup == "42-49")
                    {
                        Retakes[3] = data.RetakeCount;
                    }
                    else if (data.AgeGroup == "50-55")
                    {
                        Retakes[4] = data.RetakeCount;
                    }
                    else if (data.AgeGroup == "55+")
                    {
                        Retakes[5] = data.RetakeCount;
                    }
                }
            }

            ViewBag.Passes = JsonConvert.SerializeObject(Passes);
            ViewBag.Retakes = JsonConvert.SerializeObject(Retakes);

        }
    }
}
