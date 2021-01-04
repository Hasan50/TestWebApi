using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem.WebApi.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class LocalPasswordModel
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
