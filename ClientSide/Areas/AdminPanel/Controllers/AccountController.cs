using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Areas.AdminPanel.Controllers
{

    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]  
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
