using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Joya.Data;
using Joya.Models;
using Microsoft.AspNetCore.Authorization;

namespace Joya.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RingsController : Controller
    {
        private readonly JoyaDbContext _context;

        public RingsController(JoyaDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]

        public async Task<ActionResult> Index(string ringType, string searchString, string bussinesName)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Rings
                                            orderby m.Type
                                            select m.Type;

            //var rings = from m in db.Rings select m;


            var rings = (from m in _context.Rings
                         join supplier in _context.Suppliers on m.SupplierId equals supplier.Id
                         select new { bussinessName = supplier.BussinesName, CatalogNumber = m.CatalogNumber, Type = m.Type, Color = m.Color, ImageLocation = m.ImageLocation, Name = m.Name, Price = m.Price, Size = m.Size, SupplierId = m.SupplierId })
                        .ToList()
                        .Select(c => new RingJoinBusiness
                        {
                            BusinessName = c.bussinessName,
                            CatalogNumber = c.CatalogNumber,
                            Type = c.Type,
                            Color = c.Color,
                            ImageLocation = c.ImageLocation,
                            Name = c.Name,
                            Price = c.Price,
                            Size = c.Size,
                            SupplierId = c.SupplierId
                        });

            if (!string.IsNullOrEmpty(bussinesName))
            {
                rings = rings.Where(s => s.Color.Contains(bussinesName));
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                rings = rings.Where(s => s.Color.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(ringType))
            {
                rings = rings.Where(x => x.Type == ringType);
            }

            var ringFilterVM = new RingFilterViewModel
            {
                Types = new SelectList(await genreQuery.Distinct().ToListAsync(), ringType),
                Rings = rings.ToList()
            };

            return View(ringFilterVM);


        }

        [AllowAnonymous]

        // GET: Rings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ring = await _context.Rings
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogNumber == id);
            if (ring == null)
            {
                return NotFound();
            }

            return View(ring);
        }


        // GET: Rings/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: Rings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogNumber,Name,Type,Color,Price,SupplierId,Size,ImageLocation")] Ring ring)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ring);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", ring.SupplierId);
            return View(ring);
        }

        // GET: Rings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ring = await _context.Rings.FindAsync(id);
            if (ring == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", ring.SupplierId);
            return View(ring);
        }

        // POST: Rings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogNumber,Name,Type,Color,Price,SupplierId,Size,ImageLocation")] Ring ring)
        {
            if (id != ring.CatalogNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ring);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RingExists(ring.CatalogNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", ring.SupplierId);
            return View(ring);
        }

        // GET: Rings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ring = await _context.Rings
                .Include(r => r.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogNumber == id);
            if (ring == null)
            {
                return NotFound();
            }

            return View(ring);
        }

        // POST: Rings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ring = await _context.Rings.FindAsync(id);
            _context.Rings.Remove(ring);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RingExists(int id)
        {
            return _context.Rings.Any(e => e.CatalogNumber == id);
        }
    }
}
