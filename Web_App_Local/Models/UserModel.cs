using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_App_Local.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="UserName is must")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is must")]
         public string Password { get; set; }

    }

    public class RegisterUser
    {
        [Required(ErrorMessage = "Email is must")]
        [EmailAddress]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Password is must")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be minimum 8 characters and must be string password with uppercase character, number and sepcial character")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
