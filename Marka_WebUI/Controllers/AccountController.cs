using Marka_BLL.Abstract;
using Marka_WebUI.EmailServices;
using Marka_WebUI.Extensions;
using Marka_WebUI.Identity;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;

namespace Marka_WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private ICartService _cartService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartService = cartService;
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

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Email adresinize gelen link ile hesabınızı onaylayınız",
                    Css = "warning"
                });
                return RedirectToAction("login", "account");

            }
            ModelState.AddModelError("", "Kayıt Esnasında Bilinmeyen Bir Hata Oluştu!!");
            return View(model);
        }
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {            
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
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("", "Email veya Parola yanlış.");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new ResultMessage()
            {
                Title = "Oturum Kapatıldı",
                Message = "Hesabınızdan Güvenli Bir Şekilde Çıkış Yapılmıştır.",
                Css = "warning"
            });
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Hesap Onayı İçin Bilgileriniz Yanlıştır.",
                    Css = "danger"
                });
                return Redirect("~/");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    _cartService.InitializeCart(user.Id);
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hesap Onayı",
                        Message = $"Hoşgeldiniz Sayın {user.FullName}, Hesabınız Onaylanmıştır.",
                        Css = "success"
                    });
                    return RedirectToAction("Login","Account");
                }
            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hesap Onayı",
                Message = "Üzgünüz Hesabınızın Aktivasyonu Gerçekleştirilemedi.",
                Css = "danger"
            });
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Bilgileriniz Hatalıdır.",
                    Css = "danger"
                });
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Belirtmiş Olduğunuz Email Adresine Tanımlı Kullanıcı Bulunamadı.",
                    Css = "danger"
                });
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = code
            });
            string siteUrl = "https://localhost:7232";
            string body = $"Merhabalar, Parolanızı Yenilemek için  <a href='{siteUrl}{callbackUrl}'> tıklayınız</a>.";

            MailHelper.SendEmail(body, email, "Marka Password Reset");
            TempData.Put("message", new ResultMessage()
            {
                Title = "Forgot Password",
                Message = "Parola Yenilemek İçin Hesabınıza Email Gönderilmiştir.",
                Css = "warning"
            });
            return RedirectToAction("login","account");
        }
        public IActionResult ResetPassword(string token)
        {
            if (token!=null)
            {
                var model = new ResetPasswordModel() { Token = token };
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user!=null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        TempData.Put("message", new ResultMessage()
                        {
                            Title = "Forgot Password",
                            Message = "Parolanız Başarı İle Yenilenmiştir.",
                            Css = "success"
                        });
                        return RedirectToAction("Login", "Account");
                    }
                    return View(model);
                }
                ModelState.AddModelError("", "Kullanıcı Bulunamadı.");
                return View(model);
            }  
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
 