using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;

namespace ClientSide.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]
    public class NewsController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _env;

        public NewsController(IAdminService adminService, IWebHostEnvironment env)
        {
            _adminService = adminService;
            _env = env;
        }

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var newsList = _adminService.GetAllNewsForAdmin(pageId, search);
            return View(newsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =  _adminService.AddNews(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "خبر با موفقیت اضافه شد";
                    return RedirectToAction("Index");
                }
            }

            TempData["ErrorMessage"] = "خطا در افزودن خبر";
            return View(model);
        }

        [HttpGet]
        public IActionResult EditNews(int id)
        {
            var news = _adminService.GetNewsById(id);
            if (news == null) return NotFound();

            return PartialView("_EditNewsModal", news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNews(NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });
            }

            var result = _adminService.UpdateNews(model);
            return Json(new
            {
                success = result,
                message = result ? "خبر با موفقیت ویرایش شد" : "خطا در ویرایش خبر"
            });
        }

        [HttpPost]
        public IActionResult DeleteNews(int id)
        {
            var result = _adminService.DeleteNews(id);
            return Json(new
            {
                success = result,
                message = result ? "خبر با موفقیت حذف شد" : "خطا در حذف خبر"
            });
        }
    }
}
