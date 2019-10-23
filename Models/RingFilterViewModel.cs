using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joya.Models
{
    public class RingFilterViewModel
    {
        public List<RingJoinBusiness> Rings { get; set; }
        public SelectList Types { get; set; }
        public string RingType { get; set; }
        public string SearchString { get; set; }
    }
}
