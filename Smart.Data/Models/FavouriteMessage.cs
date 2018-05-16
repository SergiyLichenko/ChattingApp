using System.ComponentModel.DataAnnotations;

namespace ChattingApp.Repository.Models
{
    public class FavouriteMessage
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }

        [Required]
        public System.Guid MessageId { get; set; }
        public Message Message { get; set; }

        [Required]
        public System.Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}