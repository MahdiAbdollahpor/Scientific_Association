using DataLayer.Context;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _db;

        public AdminService(ApplicationDbContext db)
        {
            _db = db;
        }


        public BaseFilterViewModel<ListUserViewModel> GetAllUserForAdmin(int pageIndex, string search)
        {
            var userList = _db.Users.Where(x => x.IsDeleted == false).OrderByDescending(x => x.RegisterTime).ToList();
            int take = 15;
            int howManyPageShow = 2;
            var pager = PagingHelper.Pager(pageIndex, userList.Count(), take, howManyPageShow);

            if (search != null)
            {
                userList = userList.Where(x => x.PhoneNumber.Contains(search) || x.firstName.Contains(search) || x.lastName.Contains(search) || x.nationalCode.Contains(search) || x.studentNumber.Contains(search)).ToList();
            }

            var resault = userList.Select(x => new ListUserViewModel
            {
                CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                firstName = x.firstName,
                lastName = x.lastName,
                PhoneNumber = x.PhoneNumber,
                nationalCode = x.nationalCode,
                studentNumber = x.studentNumber,
                IsDeleted = x.IsDeleted,
                Id = x.UserId
            }).ToList();

            var outPut = PagingHelper.Pagination<ListUserViewModel>(resault, pager);

            BaseFilterViewModel<ListUserViewModel> res = new BaseFilterViewModel<ListUserViewModel>
            {
                EndPage = pager.EndPage,
                Entities = outPut,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                PageIndex = pageIndex
            };

            return res;
        }
    }
}
