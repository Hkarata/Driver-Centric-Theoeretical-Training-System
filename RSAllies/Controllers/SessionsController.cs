using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class SessionsController(ApiClient apiClient) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await apiClient.GetSessionsAsync(cancellationToken);
            return result.IsSuccess ? View(result.Value) : View(null);
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

        public async Task<IActionResult> Details(string id, string startTime,
            string endTime, string date,
            string capacity, string venueName)
        {
            ViewBag.SessionId = id;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;
            ViewBag.Date = date;
            ViewBag.Capacity = capacity;
            ViewBag.VenueName = venueName;


            var result = await apiClient.GetSessionUsers(Guid.Parse(id));

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search()
        {
            var regionId = Request.Form["regionId"];
            var date = Request.Form["date"];

            if (!string.IsNullOrEmpty(date) || false)
            {
                var result = await apiClient.GetSessionByDate(DateTime.Parse(date!));
                return result.IsSuccess ? View("Index", result.Value) : View(null);
            }

            if (string.IsNullOrEmpty(regionId))
            {
                ModelState.AddModelError(string.Empty, "Please select a region");
                return View();
            }
            else
            {
                var result = await apiClient.GetSessionByRegionAndDate(regionId!);

                return result.IsSuccess ? View("Index",result.Value) : View(null);
            }
        }
    }
}
