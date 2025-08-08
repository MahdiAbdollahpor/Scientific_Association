using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;

namespace ClientSide.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [PermissionChecker(1)]
    public class EventController : Controller
    {
        private readonly IAdminService _adminService;

        public EventController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var eventList = _adminService.GetAllEventsForAdmin(pageId, search);
            return View(eventList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _adminService.AddEvent(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "همایش با موفقیت اضافه شد";
                    return RedirectToAction("Index");
                }
            }

            TempData["ErrorMessage"] = "خطا در افزودن همایش";
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _adminService.GetEventForEdit(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _adminService.UpdateEvent(model);

            if (result)
            {
                TempData["SuccessMessage"] = "همایش با موفقیت ویرایش شد";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "خطا در ویرایش همایش";
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteEvent(int id)
        {
            var result = _adminService.DeleteEvent(id);
            return Json(new
            {
                success = result,
                message = result ? "همایش با موفقیت حذف شد" : "خطا در حذف همایش"
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var eventDetails = _adminService.GetEventDetails(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            return View(eventDetails);
        }

        [HttpPost]
        public IActionResult ApproveRegistration(int registrationId)
        {
            var result = _adminService.ApproveRegistration(registrationId);
            return Json(new
            {
                success = result,
                message = result ? "ثبت‌نام با موفقیت تایید شد" : "خطا در تایید ثبت‌نام"
            });
        }

        [HttpPost]
        public IActionResult RejectRegistration(int registrationId)
        {
            var result = _adminService.RejectRegistration(registrationId);
            return Json(new
            {
                success = result,
                message = result ? "ثبت‌نام با موفقیت رد شد" : "خطا در رد ثبت‌نام"
            });
        }

        [HttpGet]
        public IActionResult GetRegistrations(int eventId, int pageId = 1)
        {
            var registrations = _adminService.GetEventRegistrations(eventId, pageId);
            return PartialView("_EventRegistrationsPartial", registrations);
        }
    }
}
