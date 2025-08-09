using ServiceLayer.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.UserViewModels
{
    public class HomePageViewModel
    {
        public List<NewsViewModel> LatestNews { get; set; }
        public List<EventViewModel> UpcomingEvents { get; set; }
    }
}
