using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Service.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Input login please")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Input Password please")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password please")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Input Email please")]
        [Required(ErrorMessage = "Input Email address please")]
        public string Email { get; set; }
    }
}