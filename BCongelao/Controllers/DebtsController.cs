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
    public class DebtsController : Controller
    {
        private readonly BCongelaoContext _context;

        public DebtsController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: DebtSales
        public async Task<ActionResult> DebtSales()
        {
            ViewData["TotalDeudaVentas"] = _context.Sales.Where(s => s.Debt > 0).Sum(s => s.Debt).ToString("C2");
            var debtSales = _context.Sales.Include(s => s.Customer).Include(s => s.SalesPerson).Where(s => s.Debt > 0 && s.StatusSale == Models.Enum.StatusSale.Activa).OrderBy(o => o.SaleDate);
            return View(await debtSales.ToListAsync());
        }

        // GET: DebtBalances
        public ViewResult DebtBalances()
        {
            //var query = _context.Sales.Where(s => s.Debt > 0).GroupBy(s => s.PaymentType);
            var query = _context.Sales.Where(s => s.Debt > 0 && s.StatusSale == Models.Enum.StatusSale.Activa).GroupBy(s => s.PaymentType).Select(s => new { PaymentType = s.Key, Debt = s.Sum(g => g.Debt) });

            List<SaleDebt> saleDebt = new List<SaleDebt>();

            foreach (var item in query)
            {
                SaleDebt saleD = new SaleDebt();
                saleD.PaymentType = item.PaymentType;
                saleD.Debt = item.Debt;
                saleDebt.Add(saleD);
            }

            ViewData["TotalDeudaBalances"] = _context.Sales.Where(s => s.Debt > 0 && s.StatusSale == Models.Enum.StatusSale.Activa).Sum(s => s.Debt).ToString("C2");
            return View(saleDebt.ToList());
        }

        // GET: Sales/Pay/5
        public async Task<IActionResult> PaySale(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .FirstOrDefaultAsync(m => m.SaleId == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Pay/5
        [HttpPost, ActionName("PaySale")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaySaleConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            var paidOld = sale.Paid;

            sale.Debt = 0;
            sale.Paid = sale.Total;

            decimal _balance = 0;
            _balance = sale.Paid - paidOld;

            Balance balance = await _context.Balances.FirstOrDefaultAsync(d => d.PaymentType == sale.PaymentType);
            balance.Total += _balance;

            _context.Update(balance);
            _context.Update(sale);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DebtSales));
        }
    }
}
