using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Smart.Data;

namespace Smart.Models.Entities
{
    
    public class Message
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }
        public string Text { get; set; }

        [Required]
        public System.DateTime CreateDate { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public System.Guid UserId { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsRead { get; set; }

        [Required]
        public System.Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public bool IsModified { get; set; }
    }
}