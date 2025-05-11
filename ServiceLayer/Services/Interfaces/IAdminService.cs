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
    }
}
