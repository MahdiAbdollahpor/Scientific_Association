using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class UserInfoForUserPanelViewModel
    {
        [ValidateNever]
        public int UserId { get; set; }
        [Display(Name = "نام ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string firstName { get; set; }
        [Display(Name = " نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string lastName { get; set; }
        [Display(Name = " کد ملی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string nationalCode { get; set; }
        [Display(Name = " کد ملی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string studentNumber { get; set; }
        [ValidateNever]
        public string RegisterTime { get; set; }
        [ValidateNever]
        public string PhoneNumber { get; set; }
    }
}
