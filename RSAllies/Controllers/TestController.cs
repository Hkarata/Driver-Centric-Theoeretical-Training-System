using Microsoft.AspNetCore.Mvc;

namespace RSAllies.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manage()
        {
            return View();
        }
    }
}
