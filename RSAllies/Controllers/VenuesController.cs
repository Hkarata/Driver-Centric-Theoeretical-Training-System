using Microsoft.AspNetCore.Mvc;

namespace RSAllies.Controllers
{
    public class VenuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddVenue()
        {
            return View("Add");
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
