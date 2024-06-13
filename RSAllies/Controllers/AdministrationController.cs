using Microsoft.AspNetCore.Mvc;
using RSAllies.Models;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class AdministrationController(ApiClient apiClient) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Create-Admin")]
        public IActionResult CreateAdmin()
        {
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
                return RedirectToAction("Index");
            }

            return View("CreateAdmin", model);
        }

        public async Task<IActionResult> Admins()
        {
            var result = await apiClient.GetAdmins();

            return result.IsSuccess ? View(result.Value) : View(null);
        }

        public async Task<IActionResult> Admin(string id)
        {
            var result = await apiClient.GetAdmin(Guid.Parse(id));

            return View(result.Value ?? null);
        }
    }
}
