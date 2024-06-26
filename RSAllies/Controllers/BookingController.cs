using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class BookingController(ApiClient apiClient, IHttpContextAccessor httpContextAccessor) : Controller
    {
        public async Task<IActionResult> Index(string userId, string sessionId, string date)
        {
            var request = new CreateBookingDto
            {
                UserId = Guid.Parse(userId),
                SessionId = Guid.Parse(sessionId)
            };

            var result = await apiClient.CreateBooking(request);

            if (result.IsSuccess)
            {
                var cookieBuilder = new CookieBuilder
                {
                    Name = "hasBooked",
                    HttpOnly = false,
                    Expiration = DateTime.Parse(date) - DateTime.Today
                };

                var cookie = cookieBuilder.Build(httpContextAccessor.HttpContext!);

                httpContextAccessor.HttpContext!
                    .Response.Cookies.Append("HasBooked", "true", cookie);

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public IActionResult Analysis()
        {
            return View();
        }
    }
}
