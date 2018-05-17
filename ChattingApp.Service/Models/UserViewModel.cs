using System;
using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Service.Models
{
    public class UserViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "user name")]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password and confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Guid Id { get; set; }
        public string Img { get; set; }
    }
}