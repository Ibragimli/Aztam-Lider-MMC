﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aztamlider.Core.Entites;
using Aztamlider.Core.IUnitOfWork;
using Aztamlider.Services.CustomExceptions;
using Aztamlider.Services.Dtos.Area;
using Aztamlider.Services.Services.Interfaces.Area.Login;
using Microsoft.EntityFrameworkCore;
using Aztamlider.Data.Datacontext;
using Microsoft.Build.Framework;
using Aztamlider.Services.HelperService.Interfaces;

namespace Aztamlider.Services.Services.Implementations.Area.Login
{
    public class AdminLoginServices : IAdminLoginServices
    {
        private readonly DataContext _context;
        private readonly ILoggerServices _loggerServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminLoginServices(DataContext context, ILoggerServices loggerServices, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _loggerServices = loggerServices;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Login(AdminLoginPostDto adminLoginPostDto)
        {
            CheckValues(adminLoginPostDto);
            AppUser adminExist = await _unitOfWork.AppUserRepository.GetAsync(x => x.UserName == adminLoginPostDto.Username);

            if (adminExist != null && adminExist.IsAdmin == true && adminExist.LoginAttemptCount != 0)
            {
                var logger = await _context.Loggers.Where(x => x.Username == adminExist.UserName).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                var result = await _signInManager.PasswordSignInAsync(adminExist, adminLoginPostDto.Password, false, false);

                if (!result.Succeeded)
                {
                    adminExist.LoginAttemptCount -= 1;
                    if (adminExist.LoginAttemptCount == 0)
                    {
                        await _unitOfWork.CommitAsync();
                        throw new UserLoginAttempCountException("Hesab bloklandı!");
                    }
                    await _loggerServices.LoggerCreate("Account", "Login", adminExist.FullName, adminExist.UserName, "Düzgün daxil edilməyən şifrə", adminExist.UserName);
                    await _unitOfWork.CommitAsync();
                    throw new UserLoginAttempCountException("Mümkün təkrar cəhd sayı! - " + adminExist.LoginAttemptCount);
                }
                if (logger != null && adminExist.LoginAttemptCount < 5 && (DateTime.UtcNow.AddHours(4) - logger.CreatedDate).Days > 0)
                {
                    adminExist.LoginAttemptCount = 5;
                    await _unitOfWork.CommitAsync();
                }
                return true;
            }
            if (adminExist?.LoginAttemptCount == 0)
                throw new UserLoginAttempCountException("Hesab bloklandı!");

            throw new UserNotFoundException("Username və ya Password yanlışdır!");
        }
        private void CheckValues(AdminLoginPostDto adminLoginPostDto)
        {
            if (adminLoginPostDto.Username == null)
                throw new ItemNullException("Username-i daxil edin");
            if (adminLoginPostDto.Password == null)
                throw new ItemNullException("Password-u daxil edin");
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
