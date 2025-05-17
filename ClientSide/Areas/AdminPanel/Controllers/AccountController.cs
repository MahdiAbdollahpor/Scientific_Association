using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;

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
        [HttpGet]
        public  IActionResult EditUser(int id)
        {

            

            var user =  _adminService.GetUserById(id);
            if (user == null) return NotFound();

            return PartialView("_EditUserModal", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult EditUser(ListUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "اطلاعات وارد شده معتبر نیست"
                });
            }

            var result = _adminService.UpdateUser(model);

            return Json(new
            {
                success = result,
                message = result ? "اطلاعات کاربر با موفقیت ویرایش شد" : "خطا در ویرایش کاربر"
            });
        }
    }
}
