using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smart.Models.Entities
{
  
   public class Role
    {
        [Key]
        [Required]
        public System.Guid Id { get; set; }
        public string Name { get; set; }

    }
}