using Family.Data;
using Family.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Family.Controllers
{
    public class HomeController : Controller
    {
        private readonly FamilyDbContext _context;

        public HomeController(FamilyDbContext context)
        {
            _context = context;
        }

        //Get: Family Member
        public async Task<IActionResult> Index()
        {
            return View(await _context.familyTrees.ToListAsync());
        }

        //Get: family/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyTree = await _context.familyTrees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyTree == null)
            {
                return NotFound();
            }

            return View(familyTree);
        }

        // GET: Family Member/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Family/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Age, Occupation")] FamilyTree familyTree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyTree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(familyTree);
        }

        //Get: Family Member/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.familyTrees.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: Family Member/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, Age, Occupation")] FamilyTree familyTree)
        {
            if (id !=familyTree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyTree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExist(familyTree.Id))
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
            return View(familyTree);
        }

        // GET: Family Member/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.familyTrees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Family/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.familyTrees.FindAsync(id);
            _context.familyTrees.Remove(family);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyExist(int id)
        {
            return _context.familyTrees.Any(e => e.Id == id);
        }
    }
}
