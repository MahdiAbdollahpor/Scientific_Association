using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره همراه ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(11, ErrorMessage = "{0} باید 11 کاراکتر باشد ", MinimumLength = 11)]
        public string PhoneNumber { get; set; }
        [Display(Name = " نام  ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(25, ErrorMessage = "  {0}   باید بین {2} کاراکتر تا {1} کاراکتر باشد    ", MinimumLength = 4)]
        public string firstName { get; set; }
        [Display(Name = " نام خانوادگی  ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(25, ErrorMessage = "  {0}   باید بین {2} کاراکتر تا {1} کاراکتر باشد    ", MinimumLength = 4)]
        public string lastName { get; set; }
        [Display(Name = "کد ملی خود را وارد کنید ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(10, ErrorMessage = "{0} باید 10 کاراکتر باشد ", MinimumLength = 10)]
        public string nationalCode { get; set; }
        [Display(Name = "شماره دانشجویی خود را وارد کنید ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(14, ErrorMessage = "{0} باید 14 کاراکتر باشد ", MinimumLength = 14)]
        public string studentNumber { get; set; }
        [Display(Name = " رمز عبور ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(20, ErrorMessage = "  {0}   باید بین {2} کاراکتر تا {1} کاراکتر باشد    ", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = " تکرار رمز عبور ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(20, ErrorMessage = "  {0}   باید بین {2} کاراکتر تا {1} کاراکتر باشد    ", MinimumLength = 5)]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار یکی نیست")]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
