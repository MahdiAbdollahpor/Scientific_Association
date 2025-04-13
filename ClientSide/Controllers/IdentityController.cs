using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;
using System.Security.Claims;

namespace ClientSide.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

       

        [HttpGet]
        [Route("Register")]
        public IActionResult RegisterByMobile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterByMobile(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.RegisterByPhoneNumber(model);
                if (res == 1)
                {

                    int userId = _identityService.GetUserIdByPhoneNumber(model.PhoneNumber);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,model.PhoneNumber)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);

                    TempData["success"] = "ثبت نام با موفقیت انجام شد";
                    return RedirectToAction("Index", "Home");
                }
                if (res == -100)
                {
                    TempData["error"] = "این شماره موبایل قبلا در سایت ثبت نام کرده است";
                    return View(model);
                }
                
            }
            TempData["error"] = "کاربر گرامی متاسفانه ثبت نام موفقیت آمیز نبود  ";
            return View(model);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult LoginByMobile()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginByMobile(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = _identityService.GetUserStatusForLoginByPhoneNumber(model.studentNumber, model.Password);
                if (res == -100)
                {
                    TempData["error"] = "حساب کاربری وجود ندارد";
                    return View();
                }
                if (res == -50)
                {
                    TempData["error"] = "کاربر گرامی رمز عبور شما اشتبه است";
                    return View(model);
                }
                if (res == -200)
                {
                    TempData["error"] = "حساب کاربری شما فعال می باشد";
                    return View();
                }
                else
                {
                    int userId = _identityService.GetUserIdByStudentNumber(model.studentNumber);
                    
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                        new Claim(ClaimTypes.Name,_identityService.GetPhoneNumberByStudentNumber(model.studentNumber))
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(principal, properties);

                    //if (returnUrl  != null)
                    //{
                    //    TempData["success"] = "ورود با موفقیت انجام شد";
                    //    return RedirectToAction("returnUrl");
                    //}

                    TempData["success"] = "ورود با موفقیت انجام شد";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["error"] = "کاربر گرامی متاسفانه ورود به حساب کاربری موفقیت آمیز نبود  ";
            return View(model);
        }

        [Route("SignOut")]
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["info"] = "کاربر گرامی شما از حساب کاربری خود خارج شدید";
            return RedirectToAction("Index", "Home");
        }

    }
}
