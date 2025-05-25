using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.AdminViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان خبر الزامی است")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیحات خبر الزامی است")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "تصویر خبر")]
        public IFormFile ImageFile { get; set; }

    }
}
