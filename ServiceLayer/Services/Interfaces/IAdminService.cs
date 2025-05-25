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
        bool UpdateNews(NewsViewModel model);
        bool DeleteNews(int id);
    }
}
