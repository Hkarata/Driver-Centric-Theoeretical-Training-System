using Microsoft.AspNetCore.Mvc;

namespace RSAllies.Controllers
{
    public class VenueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
