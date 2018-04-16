using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheCakeShop.Models.ViewModels.Account
{
    public class LoginUserVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        [Required]
        public bool RememberME { get; set; }
    }
}