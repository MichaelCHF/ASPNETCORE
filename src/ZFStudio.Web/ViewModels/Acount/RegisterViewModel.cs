using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZFStudio.Web.ViewModels.Acount
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 4)]
        public string UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string Passwrod { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
