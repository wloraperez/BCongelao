using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BCongelao.Models;

namespace BCongelao.Controllers
{
    public class SalesPersonsController : Controller
    {
        private readonly BCongelaoContext _context;

        public SalesPersonsController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: SalesPersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.SalesPersons.ToListAsync());
        }

        // GET: SalesPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons
                .FirstOrDefaultAsync(m => m.SalesPersonId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesPersonId,SalesPersonName,Phone,Address")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                salesPerson.CreatedDate = DateTime.Now;
                salesPerson.UserId = User.Identity.Name;
                salesPerson.StatusSalesPerson = Models.Enum.StatusSalesPerson.Activo;
                _context.Add(salesPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesPersonId,SalesPersonName,Phone,Address,StatusSalesPerson,CreatedDate,UserId")] SalesPerson salesPerson)
        {
            if (id != salesPerson.SalesPersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonExists(salesPerson.SalesPersonId))
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
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersons
                .FirstOrDefaultAsync(m => m.SalesPersonId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPerson = await _context.SalesPersons.FindAsync(id);
            //_context.SalesPersons.Remove(salesPerson);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPersonExists(int id)
        {
            return _context.SalesPersons.Any(e => e.SalesPersonId == id);
        }
    }
}
