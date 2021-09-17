using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;
using WorkersOrder.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WorkersOrder.Service;

namespace WorkersOrder.Controllers
{
    public class AccountController : Controller
    {
        private Context db;
        private Service.Service service;
        public AccountController(Context context)
        {
            this.db = context;
            this.service = new Service.Service(context);
        }

        [HttpGet]
        public IActionResult Login(string Sign)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string ss)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await service.Find(null, model);
                if (employee != null)
                {
                    await Authenticate(model.Login);
                    return RedirectToAction("Menus", "Home");
                }
                ModelState.AddModelError("","Invalid Login or Password");
            }
            else if ((model.Login == null || model.Password == null) && ss != null)
                ModelState.AddModelError("", "Invalid Login or Password");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string ss)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await service.Find(model, null);
                if (employee == null)
                {
                    service.AddToDBEmployee(model.Surname, model.Name, model.Login, model.Password, model.Role);
                    await service.Save();
                    await Authenticate(model.Login);
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("", "Invalid Login or Password");
            }
            else if((model.Surname==null ||model.Name==null || model.Login == null||model.Password == null|| model.Role == null) && ss!=null)
                ModelState.AddModelError("", "You didn't fill out everything");
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Autorization", "Home");
        }

    }
}
