using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IUserService
    {
        BaseFilterViewModel<NewsViewModel> GetAllNewsForUser(int pageIndex, string search);
        NewsDetailsViewModel GetNewsDetails(int id);
        List<NewsViewModel> GetLatestNews(int count);

        // متدهای جدید برای مدیریت همایش‌ها
        BaseFilterViewModel<EventViewModel> GetAllEventsForUser(int pageIndex, string search);
        List<EventViewModel> GetUpcomingEvents(int count);
        EventDetailsViewModel GetEventDetails(int id);
        bool RegisterForEvent(int eventId, string phoneNumber);
        List<UserEventViewModel> GetUserEvents(string phoneNumber);
    }
}
