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
    public class SaleDetailsController : Controller
    {
        private readonly BCongelaoContext _context;

        public SaleDetailsController(BCongelaoContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var bCongelaoContext = _context.SaleDetails.Include(s => s.Product).Include(s => s.Sale).Include(s => s.Sale.Customer).OrderByDescending(o => o.Sale.SaleDate);
            return View(await bCongelaoContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public IActionResult Create(int? Id)
        {
            CreateInitial(0, Id);

            return View();
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleDetailId,SaleId,ProductId,Quantity,Discount")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(d => d.ProductId == saleDetail.ProductId);
                saleDetail.Sale = await _context.Sales.FirstOrDefaultAsync(d => d.SaleId == saleDetail.SaleId);

                saleDetail.UserId = User.Identity.Name;
                saleDetail.UnitPrice = product.UnitPrice;
                decimal _totalNoItbis = (saleDetail.Quantity * saleDetail.UnitPrice) - saleDetail.Discount;
                saleDetail.ITBIS = _totalNoItbis * (product.ITBIS / 100);
                saleDetail.Total = _totalNoItbis + saleDetail.ITBIS;

                saleDetail.Sale.Quantity = saleDetail.Sale.Quantity + saleDetail.Quantity;
                saleDetail.Sale.TotalNoITBIS = saleDetail.Sale.TotalNoITBIS + _totalNoItbis;
                saleDetail.Sale.Discount = saleDetail.Discount;
                saleDetail.Sale.ITBIS = saleDetail.Sale.ITBIS + saleDetail.ITBIS;
                saleDetail.Sale.Total = saleDetail.Sale.Total + saleDetail.Total;

                saleDetail.Sale.Debt = saleDetail.Sale.Total - saleDetail.Sale.Paid;

                _context.Add(saleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateInitial(saleDetail.ProductId, saleDetail.SaleId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            CreateInitial(saleDetail.ProductId, saleDetail.SaleId);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleDetailId,SaleId,ProductId,Quantity,Discount")] SaleDetail saleDetail)
        {
            if (id != saleDetail.SaleDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    saleDetail.Sale = await _context.Sales.FirstOrDefaultAsync(d => d.SaleId == saleDetail.SaleId);

                    SaleDetail oldSaleDetail = await _context.SaleDetails.FirstOrDefaultAsync(d => d.SaleDetailId == id);
                    //restar valores de detalle viejos

                    saleDetail.Sale.Quantity = saleDetail.Sale.Quantity - oldSaleDetail.Quantity;

                    saleDetail.Sale.Discount = saleDetail.Sale.Discount - oldSaleDetail.Discount;
                    saleDetail.Sale.TotalNoITBIS = saleDetail.Sale.TotalNoITBIS - (oldSaleDetail.UnitPrice * oldSaleDetail.Quantity) - oldSaleDetail.Discount;
                    saleDetail.Sale.ITBIS = saleDetail.Sale.ITBIS - oldSaleDetail.ITBIS;
                    saleDetail.Sale.Total = saleDetail.Sale.Total - oldSaleDetail.Total;

                    Product product = await _context.Products.FirstOrDefaultAsync(d => d.ProductId == saleDetail.ProductId);
                    saleDetail.UserId = User.Identity.Name;
                    saleDetail.UnitPrice = product.UnitPrice;
                    decimal _totalNoItbis = (saleDetail.Quantity * saleDetail.UnitPrice) - saleDetail.Discount;
                    saleDetail.ITBIS = _totalNoItbis * (product.ITBIS / 100);
                    saleDetail.Total = _totalNoItbis + saleDetail.ITBIS;

                    saleDetail.Sale.Quantity = saleDetail.Sale.Quantity + saleDetail.Quantity;
                    saleDetail.Sale.TotalNoITBIS = saleDetail.Sale.TotalNoITBIS + _totalNoItbis;
                    saleDetail.Sale.Discount = saleDetail.Sale.Discount + saleDetail.Discount;
                    saleDetail.Sale.ITBIS = saleDetail.Sale.ITBIS + saleDetail.ITBIS;
                    saleDetail.Sale.Total = saleDetail.Sale.Total + saleDetail.Total;

                    saleDetail.Sale.Debt = saleDetail.Sale.Total - saleDetail.Sale.Paid;

                    _context.Entry(oldSaleDetail).State = EntityState.Detached;
                    _context.Update(saleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.SaleDetailId))
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
            CreateInitial(saleDetail.ProductId, saleDetail.SaleId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetail = await _context.SaleDetails.FindAsync(id);
            var sale = await _context.Sales.FirstOrDefaultAsync(d => d.SaleId == saleDetail.SaleId);

            //restar valores de detalle
            sale.Quantity = sale.Quantity - saleDetail.Quantity;
            sale.TotalNoITBIS = sale.TotalNoITBIS - (saleDetail.UnitPrice * saleDetail.Quantity) - saleDetail.Discount;
            sale.Discount = sale.Discount - saleDetail.Discount;
            sale.ITBIS = sale.ITBIS - saleDetail.ITBIS;
            sale.Total = sale.Total - saleDetail.Total;

            _context.Sales.Update(sale);
            _context.SaleDetails.Remove(saleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetails.Any(e => e.SaleDetailId == id);
        }

        private void CreateInitial(int? productId, int? saleId)
        {
            ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Category == Models.Enum.CategoryProduct.Helado || p.Category == Models.Enum.CategoryProduct.Sorbete).Where(s => s.StatusProduct == Models.Enum.StatusProduct.Activo).Select(s => new { ProductId = s.ProductId, ProductName = s.ProductName + " - " + EnumHelper<Models.Enum.Envase>.GetDisplayValue(s.Envase) + " - " + s.UnitPrice.ToString("C") }).OrderBy(o => o.ProductName), "ProductId", "ProductName", productId);

            ViewData["SaleId"] = new SelectList(_context.Sales, "SaleId", "SaleId", saleId);
        }
    }
}
