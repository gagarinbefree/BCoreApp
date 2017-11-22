using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BCoreIdentity.Models.AccountViewModels
{
    public class LoginViewModel
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
