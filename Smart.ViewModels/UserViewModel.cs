using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smart.ViewModels
{
    public class UserViewModel
    {
        //public Guid id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "user name")]
        public string userName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public Guid id { get; set; }
        public string img { get; set; }
    }
}