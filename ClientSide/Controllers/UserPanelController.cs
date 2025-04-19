using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ClientSide.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserPanelController : Controller
    {
        private readonly IIdentityService _identityService;

        public UserPanelController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Dashboard()
        {
            var userInfo = _identityService.GetUserInfoForUserPanel(User.Identity.Name);
            return View(userInfo);
        }

        [HttpGet]
        [Route("newPassword")]
        public IActionResult NewPassword()
        {
            ViewBag.userphone = User.Identity.Name;
            return View();
        }

        [HttpPost]
        [Route("newPassword")]
        public IActionResult NewPassword(NewPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool res = _identityService.AddNewPassword(model);
                if (res)
                {
                    TempData["success"] = "رمز شما با موفقیت تغییر یافت";
                    return RedirectToAction("Dashboard", "UserPanel");
                }


            }
            TempData["error"] = "خطا در تغییر کد ";
            return View(model);
        }
    }
}
