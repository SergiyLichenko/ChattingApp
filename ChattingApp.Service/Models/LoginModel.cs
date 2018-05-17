using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Service.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter login please")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Password please")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}