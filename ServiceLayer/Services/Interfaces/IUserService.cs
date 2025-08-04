using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
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
    }
}
