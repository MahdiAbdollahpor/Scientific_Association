using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string RegistrationDeadline { get; set; }
        public string EventStartDate { get; set; }
        public string? EventEndDate { get; set; }
        public List<EventRegistrationViewModel> Registrations { get; set; }
        public string CreateDate { get; set; }
    }
}
