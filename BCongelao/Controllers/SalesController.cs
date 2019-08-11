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
    public class SalesController : Controller
    {
        private readonly BCongelaoContext _context;

        public SalesController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var bCongelaoContext = _context.Sales.Include(s => s.Customer).Include(s => s.SalesPerson).OrderByDescending(o => o.SaleDate).ThenBy(o => o.Customer.CustomerName);
            return View(await bCongelaoContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Sales/Create
        public IActionResult Create()
        {
            Sale sale = new Sale();
            sale.Customer = new Customer();
            sale.SaleDate = DateTime.Now;
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName");
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName");
            return View(sale);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,CustomerId,CustomerDescription,SaleDate,PaymentType,Delivery,SalesPersonId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                sale.CreatedDate = DateTime.Now;
                sale.StatusSale = Models.Enum.StatusSale.Activa;

                sale.Discount = 0;
                sale.Quantity = 0;
                sale.Paid = 0;
                sale.Total = sale.Delivery;
                sale.TotalNoITBIS = sale.Delivery;

                sale.UserId = User.Identity.Name.ToString();

                if (sale.CustomerId <= 0 && sale.Customer.CustomerName != string.Empty)
                {
                    Customer customer = new Customer();
                    customer.CustomerName = sale.Customer.CustomerName;
                    customer.CreatedDate = DateTime.Now;
                    customer.StatusCustomer = Models.Enum.StatusCustomer.Activo;
                    customer.UserId = User.Identity.Name.ToString();

                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    sale.CustomerId = customer.CustomerId;
                }
                else
                {
                    sale.Customer = null;
                }

                _context.Add(sale);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName", sale.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName", sale.SalesPersonId);
            ViewData["SaleId"] = sale.SaleId;
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName", sale.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName", sale.SalesPersonId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,CustomerId,CustomerDescription,SaleDate,Paid,Debt,SalesPersonId,Delivery")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Sale saleOld = await _context.Sales.FirstOrDefaultAsync(s => s.SaleId == id);
                    if (saleOld.Delivery != sale.Delivery)
                    {
                        decimal _delivery = sale.Delivery - saleOld.Delivery;

                        sale.Total = saleOld.Total + _delivery;
                        sale.TotalNoITBIS = saleOld.TotalNoITBIS + _delivery;
                    }
                    else
                    {
                        sale.Total = saleOld.Total;
                        sale.TotalNoITBIS = saleOld.TotalNoITBIS;
                    }
                    
                    sale.ITBIS = saleOld.ITBIS;
                    sale.Quantity = saleOld.Quantity;
                    sale.PaymentType = saleOld.PaymentType;
                    sale.StatusSale = saleOld.StatusSale;
                    sale.CreatedDate = saleOld.CreatedDate;
                    sale.UserId = saleOld.UserId;
                    sale.Discount = saleOld.Discount;

                    sale.Debt = sale.Total - sale.Paid;

                    decimal _balance = 0;
                    _balance = sale.Paid - saleOld.Paid;

                    Balance balance = await _context.Balances.FirstOrDefaultAsync(d => d.PaymentType == sale.PaymentType);
                    balance.Total += _balance;
                    _context.Update(balance);

                    _context.Entry(saleOld).State = EntityState.Detached; //detach old to update the new

                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.SaleId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName", sale.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName", sale.SalesPersonId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            sale.StatusSale = Models.Enum.StatusSale.Anulada;
            sale.UserId = User.Identity.Name;

            Balance balance = await _context.Balances.FirstOrDefaultAsync(d => d.PaymentType == sale.PaymentType);
            balance.Total -= sale.Paid;
            _context.Update(balance);
            _context.Update(sale);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }
    }
}
