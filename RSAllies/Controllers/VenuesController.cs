using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class VenuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var x = new SelectList(FileService.GetDistricts(), "Id", "Name");
            var y = new SelectList(FileService.GetRegions(), "Id", "Name");

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
