using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "شماره دانشجویی خود را وارد کنید ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(14, ErrorMessage = "{0} باید 14 کاراکتر باشد ", MinimumLength = 14)]
        public string studentNumber { get; set; }
        [Display(Name = " رمز عبور ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        [StringLength(20, ErrorMessage = "  {0}   باید بین {2} کاراکتر تا {1} کاراکتر باشد    ", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار ")]
        public bool RememberMe { get; set; }
    }
}
