using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSAllies.Analytics;
using RSAllies.Contracts.Requests;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class BookingController(ApiClient apiClient,
        ApiService apiService,
        IHttpContextAccessor httpContextAccessor) : Controller
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
                    SameSite = SameSiteMode.Lax,
                    Expiration = DateTime.Parse(date) - DateTime.Today
                };

                var cookie = cookieBuilder.Build(httpContextAccessor.HttpContext!);

                httpContextAccessor.HttpContext!
                    .Response.Cookies.Append("HasBooked", "true", cookie);

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public async Task<IActionResult> Analysis()
        {
            await SeedBookingRate();
            await SeedRates();
            await SeedTimes();
            await SeedMonths();
            await SeedYears();
            await SeedVenueStatusCounts();

            var repeatedBookings = await apiService.GetRepeatedBookingsCount();

            if (repeatedBookings.IsSuccess)
            {
                ViewBag.RepeatedBookings = repeatedBookings.Value;
            }
            else
            {
                ViewBag.RepeatedBookings = 0;
            }

            return View();
        }

        public async Task SeedBookingRate()
        {
            var x = await apiService.GetBookingRate();

            var venues = x.Value.Select(v => v.VenueName).ToArray();
            var numbers = x.Value.Select(v => v.BookingRate);

            var a = JsonConvert.SerializeObject(venues);
            var b = JsonConvert.SerializeObject(numbers);

            ViewBag.Venues = a;
            ViewBag.VenueNumbers = b;
        }

        public async Task SeedRates()
        {
            var confirmed = await apiService.GetConfirmationRates();
            var confirmedVenues = confirmed.Value.Select(v => v.VenueName).ToArray();
            var confirmedNumbers = confirmed.Value.Select(v => v.ConfirmationRate);

            var a = JsonConvert.SerializeObject(confirmedVenues);
            var b = JsonConvert.SerializeObject(confirmedNumbers);

            ViewBag.ConfirmedVenues = a;
            ViewBag.ConfirmedNumbers = b;

            var cancelled = await apiService.GetCancellationRates();

            var cancelledNumbers = cancelled.Value.Select(v => v.CancellationRate);

            var d = JsonConvert.SerializeObject(cancelledNumbers);

            ViewBag.CancelledNumbers = d;
        }

        public async Task SeedTimes()
        {
            var data = await apiService.GetPeakBookingTimes();

            var amGroup = data.Value.Where(b => b.BookingHour >= 0 && b.BookingHour < 12);
            var pmGroup = data.Value.Where(b => b.BookingHour >= 12 && b.BookingHour < 24);

            List<List<object>> amData = new List<List<object>>();
            List<List<object>> pmData = new List<List<object>>();

            foreach (var item in amGroup)
            {
                amData.Add(new List<object> { item.BookingHour.ToString(), item.BookingCount });
            }

            foreach (var item in pmGroup)
            {
                pmData.Add(new List<object> { item.BookingHour.ToString(), item.BookingCount });
            }

            var am = JsonConvert.SerializeObject(amData);
            var pm = JsonConvert.SerializeObject(pmData);

            ViewBag.AmData = am;
            ViewBag.PmData = pm;

        }

        public async Task SeedMonths()
        {
            var response = await apiService.GetPeekBookingMonths();

            var data = response.Value.OrderBy(b => b.Month);

            int[] monthlyBookings = new int[12];

            foreach (var dataset in data)
            {
                // Map month names to array indices
                switch (dataset.Month.ToLower())
                {
                    case "january":
                        monthlyBookings[0] = dataset.Bookings;
                        break;
                    case "february":
                        monthlyBookings[1] = dataset.Bookings;
                        break;
                    case "march":
                        monthlyBookings[2] = dataset.Bookings;
                        break;
                    case "april":
                        monthlyBookings[3] = dataset.Bookings;
                        break;
                    case "may":
                        monthlyBookings[4] = dataset.Bookings;
                        break;
                    case "june":
                        monthlyBookings[5] = dataset.Bookings;
                        break;
                    case "july":
                        monthlyBookings[6] = dataset.Bookings;
                        break;
                    case "august":
                        monthlyBookings[7] = dataset.Bookings;
                        break;
                    case "september":
                        monthlyBookings[8] = dataset.Bookings;
                        break;
                    case "october":
                        monthlyBookings[9] = dataset.Bookings;
                        break;
                    case "november":
                        monthlyBookings[10] = dataset.Bookings;
                        break;
                    case "december":
                        monthlyBookings[11] = dataset.Bookings;
                        break;
                    default:
                        // Handle unknown month names (optional)
                        break;
                }
            }

            var b = JsonConvert.SerializeObject(monthlyBookings);

            ViewBag.MonthlyBookings = b;
        }

        public async Task SeedYears()
        {
            var response = await apiService.PeakBookingYears();

            var data = response.Value.OrderBy(b => b.Year);

            var years = data.Select(b => b.Year).ToArray();
            var bookings = data.Select(b => b.Bookings).ToArray();

            var a = JsonConvert.SerializeObject(years);
            var b = JsonConvert.SerializeObject(bookings);

            ViewBag.Years = a;
            ViewBag.Bookings = b;

        }

        public async Task SeedVenueStatusCounts()
        {
            var response = await apiService.GetVenueBookingStatusCounts();

            var data = response.Value.OrderBy(v => v.VenueName);

            var venues = data.Select(b => b.VenueName).ToArray();
            var booked = data.Select(b => b.Booked).ToArray();
            var paid = data.Select(b => b.Paid).ToArray();
            var confirmed = data.Select(b => b.Confirmed).ToArray();
            var cancelled = data.Select(b => b.Cancelled).ToArray();

            var a = JsonConvert.SerializeObject(venues);
            var b = JsonConvert.SerializeObject(booked);
            var c = JsonConvert.SerializeObject(paid);
            var d = JsonConvert.SerializeObject(confirmed);
            var e = JsonConvert.SerializeObject(cancelled);

            ViewBag.VenuesList = a;
            ViewBag.Booked = b;
            ViewBag.Paid = c;
            ViewBag.Confirmed = d;
            ViewBag.Cancelled = e;
        }
    }
}
