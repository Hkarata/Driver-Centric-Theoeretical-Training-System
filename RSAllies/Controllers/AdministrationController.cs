using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RSAllies.Analytics;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class AdministrationController(ApiClient apiClient, SessionService sessionService, ApiService apiService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            await SeedGenderCount();
            await SeedAgeGroupCounts();
            await SeedEducationLevelData();
            await SeedPeekBookingDays();
            return View();
        }

        public async Task SeedGenderCount()
        {
            var result = await apiService.GetGenderCount();

            int maleCount = 0;
            int femaleCount = 0;

            if (result.IsSuccess)
            {
                var dataset = result.Value;

                foreach (var item in dataset)
                {
                    if (item.GenderType == "Male")
                    {
                        maleCount = item.Count;
                    }
                    else
                    {
                        femaleCount = item.Count;
                    }

                }
            }

            ViewBag.MaleCount = maleCount;
            ViewBag.FemaleCount = femaleCount;
        }

        public async Task SeedAgeGroupCounts()
        {
            var result = await apiService.GetAgeGroups();

            int[] ageGroupCounts = new int[] { 0, 0, 0, 0, 0, 0 };

            if (result.IsSuccess)
            {
                var dataset = result.Value;

                foreach (var item in dataset)
                {
                    switch (item.AgeGroup)
                    {
                        case "18-25":
                            ageGroupCounts[0] = item.Count;
                            break;
                        case "26-33":
                            ageGroupCounts[1] = item.Count;
                            break;
                        case "34-41":
                            ageGroupCounts[2] = item.Count;
                            break;
                        case "42-49":
                            ageGroupCounts[3] = item.Count;
                            break;
                        case "50-55":
                            ageGroupCounts[4] = item.Count;
                            break;
                        default:
                            ageGroupCounts[5] = item.Count;
                            break;
                    }
                }
            }

            ViewBag.AgeGroupCounts = ageGroupCounts;
        }

        public async Task SeedEducationLevelData()
        {
            var result = await apiService.GetEducationLevelCount();

            int[] educationLevelCounts = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            int[] degreeCounts = new int[] { 0, 0, 0 };

            if(result.IsSuccess)
            {
                var dataset = result.Value;

                foreach (var item in dataset)
                {
                    switch (item.Level)
                    {
                        case "Uneducated":
                            educationLevelCounts[0] = item.Count;
                            break;
                        case "Class 7":
                            educationLevelCounts[1] = item.Count;
                            break;
                        case "Form 2":
                            educationLevelCounts[2] = item.Count;
                            break;
                        case "Form 4":
                            educationLevelCounts[3] = item.Count;
                            break;
                        case "Form 6":
                            educationLevelCounts[4] = item.Count;
                            break;
                        case "Diploma":
                            educationLevelCounts[5] = item.Count;
                            break;
                        default:
                            educationLevelCounts[6] += item.Count;
                            break;
                    }
                }

                foreach (var item in dataset)
                {
                    switch (item.Level)
                    {
                        case "Bachelor's Degree":
                            degreeCounts[0] = item.Count;
                            break;
                        case "Master's Degree":
                            degreeCounts[1] = item.Count;
                            break;
                        default:
                            degreeCounts[2] = item.Count;
                            break;
                    }
                }
            }

            ViewBag.EducationLevelCounts = educationLevelCounts;
            ViewBag.DegreeCounts = degreeCounts;
        }

        public async Task SeedPeekBookingDays()
        {
            var result = await apiService.GetPeekBookingDays();

            int[] bookingCounts = new int[] { 0, 0, 0, 0, 0, 0, 0 };

            if (result.IsSuccess)
            {
                var dataset = result.Value;

                foreach (var item in dataset)
                {
                    switch (item.Day.ToLower())
                    {
                        case "monday":
                            bookingCounts[0] = item.Bookings;
                            break;
                        case "tuesday":
                            bookingCounts[1] = item.Bookings;
                            break;
                        case "wednesday":
                            bookingCounts[2] = item.Bookings;
                            break;
                        case "thursday":
                            bookingCounts[3] = item.Bookings;
                            break;
                        case "friday":
                            bookingCounts[4] = item.Bookings;
                            break;
                        case "saturday":
                            bookingCounts[5] = item.Bookings;
                            break;
                        default:
                            bookingCounts[6] = item.Bookings;
                            break;
                    }
                }
            }

            ViewBag.BookingCounts = JsonConvert.SerializeObject(bookingCounts);

        }

        [ActionName("Create-Admin")]
        public IActionResult CreateAdmin()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            return View("CreateAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdmin model)
        {
            if (ModelState.IsValid)
            {
                return View("CreateAdmin", model);
            }

            var admin = new Contracts.Requests.AdminDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = $"{model.FirstName}.{model.LastName}",
                Phone = model.Phone,
                Email = model.Email,
                Password = model.Password,
                RoleId = model.RoleId
            };

            var result = await apiClient.CreateAdmin(admin);

            if (result.IsSuccess)
            {
                return RedirectToAction("Admins");
            }

            return View("CreateAdmin", model);
        }

        public async Task<IActionResult> Admins()
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            await SeedCaseData();

            var result = await apiClient.GetAdmins();

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        public async Task<IActionResult> Admin(string id)
        {
            if (!sessionService.CheckAdmin())
            {
                return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
            }

            var result = await apiClient.GetAdmin(Guid.Parse(id));

            return View(result.Value ?? null);
        }

        public async Task SeedCaseData()
        {
            var result = await apiService.GetCaseAnalysis();

            int closedCases = 0;
            int openCases = 0;

            if (result.IsSuccess)
            {
                closedCases = result.Value.ClosedCases;
                openCases = result.Value.OpenCases;
            }

            ViewBag.ClosedCases = closedCases;
            ViewBag.OpenCases = openCases;
        }
    }
}
