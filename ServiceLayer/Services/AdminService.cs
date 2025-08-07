using DataLayer.Context;
using DataLayer.Models.Item;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;

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

        public BaseFilterViewModel<NewsViewModel> GetAllNewsForAdmin(int pageIndex, string search)
        {
            var newsList = _db.News.Include(x => x.Images).Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateDate).ToList();
            int take = 10;
            int howManyPageShow = 2;
            var pager = PagingHelper.Pager(pageIndex, newsList.Count(), take, howManyPageShow);
            if (!string.IsNullOrEmpty(search))
            {
                newsList = newsList.Where(x =>
                    x.Title.Contains(search) ||
                    x.Description.Contains(search)).ToList();
            }
            var result = newsList.Select(x => new NewsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePaths = x.Images.Select(i => i.ImagePath).ToList() // تغییر اینجا
            }).ToList();
            var outPut = PagingHelper.Pagination<NewsViewModel>(result, pager);
            return new BaseFilterViewModel<NewsViewModel>
            {
                EndPage = pager.EndPage,
                Entities = outPut,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                PageIndex = pageIndex
            };
        }

        public NewsViewModel GetNewsById(int id)
        {
            return _db.News.Include(n => n.Images).Where(x => x.Id == id)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePaths = x.Images.Select(i => i.ImagePath).ToList()
                }).FirstOrDefault();
        }

        public bool AddNews(NewsCreateViewModel model)
        {
            try
            {
                var news = new News
                {
                    Title = model.Title,
                    Description = model.Description
                };
                _db.News.Add(news);
                _db.SaveChanges(); // برای گرفتن Id

                // ذخیره تصاویر
                if (model.ImageFiles != null && model.ImageFiles.Count > 0)
                {
                    foreach (var file in model.ImageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var imagePath = SaveImage(file);
                            _db.NewsImages.Add(new NewsImage
                            {
                                NewsId = news.Id,
                                ImagePath = imagePath
                            });
                        }
                    }
                    _db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine("wwwroot", "images", "news");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/news/" + uniqueFileName;
        }

        public NewsEditViewModel GetNewsForEdit(int id)
        {
            return _db.News
                .Include(n => n.Images)
                .Where(n => n.Id == id)
                .Select(n => new NewsEditViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    CurrentImagePaths = n.Images.Select(i => i.ImagePath).ToList()
                }).FirstOrDefault();
        }

        public bool UpdateNews(NewsEditViewModel model)
        {
            var news = _db.News.Include(n => n.Images).FirstOrDefault(n => n.Id == model.Id);
            if (news == null) return false;

            news.Title = model.Title;
            news.Description = model.Description;

            // حذف تصاویر قدیمی اگر لیست جدید داده شده باشد
            if (model.NewImageFiles != null && model.NewImageFiles.Count > 0)
            {
                foreach (var img in news.Images)
                {
                    DeleteImage(img.ImagePath);
                }
                _db.NewsImages.RemoveRange(news.Images);
                _db.SaveChanges();

                // ذخیره تصاویر جدید
                foreach (var file in model.NewImageFiles)
                {
                    if (file.Length > 0)
                    {
                        var imagePath = SaveImage(file);
                        _db.NewsImages.Add(new NewsImage
                        {
                            NewsId = news.Id,
                            ImagePath = imagePath
                        });
                    }
                }
                _db.SaveChanges();
            }

            _db.News.Update(news);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteNews(int id)
        {
            var news = _db.News.Find(id);
            if (news == null) return false;

            news.IsDeleted = true;
            _db.News.Update(news);
            _db.SaveChanges();
            return true;
        }

        private string SaveImage(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "news");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return "/images/news/" + uniqueFileName;
        }

        private void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public NewsDetailsViewModel GetNewsDetails(int id)
        {
            return _db.News
                .Include(n => n.Images)
                .Where(n => n.Id == id)
                .Select(n => new NewsDetailsViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    ImagePaths = n.Images.Select(i => i.ImagePath).ToList(),
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(n.CreateDate, false)
                })
                .FirstOrDefault();
        }

    }
}
