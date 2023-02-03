using Marka_WebUI.EmailServices;
using Marka_WebUI.Identity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
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
            return View(new RegisterModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                //send email
                string siteUrl = "https://localhost:7232";
                string activateUrl = $"{siteUrl}{callbackUrl}";
                string body = $"Merhaba {model.UserName};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUrl}' target='_blank'> tıklayınız</a>.";

                MailHelper.SendEmail(body, model.Email, "Marka User Activition");



                //TempData.Put("message", new ResultMessage()
                //{
                //    Title = "Hesap Onayı",
                //    Message = "Email adresinize gelen link ile hesabınızı onaylayınız",
                //    Css = "warning"
                //});
                return RedirectToAction("login", "account");

            }
            ModelState.AddModelError("", "Kayıt Esnasında Bilinmeyen Bir Hata Oluştu!!");
            return View(model);
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı.");
                return View(model);
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen Hesabınızı email ile aktivasyonunu gerçekleştiriniz.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }
            ModelState.AddModelError("", "Email veya Parola yanlış.");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId==null || token == null)
            {
                TempData["message"] = "Kullanıcı Adı veya Token Geçersiz";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["message"] = "Hoşgeldiniz,Hesabınız Onaylandı.";
                    return View();
                }
            }
            TempData["message"] = "Üzgünüz Hesabınız Onaylanmadı.";
            return View();
        }
    }
}
