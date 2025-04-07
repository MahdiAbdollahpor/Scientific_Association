using DataLayer.Context;
using DataLayer.Models.Identity;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _db;

        public IdentityService(ApplicationDbContext db)
        {
            _db = db;
        }




        //-1 => ثبت نام با شکست مواجه شد
        // 1 => ثبت نام با موفقیت انجام شد
        // -100 => کاربر قبلا ثبت نام کرده
        // -50 => بعد از 3 دقیقه باید تلاش کنبد
        public int RegisterByPhoneNumber(RegisterViewModel model)
        {
            int StatusPhoneNumber = IsExistPhoneNumber(model.PhoneNumber);
            if (StatusPhoneNumber == 1)
            {
                User user = new User
                {
                    studentNumber = model.studentNumber,
                    nationalCode = model.nationalCode,
                    lastName = model.lastName,
                    firstName = model.firstName,
                    IsDeleted = false,
                    Password = PasswordHelper.EncodePasswordMd5(model.Password),
                    PhoneNumber = model.PhoneNumber,
                };

               

                _db.Users.Add(user);
                _db.SaveChanges();
                return 1;

            }
            if (StatusPhoneNumber == -100)
            {
                return -100;
            }
           

            return -1;
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
    }
}
