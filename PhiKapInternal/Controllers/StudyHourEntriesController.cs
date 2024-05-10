using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhiKapInternal.Models.PKT_Internal;

namespace PhiKapInternal.Controllers
{
    public class StudyHourEntriesController : Controller
    {
        private readonly PktInternalContext _context;

        public StudyHourEntriesController(PktInternalContext context)
        {
            _context = context;
        }

        // GET: StudyHourEntries
        public async Task<IActionResult> Index()
        {
              return _context.StudyHourEntries != null ? 
                          View(await _context.StudyHourEntries.ToListAsync()) :
                          Problem("Entity set 'PktInternalContext.StudyHourEntries'  is null.");
        }

        // GET: StudyHourEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudyHourEntries == null)
            {
                return NotFound();
            }

            var studyHourEntry = await _context.StudyHourEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyHourEntry == null)
            {
                return NotFound();
            }

            return View(studyHourEntry);
        }

        // GET: StudyHourEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudyHourEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssociateId,ProctorId,Length,Date")] StudyHourEntry studyHourEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studyHourEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studyHourEntry);
        }

        // GET: StudyHourEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudyHourEntries == null)
            {
                return NotFound();
            }

            var studyHourEntry = await _context.StudyHourEntries.FindAsync(id);
            if (studyHourEntry == null)
            {
                return NotFound();
            }
            return View(studyHourEntry);
        }

        // POST: StudyHourEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssociateId,ProctorId,Length,Date")] StudyHourEntry studyHourEntry)
        {
            if (id != studyHourEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studyHourEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyHourEntryExists(studyHourEntry.Id))
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
            return View(studyHourEntry);
        }

        // GET: StudyHourEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudyHourEntries == null)
            {
                return NotFound();
            }

            var studyHourEntry = await _context.StudyHourEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studyHourEntry == null)
            {
                return NotFound();
            }

            return View(studyHourEntry);
        }

        // POST: StudyHourEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudyHourEntries == null)
            {
                return Problem("Entity set 'PktInternalContext.StudyHourEntries'  is null.");
            }
            var studyHourEntry = await _context.StudyHourEntries.FindAsync(id);
            if (studyHourEntry != null)
            {
                _context.StudyHourEntries.Remove(studyHourEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyHourEntryExists(int id)
        {
          return (_context.StudyHourEntries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
