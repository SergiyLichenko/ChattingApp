﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingApp.Repository.Models
{
    [Table("ReferrerMessages")]
    public class ReferrerMessage
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }

        [Required]
        public System.Guid MessageId { get; set; }
        public Message Message { get; set; }

        [Required]
        public System.Guid ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}