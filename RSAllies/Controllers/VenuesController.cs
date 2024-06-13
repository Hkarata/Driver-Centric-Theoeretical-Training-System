using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class VenuesController(SessionService sessionService, ApiClient apiClient) : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await apiClient.GetVenues();

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        public IActionResult Create()
        {
            if (sessionService.CheckAdmin())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public async Task<IActionResult> Venue(string id)
        {
            var result = await apiClient.GetVenue(Guid.Parse(id));

            return View(result.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVenue(Venue model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            var venue = new CreateVenueDto
            {
                Name = model.Name,
                DistrictId = model.DistrictId,
                RegionId = model.RegionId,
                Capacity = model.Capacity,
                ImageUrl = ImageUploadService.UploadVenueImage(model.ImageUrl),
                Address = model.Address
            };

            var result = await apiClient.CreateVenue(venue);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Venues");
            }

            return View("Create", model);

        }
    }
}
