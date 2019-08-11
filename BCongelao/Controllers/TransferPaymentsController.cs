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
    public class TransferPaymentsController : Controller
    {
        private readonly BCongelaoContext _context;

        public TransferPaymentsController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: TransferPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransferPayments.ToListAsync());
        }

        // GET: TransferPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferPayment = await _context.TransferPayments
                .FirstOrDefaultAsync(m => m.TransferPaymentId == id);
            if (transferPayment == null)
            {
                return NotFound();
            }

            return View(transferPayment);
        }

        // GET: TransferPayments/Create
        public IActionResult Create()
        {
            InitialCreate(0, 0);
            return View();
        }

        // POST: TransferPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransferPaymentId,PaymentTypeFrom,PaymentTypeTo,Total,Description")] TransferPayment transferPayment)
        {
            if (ModelState.IsValid)
            {
                transferPayment.CreatedDate = DateTime.Now;
                transferPayment.UserId = User.Identity.Name.ToString();

                var balanceFrom = await _context.Balances
                .FirstOrDefaultAsync(m => m.PaymentType == transferPayment.PaymentTypeFrom);

                balanceFrom.Total = balanceFrom.Total - transferPayment.Total;
                balanceFrom.UpdateDate = DateTime.Now;
                balanceFrom.UserId = User.Identity.Name.ToString();

                var balanceTo = await _context.Balances
                .FirstOrDefaultAsync(m => m.PaymentType == transferPayment.PaymentTypeTo);

                balanceTo.Total = balanceTo.Total + transferPayment.Total;
                balanceTo.UpdateDate = DateTime.Now;
                balanceTo.UserId = User.Identity.Name.ToString();

                _context.Update(balanceFrom);
                _context.Update(balanceTo);
                _context.Add(transferPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transferPayment);
        }

        // GET: TransferPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferPayment = await _context.TransferPayments.FindAsync(id);
            if (transferPayment == null)
            {
                return NotFound();
            }
            InitialCreate((int)transferPayment.PaymentTypeFrom, (int)transferPayment.PaymentTypeTo);
            return View(transferPayment);
        }

        // POST: TransferPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransferPaymentId,PaymentTypeFrom,PaymentTypeTo,Total,CreatedDate,UserId,Description")] TransferPayment transferPayment)
        {
            if (id != transferPayment.TransferPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferPaymentExists(transferPayment.TransferPaymentId))
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
            InitialCreate((int)transferPayment.PaymentTypeFrom, (int)transferPayment.PaymentTypeTo);
            return View(transferPayment);
        }

        // GET: TransferPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferPayment = await _context.TransferPayments
                .FirstOrDefaultAsync(m => m.TransferPaymentId == id);
            if (transferPayment == null)
            {
                return NotFound();
            }

            return View(transferPayment);
        }

        // POST: TransferPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transferPayment = await _context.TransferPayments.FindAsync(id);


            //update balances
            var balanceFrom = await _context.Balances
                .FirstOrDefaultAsync(m => m.PaymentType == transferPayment.PaymentTypeFrom);

            balanceFrom.Total = balanceFrom.Total + transferPayment.Total;
            balanceFrom.UpdateDate = DateTime.Now;
            balanceFrom.UserId = User.Identity.Name.ToString();

            var balanceTo = await _context.Balances
            .FirstOrDefaultAsync(m => m.PaymentType == transferPayment.PaymentTypeTo);

            balanceTo.Total = balanceTo.Total - transferPayment.Total;
            balanceTo.UpdateDate = DateTime.Now;
            balanceTo.UserId = User.Identity.Name.ToString();

            _context.Update(balanceFrom);
            _context.Update(balanceTo);

            //borrar transferencia
            _context.TransferPayments.Remove(transferPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferPaymentExists(int id)
        {
            return _context.TransferPayments.Any(e => e.TransferPaymentId == id);
        }

        private void InitialCreate(int p1, int p2)
        {
            Array values = System.Enum.GetValues(typeof(Models.Enum.PaymentType));
            List<SelectListItem> items = new List<SelectListItem>(values.Length);

            foreach (var c in values)
            {
                Models.Enum.PaymentType type = (Models.Enum.PaymentType)c;
                Balance bal = _context.Balances.FirstOrDefault(b => b.PaymentType == type);

                if (bal != null)
                {
                    var description = EnumHelper<Models.Enum.PaymentType>.GetDisplayValue(type);
                    items.Add(new SelectListItem
                    {
                        Text = description + " - " + bal.Total.ToString("C"),
                        Value = type.ToString()
                    });
                }
            }

            ViewData["EnvaseId"] = new SelectList(items.Select(s => new { EnvaseId = s.Value, EnvaseName = s.Text }), "EnvaseId", "EnvaseName", 0);

            ViewData["PaymentType1"] = new SelectList(items.Select(s => new { PaymentType = s.Value, PaymentDescription = s.Text }), "PaymentType", "PaymentDescription", p1);

            ViewData["PaymentType2"] = new SelectList(items.Select(s => new { PaymentType = s.Value, PaymentDescription = s.Text }), "PaymentType", "PaymentDescription", p2);

        }
    }
}
