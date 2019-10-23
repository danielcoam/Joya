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
    [Authorize]
    public class BraceletsController : Controller
    {
        private readonly JoyaDbContext _context;

        public BraceletsController(JoyaDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Bracelets
        public async Task<IActionResult> Index()
        {
            var joyaDbContext = _context.Bracelets.Include(b => b.Supplier);
            return View(await joyaDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Bracelets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bracelet = await _context.Bracelets
                .Include(b => b.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogNumber == id);
            if (bracelet == null)
            {
                return NotFound();
            }

            return View(bracelet);
        }

        // GET: Bracelets/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: Bracelets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogNumber,Name,Type,Color,Price,SupplierId,Size,ImageLocation")] Bracelet bracelet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bracelet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", bracelet.SupplierId);
            return View(bracelet);
        }

        // GET: Bracelets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bracelet = await _context.Bracelets.FindAsync(id);
            if (bracelet == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", bracelet.SupplierId);
            return View(bracelet);
        }

        // POST: Bracelets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogNumber,Name,Type,Color,Price,SupplierId,Size,ImageLocation")] Bracelet bracelet)
        {
            if (id != bracelet.CatalogNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bracelet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BraceletExists(bracelet.CatalogNumber))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", bracelet.SupplierId);
            return View(bracelet);
        }

        // GET: Bracelets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bracelet = await _context.Bracelets
                .Include(b => b.Supplier)
                .FirstOrDefaultAsync(m => m.CatalogNumber == id);
            if (bracelet == null)
            {
                return NotFound();
            }

            return View(bracelet);
        }

        // POST: Bracelets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bracelet = await _context.Bracelets.FindAsync(id);
            _context.Bracelets.Remove(bracelet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BraceletExists(int id)
        {
            return _context.Bracelets.Any(e => e.CatalogNumber == id);
        }
    }
}
