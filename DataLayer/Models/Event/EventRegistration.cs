using DataLayer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Event
{
    public class EventRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;

        // روابط
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
