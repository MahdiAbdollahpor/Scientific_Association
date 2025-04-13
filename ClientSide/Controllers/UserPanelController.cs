using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

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
    }
}
