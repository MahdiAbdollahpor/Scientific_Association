using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class EventEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان همایش الزامی است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیحات همایش الزامی است")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required(ErrorMessage = "تاریخ پایان ثبت‌نام الزامی است")]
        [Display(Name = "تاریخ پایان ثبت‌نام")]
        public DateTime RegistrationDeadline { get; set; }

        [Required(ErrorMessage = "تاریخ شروع همایش الزامی است")]
        [Display(Name = "تاریخ شروع همایش")]
        public DateTime EventStartDate { get; set; }

        [Display(Name = "تاریخ پایان همایش (در صورت چند روزه بودن)")]
        public DateTime? EventEndDate { get; set; }

        [Display(Name = "تصویر جدید همایش")]
        public IFormFile? NewImageFile { get; set; }

        public string? CurrentImagePath { get; set; }
    }
}
