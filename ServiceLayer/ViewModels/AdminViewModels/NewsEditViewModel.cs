using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class NewsEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان خبر الزامی است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "متن خبر الزامی است")]
        [Display(Name = "محتوا")]
        public string Description { get; set; }

        // لیست تصاویر فعلی
        public List<string> CurrentImagePaths { get; set; } = new List<string>();

        [Display(Name = "افزودن تصاویر جدید")]
        public List<IFormFile>? NewImageFiles { get; set; }
    }
}
