using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Smart.Data;

namespace Smart.Models.Entities
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