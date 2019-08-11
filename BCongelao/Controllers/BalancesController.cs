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
    public class BalancesController : Controller
    {
        private readonly BCongelaoContext _context;

        public BalancesController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: Balances
        public async Task<IActionResult> Index()
        {
            ViewData["TotalBalance"] = _context.Balances.Where(o => o.PaymentType != Models.Enum.PaymentType.Gasto).Sum(o => o.Total).ToString("C2");
            ViewData["TotalGasto"] = _context.Balances.Where(o => o.PaymentType == Models.Enum.PaymentType.Gasto).Sum(o => o.Total).ToString("C2");
            return View(await _context.Balances.ToListAsync());
        }

        // GET: Balances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances
                .FirstOrDefaultAsync(m => m.BalanceId == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // GET: Balances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Balances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BalanceId,PaymentType,Total")] Balance balance)
        {
            if (ModelState.IsValid)
            {
                balance.CreatedDate = DateTime.Now;
                balance.UpdateDate = DateTime.Now;
                balance.UserId = User.Identity.Name.ToString();

                _context.Add(balance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(balance);
        }

        // GET: Balances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances.FindAsync(id);
            if (balance == null)
            {
                return NotFound();
            }
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BalanceId,PaymentType,Total,CreatedDate")] Balance balance)
        {
            if (id != balance.BalanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    balance.UpdateDate = DateTime.Now;
                    balance.UserId = User.Identity.Name.ToString();

                    _context.Update(balance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BalanceExists(balance.BalanceId))
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
            return View(balance);
        }

        // GET: Balances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances
                .FirstOrDefaultAsync(m => m.BalanceId == id);
            if (balance == null)
            {
                return NotFound();
            }

            return View(balance);
        }

        // POST: Balances/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    //var balance = await _context.Balances.FindAsync(id);
        //    //_context.Balances.Remove(balance);
        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool BalanceExists(int id)
        {
            return _context.Balances.Any(e => e.BalanceId == id);
        }
    }
}
