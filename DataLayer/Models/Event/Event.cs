using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Event
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime RegistrationDeadline { get; set; }

        [Required]
        public DateTime EventStartDate { get; set; }

        public DateTime? EventEndDate { get; set; } // برای همایش‌های چند روزه

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;

        public string ImagePath { get; set; }

        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
    }
}
