using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.UserViewModels
{
    public class UserEventViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string EventDate { get; set; }
        public string RegistrationStatus { get; set; }
        public string RegistrationDate { get; set; }
    }
}
