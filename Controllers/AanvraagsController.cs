using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C_Geoproffs.Data;
using C_Geoproffs.Models;

namespace C_Geoproffs.Controllers
{
    public class AanvraagsController : Controller
    {
        private readonly C_GeoproffsContext _context;

        public AanvraagsController(C_GeoproffsContext context)
        {
            _context = context;
        }

        // GET: Aanvraags
        public async Task<IActionResult> Index(string searchString)
        {
            var aanvragen = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                aanvragen = aanvragen.Where(s => s.Naam!.Contains(searchString));
            }

            return View(await aanvragen.ToListAsync());
        }

        // GET: Aanvraags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var aanvraag = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanvraag == null)
            {
                return NotFound();
            }

            return View(aanvraag);
        }

        // GET: Aanvraags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aanvraags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Rol,Verdeling,BeginDatum,EindDatum,Reden,Status")] Aanvraag aanvraag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aanvraag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aanvraag);
        }

        // GET: Aanvraags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var aanvraag = await _context.Movie.FindAsync(id);
            if (aanvraag == null)
            {
                return NotFound();
            }
            return View(aanvraag);
        }

        // POST: Aanvraags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Rol,Verdeling,BeginDatum,EindDatum,Reden,Status")] Aanvraag aanvraag)
        {
            if (id != aanvraag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aanvraag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AanvraagExists(aanvraag.Id))
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
            return View(aanvraag);
        }

        // GET: Aanvraags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var aanvraag = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanvraag == null)
            {
                return NotFound();
            }

            return View(aanvraag);
        }

        // POST: Aanvraags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'C_GeoproffsContext.Movie'  is null.");
            }
            var aanvraag = await _context.Movie.FindAsync(id);
            if (aanvraag != null)
            {
                _context.Movie.Remove(aanvraag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AanvraagExists(int id)
        {
          return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
