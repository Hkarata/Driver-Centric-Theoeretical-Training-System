using Mapster;
using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Models.English;
using RSAllies.Requests;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class AccountController(ApiClient apiClient, SessionService sessionService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult OnBoarding()
		{
            return View();
		}

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Admin(string accessKey)
        {
            // TODO: encrypt the accessKey
            if (accessKey != "admin")
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Register));
            }

            var result1Task = apiClient.CheckNames(model.FirstName, model.MiddleName, model.LastName);
            var result2Task = apiClient.CheckNIDA(model.Identification);

            await Task.WhenAll(result1Task, result2Task);

            var result1 = result1Task.Result;
            var result2 = result2Task.Result;

            if (result1.Value || result2.Value)
            {
                return HandleNameAndNIDAResults(result1.Value, result2.Value, model);
            }

            var request = model.Adapt<CreateUserDto>();

            var result = await apiClient.AddUserData(request);

            if (result.IsSuccess)
            {
                ViewBag.UserId = result.Value;
                return View("Create");
            }

            return View("Register");
        }

        public IActionResult HandleNameAndNIDAResults(bool nameExists, bool nidaExists, object model)
        {
            if (nameExists && nidaExists)
            {
                ModelState.AddModelError("Identification", "NIDA already exists");
                ModelState.AddModelError("FirstName", "First Name already exists");
                ModelState.AddModelError("MiddleName", "Middle Name already exists");
                ModelState.AddModelError("LastName", "Last Name already exists");
            }
            else if (nameExists)
            {
                ModelState.AddModelError("FirstName", "First Name already exists");
                ModelState.AddModelError("MiddleName", "Middle Name already exists");
                ModelState.AddModelError("LastName", "Last Name already exists");
            }
            else if (nidaExists)
            {
                ModelState.AddModelError("Identification", "NIDA already exists");
            }

            return View("Register", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(AccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateAccount", model);
            }

            var result = await apiClient.CheckAccount(model.PhoneNumber, model.Email);

            if (result.Value.EmailExists || result.Value.PhoneNumberExists)
            {
                return HandleAccountCheckResults(result.Value.PhoneNumberExists, result.Value.EmailExists, model);
            }

            var request = new CreateAccountDto
            {
                UserId = model.UserId,
                Phone = model.PhoneNumber,
                Email = model.Email,
                Password = model.Password
            };

            var response = await apiClient.CreateAccount(request);

            return response.IsSuccess ? RedirectToAction("Login") : View("CreateAccount", model);
        }

        public IActionResult HandleAccountCheckResults(bool phoneExists, bool emailExists, object model)
        {
            if (phoneExists && emailExists)
            {
                ModelState.AddModelError("Phone", "Phone Number already exists");
                ModelState.AddModelError("Email", "Email already exists");
            }
            else if (phoneExists)
            {
                ModelState.AddModelError("Phone", "Phone Number already exists");
            }
            else if (emailExists)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            var request = new AuthenticateDto
            {
                Phone = model.Phone,
                Password = model.Password
            };

            var result = await apiClient.AuthenticateUser(request);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Phone", "Invalid Phone Number or Password");

                return View("Login", model);
            }

            await sessionService.SetUserData(result.Value);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(AdminModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Admin");
            }

            var request = model.Adapt<AdminLogin>();

            var result = await apiClient.AdminLogin(request);

            if (result.IsFailure)
            {
                ModelState.AddModelError("Username", "Invalid Username or Password");

                return View("Admin", model);
            }

            await sessionService.SetAdminData(result.Value);

            return RedirectToAction("Index", "Administration");
        }

        public IActionResult Logout()
        {
            sessionService.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogoutAdmin()
        {
            sessionService.ClearAdmin();
            return RedirectToAction("Admin", "Account", new { accessKey = "admin" });
        }
    }
}
