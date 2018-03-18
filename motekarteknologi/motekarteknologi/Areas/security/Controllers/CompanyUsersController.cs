using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using motekarteknologi.Areas.security.Models;
using motekarteknologi.Data;

namespace motekarteknologi.Areas.security.Controllers
{
    [Area("security")]
    [Authorize]
    public class CompanyUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: security/CompanyUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CompanyUser.Include(c => c.ApplicationUser).Include(c => c.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: security/CompanyUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUser
                .Include(c => c.ApplicationUser)
                .Include(c => c.Company)
                .SingleOrDefaultAsync(m => m.CompanyID == id);
            if (companyUser == null)
            {
                return NotFound();
            }

            return View(companyUser);
        }

        // GET: security/CompanyUsers/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "Name");
            return View();
        }

        // POST: security/CompanyUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID,ApplicationUserID")] CompanyUser companyUser)
        {
            if (ModelState.IsValid)
            {
                companyUser.CompanyID = Guid.NewGuid();
                _context.Add(companyUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", companyUser.ApplicationUserID);
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "Name", companyUser.CompanyID);
            return View(companyUser);
        }

        // GET: security/CompanyUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUser.SingleOrDefaultAsync(m => m.CompanyID == id);
            if (companyUser == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", companyUser.ApplicationUserID);
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "Name", companyUser.CompanyID);
            return View(companyUser);
        }

        // POST: security/CompanyUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompanyID,ApplicationUserID")] CompanyUser companyUser)
        {
            if (id != companyUser.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyUserExists(companyUser.CompanyID))
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
            ViewData["ApplicationUserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", companyUser.ApplicationUserID);
            ViewData["CompanyID"] = new SelectList(_context.Company, "CompanyID", "Name", companyUser.CompanyID);
            return View(companyUser);
        }

        // GET: security/CompanyUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyUser = await _context.CompanyUser
                .Include(c => c.ApplicationUser)
                .Include(c => c.Company)
                .SingleOrDefaultAsync(m => m.CompanyID == id);
            if (companyUser == null)
            {
                return NotFound();
            }

            return View(companyUser);
        }

        // POST: security/CompanyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyUser = await _context.CompanyUser.SingleOrDefaultAsync(m => m.CompanyID == id);
            _context.CompanyUser.Remove(companyUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyUserExists(Guid id)
        {
            return _context.CompanyUser.Any(e => e.CompanyID == id);
        }
    }
}
