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
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }
        public BaseFilterViewModel<NewsViewModel> GetAllNewsForUser(int pageIndex, string search)
        {
            var newsList = _db.News.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreateDate).ToList();

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
                ImagePath = x.ImagePath
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

        public NewsDetailsViewModel GetNewsDetails(int id)
        {
            return _db.News
                .Where(n => n.Id == id)
                .Select(n => new NewsDetailsViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    ImagePath = n.ImagePath,
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(n.CreateDate, false)
                })
                .FirstOrDefault();
        }

        public List<NewsViewModel> GetLatestNews(int count)
        {
            return _db.News
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreateDate)
                .Take(count)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    
                })
                .ToList();
        }
    }
}
