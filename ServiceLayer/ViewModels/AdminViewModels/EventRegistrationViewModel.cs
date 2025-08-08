using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class EventRegistrationViewModel
    {
        public int EventId { get; set; }
        public int RegistrationId { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserStudentNumber { get; set; }
        public string RegistrationDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
