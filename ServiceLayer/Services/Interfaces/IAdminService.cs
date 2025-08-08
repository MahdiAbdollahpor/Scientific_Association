using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IAdminService
    {
        BaseFilterViewModel<ListUserViewModel> GetAllUserForAdmin(int pageIndex, string search);
        ListUserViewModel GetUserById(int id);
        int IsExistPhoneNumber(string PhoneNumber);
        bool UpdateUser(ListUserViewModel model);
        BaseFilterViewModel<NewsViewModel> GetAllNewsForAdmin(int pageIndex, string search);
        NewsViewModel GetNewsById(int id);
        bool AddNews(NewsCreateViewModel model);
        NewsEditViewModel GetNewsForEdit(int id);
        bool UpdateNews(NewsEditViewModel model);
        bool DeleteNews(int id);
        NewsDetailsViewModel GetNewsDetails(int id);


        // evant service

        BaseFilterViewModel<EventViewModel> GetAllEventsForAdmin(int pageIndex, string search);
        EventViewModel GetEventById(int id);
        bool AddEvent(EventCreateViewModel model);
        EventEditViewModel GetEventForEdit(int id);
        bool UpdateEvent(EventEditViewModel model);
        bool DeleteEvent(int id);
        EventDetailsViewModel GetEventDetails(int id);
        bool ApproveRegistration(int registrationId);
        bool RejectRegistration(int registrationId);
        BaseFilterViewModel<EventRegistrationViewModel> GetEventRegistrations(int eventId, int pageIndex);
    }
}
