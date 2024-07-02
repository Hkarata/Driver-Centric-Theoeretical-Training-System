using Microsoft.AspNetCore.Mvc;
using RSAllies.AzureStorage;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class VenuesController(SessionService sessionService, ApiClient apiClient) : Controller
    {

        const string sasToken = "?sv=2022-11-02&ss=b&srt=sco&sp=rwdlaciytfx&se=2024-07-30T18:05:10Z&st=2024-06-25T10:05:10Z&spr=https,http&sig=uKay28N0zByr8tUiGDPIOZflPIJoWcWO8xtQg8WMpEI%3D";

        public async Task<IActionResult> Index()
        {
            if (!sessionService.Check() ^ sessionService.CheckAdmin())
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await apiClient.GetVenues();

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        public IActionResult Create()
        {
            if (!sessionService.CheckAdmin())
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

            var imageUrl = StorageService.
                UploadFileAsync(Guid.NewGuid(), model.ImageUrl.OpenReadStream())
                .Result;

            var venue = new CreateVenueDto
            {
                Name = model.Name,
                DistrictId = model.DistrictId,
                RegionId = model.RegionId,
                Capacity = model.Capacity,
                ImageUrl = imageUrl + sasToken,
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
