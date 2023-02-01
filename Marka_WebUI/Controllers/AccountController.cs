﻿using Marka_WebUI.Identity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user=new ApplicationUser()
            {
                FullName=model.FullName,
                Email=model.Email,
                UserName=model.UserName,
            }
            return View();
        }
    }
}
