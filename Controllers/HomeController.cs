﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Joya.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Joya.Data;
using Microsoft.EntityFrameworkCore;

namespace Joya.Controllers
{
    public class HomeController : Controller
    {
        private readonly JoyaDbContext _context;

        public HomeController(JoyaDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.Store.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Charts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
