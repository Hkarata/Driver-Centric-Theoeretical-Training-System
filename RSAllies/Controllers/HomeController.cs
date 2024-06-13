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

            var result = await apiClient.GetCurrentUserBooking(user.Id);

            return result.IsSuccess ? View(result.Value) : View(null);
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
