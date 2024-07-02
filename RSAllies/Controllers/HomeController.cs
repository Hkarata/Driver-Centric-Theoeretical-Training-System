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
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = sessionService.GetUserId();

            var result = await apiClient.GetCurrentUserBooking(userId);

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        public IActionResult Privacy()
        {
            if (!sessionService.Check())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public async Task<IActionResult> ConfirmBooking(string bookingId)
        {
            await apiClient.ConfirmBooking(bookingId);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
