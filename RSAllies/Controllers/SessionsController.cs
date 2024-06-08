using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class SessionsController(ApiClient apiClient) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string id, string venueName)
        {
            ViewBag.VenueId = Guid.Parse(id);
            ViewBag.VenueName = venueName;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSession session)
        {
            if (ModelState.IsValid)
            {
                var request = new CreateSessionDto(session.Date, session.StartTime, session.EndTime, session.VenueId);

                var result = await apiClient.CreateSession(request);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Venue", "Venues", new
                    {

                        id = session.VenueId
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "A session already exists for the given date and time");
                }

            }
            return View(session);
        }

        public IActionResult Details(string id)
        {
            ViewBag.SessionId = Guid.Parse(id);

            return View();
        }
    }
}
