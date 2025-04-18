using DataLayer.Context;
using DataLayer.Models.Identity;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;


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

        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            var res = _db.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

            return res.UserId;

        }

        public string GetDisplayNameByPhoneNumber(string phone)
        {
            return _db.Users.FirstOrDefault(x => x.PhoneNumber == phone).firstName;
        }

        //-100 => کاربر وجود ندارد
        //-50 => پسورد اشتباه است
        // -200 =>کاربر حذف شده است
        public int GetUserStatusForLoginByPhoneNumber(string studentNumber, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.studentNumber == studentNumber);
            if (user == null)
            {
                return -100;
            }
            if (user.Password != PasswordHelper.EncodePasswordMd5(password))
            {
                return -50;
            }
            if (user.IsDeleted == true)
            {
                return -200;
            }

            return -1;
        }

        public int GetUserIdByStudentNumber(string studentNumber)
        {
            var res = _db.Users.FirstOrDefault(u => u.studentNumber == studentNumber);

            return res.UserId;

        }
        public string GetPhoneNumberByStudentNumber(string studentNumber)
        {
            var res = _db.Users.FirstOrDefault(u => u.studentNumber == studentNumber);

            return res.PhoneNumber;

        }

        public bool CheckPermission(int permissionId, string phoneNumber)
        {
            int userId = _db.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber).UserId;

            List<int> roleIds = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();

            bool flag = false;

            if (!roleIds.Any())
            {
                flag = false;
            }
            else
            {
                foreach (int roleId in roleIds)
                {
                    foreach (var _rolePermission in _db.RolePermissions.Where(x => x.RoleId == roleId).ToList())
                    {
                        if (_rolePermission.PermissionId == permissionId)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;
        }

        public UserInfoForUserPanelViewModel GetUserInfoForUserPanel(string phoneNumber)
        {
            return _db.Users.Where(x => x.PhoneNumber == phoneNumber).Select(x => new UserInfoForUserPanelViewModel
            {
                firstName = x.firstName,
                lastName = x.lastName,
                nationalCode = x.nationalCode,
                studentNumber = x.studentNumber,
                PhoneNumber = x.PhoneNumber,
                RegisterTime = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                UserId = x.UserId
            }).FirstOrDefault();
        }

        public UserPanelSidebarViewModels GetUserPanelSidebar(string phoneNumber)
        {

            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            UserPanelSidebarViewModels userPanelSidebarViewModels = new UserPanelSidebarViewModels
            {
                firstName = user.firstName,
                lastName = user.lastName

            };
            return userPanelSidebarViewModels;


        }
    }
}
