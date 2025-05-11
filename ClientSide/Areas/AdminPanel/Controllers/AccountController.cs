using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Areas.AdminPanel.Controllers
{

    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]  
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;

        public AccountController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var userList = _adminService.GetAllUserForAdmin(pageId, search);
            return View(userList);
        }
    }
}
