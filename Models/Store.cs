using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joya.Models
{
    public class Store
    {
        public int Id{get; set;}
        public string streetName { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
