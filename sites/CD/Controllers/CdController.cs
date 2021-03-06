#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CD.Data;
using CD.Models;

namespace CD.Controllers
{
    public class CdController : Controller
    {
        private readonly CdLibraryContext _context;

        public CdController(CdLibraryContext context)
        {
            _context = context;
        }

        // GET: Cd
        public async Task<IActionResult> Index(string titleSearch)
        {
            var cdLibraryContext = _context.Cds
                .Include(c => c.Artist)
                .Include(c => c.Lending);
            if (titleSearch != null)
            {
                var result = await cdLibraryContext.Where(cd => cd.Title.ToLower().Contains(titleSearch.ToLower())).ToListAsync();
                return View(result);
            }
            else
            {
                var result = await cdLibraryContext.ToListAsync();
                return View(result);
            }
        }

        // GET: Cd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cd = await _context.Cds
                .Include(c => c.Artist)
                .Include(c => c.Lending.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cd == null)
            {
                return NotFound();
            }

            if (cd.Lending == null)
            {
                ViewData["Users"] = new SelectList(_context.Users, "Id", "Name");
            }

            return View(cd);
        }

        // GET: Cd/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            return View();
        }

        // POST: Cd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Category,ArtistId")] Cd cd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", cd.ArtistId);
            return View(cd);
        }

        // GET: Cd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cd = await _context.Cds.FindAsync(id);
            if (cd == null)
            {
                return NotFound();
            }

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", cd.ArtistId);
            return View(cd);
        }

        // POST: Cd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,ArtistId")] Cd cd)
        {
            if (id != cd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CdExists(cd.Id))
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

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Name", cd.ArtistId);
            return View(cd);
        }

        // GET: Cd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cd = await _context.Cds
                .Include(c => c.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cd == null)
            {
                return NotFound();
            }

            return View(cd);
        }

        [HttpPost]
        public async Task<IActionResult> Lend(int? cdId, int? userId)
        {
            if (cdId == null || userId == null)
            {
                return NotFound();
            }
            
            var cd = await this._context.Cds.FindAsync(cdId);
            var user = await this._context.Users.FindAsync(userId);
            if (cd == null)
            {
                return NotFound();
            }

            if (user == null)
            {
                return NotFound();
            }
            
            var lendings = new Lending(cdId.Value, userId.Value);
            this._context.Add(lendings);
            await this._context.SaveChangesAsync();
            
            return RedirectToAction("Details", new { id = cdId });
        }

        // POST: Cd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cd = await _context.Cds.FindAsync(id);
            _context.Cds.Remove(cd);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CdExists(int id)
        {
            return _context.Cds.Any(e => e.Id == id);
        }
    }
}