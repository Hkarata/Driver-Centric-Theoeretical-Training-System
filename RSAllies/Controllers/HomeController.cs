using Microsoft.AspNetCore.Mvc;
using RSAllies.Models;
using RSAllies.Services;
using System.Diagnostics;

namespace RSAllies.Controllers
{
    public class HomeController(SessionService sessionService, ApiClient apiClient) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var user = sessionService.GetUserData();

            if (user is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var booking = await apiClient.GetCurrentUserBooking(user!.Id);

            return booking.IsSuccess ? View(booking) : View(null);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
