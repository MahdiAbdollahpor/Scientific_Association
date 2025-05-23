using DataLayer.Context;
using DataLayer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.IdentityViewModels;
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
            int take = 10;
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

        public ListUserViewModel GetUserById(int id)
        {
            return _db.Users.Where(x => x.UserId == id)
                .Select(x => new ListUserViewModel
                {
                    firstName = x.firstName,
                    lastName= x.lastName,
                    PhoneNumber = x.PhoneNumber,
                    nationalCode = x.nationalCode,
                    studentNumber = x.studentNumber,
                    IsDeleted = x.IsDeleted,
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                    Id = x.UserId
                }).FirstOrDefault();
        }

        

        //1 => شماره موبایل وجود ندارد
        // -1 => خطای دیتابیس
        public int IsExistPhoneNumber(string PhoneNumber)
        {
            var res = _db.Users.FirstOrDefault(u => u.PhoneNumber == PhoneNumber);
            if (res == null)
            {
                return 1;
            }

            return -1;
        }

        public  bool UpdateUser(ListUserViewModel model)
        {
            var res = _db.Users.FirstOrDefault(u => u.UserId == model.Id);

            if(res !=null)
            {
                res.firstName = model.firstName;
                res.lastName = model.lastName;
                res.PhoneNumber = model.PhoneNumber;
                res.nationalCode = model.nationalCode;
                res.studentNumber = model.studentNumber;
                res.IsDeleted = model.IsDeleted;
                
                _db.Users.Update(res);
                _db.SaveChanges();
                return true;
            }
            return  false;
        }

        
    }
}
