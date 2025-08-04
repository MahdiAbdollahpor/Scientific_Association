using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Controllers
{
    public class NewsController : Controller
    {
        private readonly IUserService _userService;

        public NewsController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult NewsList(int pageId = 1, string search = "")
        {
            var newsList = _userService.GetAllNewsForUser(pageId, search);
            return View(newsList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var news = _userService.GetNewsDetails(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }
    }
}
