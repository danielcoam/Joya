using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Joya.Models
{
    public class Ring :Jewelery
    {
       
        public string Type { get; set; }
        public string Color { get; set; }
     
        public decimal Size { get; set; }
   
    }
}
