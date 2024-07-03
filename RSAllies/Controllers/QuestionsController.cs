using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class QuestionsController(SessionService sessionService, ApiClient apiClient) : Controller
    {

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TestResponses()
		{
			var form = Request.Form;

			var answers = new List<AnswerDto>();

			// Get the answers from the form

			foreach (var key in form.Keys)
			{
				var value = form[key];
				if (!Guid.TryParse(key, out Guid questionId) ||
					!Guid.TryParse(value, out Guid selectedChoiceId)) continue;
				var answer = new AnswerDto
				{
					QuestionId = questionId,
					ChoiceId = selectedChoiceId
				};

				answers.Add(answer);
			}

			// get user Id from session
			var userId = sessionService.GetUserId();

			// create user response

			var userResponse = new UserResponseDto
			{
				UserId = userId,
				Answers = answers
			};

			// send user response to the API
			var response = await apiClient.PostUserResponses(userResponse);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Home");
			}


			return View();
		}

	}
}
