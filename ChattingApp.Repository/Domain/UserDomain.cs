using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Repository.Domain
{
    public class UserDomain
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "user name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]
        public string Email { get; set; }

        public string Img { get; set; }
    }
}