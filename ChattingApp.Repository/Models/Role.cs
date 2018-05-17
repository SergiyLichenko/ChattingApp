using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Repository.Models
{
    public class Role
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }
        public string Name { get; set; }
    }
}