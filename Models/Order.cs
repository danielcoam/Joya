using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Joya.Models
{
    public class Order
        
    {
        public int OrderId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int RingId { get; set; }

        [ForeignKey("RingId")]
        public virtual Ring Ring { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

        [Required]
        public string Status { get; set; }

        public decimal TotalPrice { get; set; }
    }

   
}
