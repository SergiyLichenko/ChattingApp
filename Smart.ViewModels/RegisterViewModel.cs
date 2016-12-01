using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Smart.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Input login please")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Input password please")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password please")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Input email please")]
        [Required(ErrorMessage = "Input email address please")]
        public string Email { get; set; }
    }
}