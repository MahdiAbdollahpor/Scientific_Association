using DataLayer.Context;
using DataLayer.Models.Event;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.AdminViewModels;
using ServiceLayer.ViewModels.BaseViewModels;
using ServiceLayer.ViewModels.UserViewModels;
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

            var query = _db.News.Include(x => x.Images).Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Title.Contains(search) ||
                                       x.Description.Contains(search));
            }

            int totalEntities = query.Count();

            if (totalEntities == 0)
            {
                return new BaseFilterViewModel<NewsViewModel>
                {
                    Entities = new List<NewsViewModel>(),
                    PageIndex = 1,
                    PageCount = 0,
                    StartPage = 1,
                    EndPage = 1
                };
            }

            int take = 3;
            int howManyPageShow = 2;
            var pager = PagingHelper.Pager(pageIndex, totalEntities, take, howManyPageShow);

            var pagedData = query
                .OrderByDescending(x => x.CreateDate)
                .Skip(pager.Skip)
                .Take(pager.Take)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePaths = x.Images.Select(i => i.ImagePath).ToList()
                })
                .ToList();

            return new BaseFilterViewModel<NewsViewModel>
            {
                Entities = pagedData,
                PageIndex = pageIndex,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                EndPage = pager.EndPage
            };

           
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

        public List<NewsViewModel> GetLatestNews(int count)
        {
            return _db.News.Include(n => n.Images)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreateDate)
                .Take(count)
                .Select(x => new NewsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePaths = x.Images.Select(i => i.ImagePath).ToList()

                })
                .ToList();
        }


        // evant sevices


        public BaseFilterViewModel<EventViewModel> GetAllEventsForUser(int pageIndex, string search)
        {

            var query = _db.Events.Include(x => x.Registrations).Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Title.Contains(search) ||
                                       x.Description.Contains(search));
            }

            int totalEntities = query.Count();

            if (totalEntities == 0)
            {
                return new BaseFilterViewModel<EventViewModel>
                {
                    Entities = new List<EventViewModel>(),
                    PageIndex = 1,
                    PageCount = 0,
                    StartPage = 1,
                    EndPage = 1
                };
            }

            int take = 3;
            int howManyPageShow = 2;
            var pager = PagingHelper.Pager(pageIndex, totalEntities, take, howManyPageShow);

            var pagedData = query
                .OrderByDescending(x => x.CreateDate)
                .Skip(pager.Skip)
                .Take(pager.Take)
                .Select(x => new EventViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    RegistrationDeadline = x.RegistrationDeadline,
                    EventStartDate = x.EventStartDate,
                    EventEndDate = x.EventEndDate,
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false)
                })
                .ToList();

            return new BaseFilterViewModel<EventViewModel>
            {
                Entities = pagedData,
                PageIndex = pageIndex,
                PageCount = pager.PageCount,
                StartPage = pager.StartPage,
                EndPage = pager.EndPage
            };

          
        }


        public List<EventViewModel> GetUpcomingEvents(int count)
        {
            return _db.Events
                .Where(x => !x.IsDeleted && x.EventStartDate > DateTime.Now)
                .OrderBy(x => x.EventStartDate)
                .Take(count)
                .Select(x => new EventViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    RegistrationDeadline = x.RegistrationDeadline,
                    EventStartDate = x.EventStartDate,
                    EventEndDate = x.EventEndDate,
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false)
                })
                .ToList();
        }

        public EventDetailsViewModel GetEventDetails(int id)
        {
            return _db.Events
                .Where(x => x.Id == id && !x.IsDeleted)
                .Select(x => new EventDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    RegistrationDeadline = MyDateTime.GetShamsiDateFromGregorian(x.RegistrationDeadline, false),
                    EventStartDate = MyDateTime.GetShamsiDateFromGregorian(x.EventStartDate, false),
                    EventEndDate = x.EventEndDate.HasValue ?
                        MyDateTime.GetShamsiDateFromGregorian(x.EventEndDate.Value, false) : null,
                    CreateDate = MyDateTime.GetShamsiDateFromGregorian(x.CreateDate, false)
                })
                .FirstOrDefault();
        }

        public bool RegisterForEvent(int eventId, string phoneNumber)
        {
            var eventEntity = _db.Events.FirstOrDefault(x => x.Id == eventId && !x.IsDeleted);
            if (eventEntity == null || eventEntity.RegistrationDeadline < DateTime.Now)
            {
                return false;
            }

            var userId = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserId;

            // بررسی اینکه کاربر قبلا ثبت‌نام نکرده باشد
            var existingRegistration = _db.EventRegistrations
                .FirstOrDefault(x => x.EventId == eventId && x.UserId == userId);

            if (existingRegistration != null)
            {
                return false;
            }

            var registration = new EventRegistration
            {
                EventId = eventId,
                UserId = userId,
                RegistrationDate = DateTime.Now,
                IsApproved = false
            };

            _db.EventRegistrations.Add(registration);
            _db.SaveChanges();
            return true;
        }

        public List<UserEventViewModel> GetUserEvents(string phoneNumber)
        {
            var userId = _db.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber).UserId;

            return _db.EventRegistrations
                .Include(x => x.Event)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.RegistrationDate)
                .Select(x => new UserEventViewModel
                {
                    EventId = x.Event.Id,
                    Title = x.Event.Title,
                    ImagePath = x.Event.ImagePath,
                    EventDate = MyDateTime.GetShamsiDateFromGregorian(x.Event.EventStartDate, false) +
                        (x.Event.EventEndDate.HasValue ?
                            " تا " + MyDateTime.GetShamsiDateFromGregorian(x.Event.EventEndDate.Value, false) : ""),
                    RegistrationStatus = x.IsApproved ? "تایید شده" : "در انتظار تایید",
                    RegistrationDate = MyDateTime.GetShamsiDateFromGregorian(x.RegistrationDate, true)
                })
                .ToList();
        }
    }
}
