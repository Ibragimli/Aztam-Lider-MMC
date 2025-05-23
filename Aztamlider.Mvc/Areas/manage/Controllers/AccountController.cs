﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Aztamlider.Core.Entites;
using Aztamlider.Services.CustomExceptions;
using System.Data;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area.Login;
using Microsoft.EntityFrameworkCore;
using Aztamlider.Services.HelperService.Interfaces;

namespace Aztamlider.Mvc.Areas.manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly ILoggerServices _loggerServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAdminLoginServices _adminLoginServices;

        public AccountController(ILoggerServices loggerServices, UserManager<AppUser> userManager, IAdminLoginServices adminLoginServices)
        {
            _loggerServices = loggerServices;
            _userManager = userManager;
            _adminLoginServices = adminLoginServices;
        }
        public async Task<IActionResult> Login()
        {
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (user != null && user.IsAdmin == true)
            {
                return RedirectToAction("index", "dashboard");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginPostDto adminLoginPostDto)
        {

            try
            {
                await _adminLoginServices.Login(adminLoginPostDto);

                //Logger
                AppUser user = await _userManager.FindByNameAsync(adminLoginPostDto.Username);

                await _loggerServices.LoggerCreate("Account-Login", "Hesaba giriş edildi", adminLoginPostDto.Username, user.RoleName, adminLoginPostDto.Username);
            }
            catch (ItemNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["Error"] = (ex.Message);
                return View();
            }
            catch (UserLoginAttempCountException ex)
            {
                TempData["Error"] = (ex.Message);

                return View();
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        [Authorize(Roles = "SuperAdmin,Admin,Editor,Viewer")]
        public IActionResult Logout()
        {
            _adminLoginServices.Logout();
            return RedirectToAction("login", "account");
        }


        //public async Task<IActionResult> CreateRole()
        //{
        //    //var role1 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    //var role2 = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    //var role3 = await _roleManager.CreateAsync(new IdentityRole("Member"));
        //    //var role4 = await _roleManager.CreateAsync(new IdentityRole("Editor"));
        //    //var role5 = await _roleManager.CreateAsync(new IdentityRole("Viewer"));

        //    AppUser SuperAdmin = new AppUser { FullName = "Name", UserName = "Username" };
        //    var admin = await _userManager.CreateAsync(SuperAdmin, "password");
        //    var resultRole = await _userManager.AddToRoleAsync(SuperAdmin, "Admin");
        //    return Ok(resultRole);
        //}

    }
}
