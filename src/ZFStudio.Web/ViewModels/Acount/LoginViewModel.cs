using System.ComponentModel.DataAnnotations;

namespace ZFStudio.Web.ViewModels.Acount
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 4)]
        [Display(Name = "账号")]
        public string UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        [Display(Name ="密码")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }

        [Display(Name = "记住我")]
        public bool IsRememberMe { get; set; } = false;
    }
}
