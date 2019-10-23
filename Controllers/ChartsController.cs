using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Joya.Data;
using Joya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joya.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChartsController : Controller
    {
        private readonly JoyaDbContext _context;

        public ChartsController(JoyaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IDictionary<string,int> GetOrders()
        {
            var rings = from order in _context.Orders
                        join ring in _context.Rings on order.RingId equals ring.CatalogNumber
                        select new { RingName = ring.Name, Count = order.Count };
                        


            return rings.ToDictionary(x => x.RingName, x => x.Count);
        }
    }
}