using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class VenuesController(SessionService sessionService) : Controller
    {
        public IActionResult Index()
        {
            if (sessionService.Check())
            {

                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Districts = new SelectList(FileService.GetDistricts(), "Id", "Name");
            ViewBag.Regions = new SelectList(FileService.GetRegions(), "Id", "Name");
            return View();
        }

        public IActionResult Venue(string id, string venueName, string district, string region, string Capacity, string contact)
        {
            ViewBag.Id = id;
            ViewBag.VenueName = venueName;
            ViewBag.District = district;
            ViewBag.Region = region;
            ViewBag.Capacity = Capacity;
            ViewBag.Contact = contact;
            return View();
        }
    }
}
