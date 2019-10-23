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
        public IActionResult Second()
        {
            return View();
        }

        public IDictionary<string,decimal> GetOrders()
        {
                    
            var total = from order in _context.Orders
                        group order by order.RingId into ordersGroup
                        join ring in _context.Rings on ordersGroup.FirstOrDefault().RingId equals ring.CatalogNumber
                        select new
                        {
                            totalSum = ordersGroup.Sum(x => x.TotalPrice),
                            RingName = ring.Name
                        };


            return total.ToDictionary(x => x.RingName, x => x.totalSum);
        }
        public IDictionary<DateTime, decimal> GetPricesByDates()
        {
            var q = from order in _context.Orders
                    group order by order.CreationDate.Date into g
                    select new
                    {
                        iCount = g.Sum(x=>x.TotalPrice),
                        strDate = g.Key
                    };




            return q.ToDictionary(x => x.strDate.Date, x => x.iCount);
        }
    }
}