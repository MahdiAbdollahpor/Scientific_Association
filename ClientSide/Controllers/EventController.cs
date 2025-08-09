using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Controllers
{
    public class EventController : Controller
    {
        private readonly IUserService _userService;

        public EventController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string search = "")
        {
            var EventsList = _userService.GetAllEventsForUser(pageId, search);
            return View(EventsList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var eventDetails = _userService.GetEventDetails(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            return View(eventDetails);
        }

        [HttpPost]
        public IActionResult Register(int eventId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Identity", new { returnUrl = $"/Event/Details/{eventId}" });
            }

            var result = _userService.RegisterForEvent(eventId, User.Identity.Name);
            if (result)
            {
                TempData["SuccessMessage"] = "ثبت‌نام شما با موفقیت انجام شد";
            }
            else
            {
                TempData["ErrorMessage"] = "خطا در ثبت‌نام یا قبلا ثبت‌نام کرده‌اید";
            }

            return RedirectToAction("Details", new { id = eventId });
        }

        [HttpGet]
        public IActionResult MyEvents()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Identity");
            }

            var myEvents = _userService.GetUserEvents(User.Identity.Name);
            return View(myEvents);
        }
    }
}
