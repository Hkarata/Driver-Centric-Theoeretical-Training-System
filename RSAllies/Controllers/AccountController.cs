﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using RSAllies.Contracts.Requests;
using RSAllies.Models;
using RSAllies.Models.English;
using RSAllies.Services;

namespace RSAllies.Controllers
{
    public class AccountController(ApiClient apiClient) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var result1Task = apiClient.CheckNames(model.FirstName, model.MiddleName, model.LastName);
            var result2Task = apiClient.CheckNIDA(model.Identification);

            await Task.WhenAll(result1Task, result2Task);

            var result1 = result1Task.Result;
            var result2 = result2Task.Result;

            if (result1.Value || result2.Value)
            {
                return HandleNameAndNIDAResults(result1.Value, result2.Value);
            }

            var request = model.Adapt<CreateUserDto>();

            var result = await apiClient.AddUserData(request);

            if (result.IsSuccess)
            {
                ViewBag.UserId = result.Value;
                return RedirectToAction("Create");
            }

            return View();
        }

        public IActionResult HandleNameAndNIDAResults(bool nameExists, bool nidaExists)
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount(AccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }

            var result = await apiClient.CheckAccount(model.PhoneNumber, model.Email);

            if (result.Value.EmailExists || result.Value.PhoneNumberExists)
            {
                return HandleAccountCheckResults(result.Value.PhoneNumberExists, result.Value.EmailExists);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult HandleAccountCheckResults(bool phoneExists, bool emailExists)
        {
            if (phoneExists && emailExists)
            {
                ModelState.AddModelError("PhoneNumber", "Phone Number already exists");
                ModelState.AddModelError("Email", "Email already exists");
            }
            else if (phoneExists)
            {
                ModelState.AddModelError("PhoneNumber", "Phone Number already exists");
            }
            else if (emailExists)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            return RedirectToAction("Create");
        }
    }
}
