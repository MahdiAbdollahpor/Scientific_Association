using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class NewsCreateViewModel
    {
        [Required(ErrorMessage = "عنوان خبر الزامی است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "متن خبر الزامی است")]
        [Display(Name = "متن خبر")]
        public string Description { get; set; }

        [Display(Name = "تصاویر خبر")]
        public List<IFormFile> ImageFiles { get; set; } // تغییر از ImageFile به ImageFiles
    }
}
