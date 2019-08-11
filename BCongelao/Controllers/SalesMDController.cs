using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCongelao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BCongelao.Controllers
{
    public class SalesMDController : Controller
    {
        private readonly BCongelaoContext _context;

        public SalesMDController(BCongelaoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bCongelaoContext = _context.Sales.Include(s => s.Customer).Include(s => s.SalesPerson).OrderByDescending(o => o.SaleDate).ThenBy(o => o.Customer.CustomerName);
            return View(await bCongelaoContext.ToListAsync());
        }

        public IActionResult SaleCreate()
        {
            Sale sale = new Sale();
            sale.Customer = new Customer();
            sale.SaleDate = DateTime.Now;
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName");
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName");
            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> SaleCreate(Sale sale)
        {
            if (ModelState.IsValid)
            {
                //default data
                sale.CreatedDate = DateTime.Now;
                sale.StatusSale = Models.Enum.StatusSale.Activa;

                sale.Quantity = 0;
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

                //each sale details, change sale amounts
                //_context.Sales.Add(sale);
                //await _context.SaveChangesAsync();
                //int id = sale.SaleId;
                if (sale.SaleDetails != null)
                {
                    decimal _quantities = sale.SaleDetails.Sum(s => s.Quantity);
                    if (_quantities > 0)
                    {
                        foreach (var detail in sale.SaleDetails)
                        {
                            if (detail.Quantity > 0)
                            {
                                //detail.SaleId = id;
                                Product product = await _context.Products.FirstOrDefaultAsync(d => d.ProductId == detail.ProductId);

                                detail.UserId = User.Identity.Name;
                                detail.UnitPrice = product.UnitPrice;
                                decimal _totalNoItbis = (detail.Quantity * detail.UnitPrice) - detail.Discount;
                                detail.ITBIS = _totalNoItbis * (product.ITBIS / 100);
                                detail.Total = _totalNoItbis + detail.ITBIS;

                                //product.UnitsInStock = product.UnitsInStock - detail.Quantity;

                                sale.Quantity = sale.Quantity + detail.Quantity;
                                sale.Discount = sale.Discount + detail.Discount;
                                sale.TotalNoITBIS = sale.TotalNoITBIS + _totalNoItbis;
                                sale.ITBIS = sale.ITBIS + detail.ITBIS;
                                sale.Total = sale.Total + detail.Total;

                                //_context.Update(product);
                                //_context.Add(detail);
                            }
                        }
                        if (sale.PaidTotal)
                        {
                            sale.Paid = sale.Total;
                            sale.Debt = 0;
                        }
                        else
                        {
                            sale.Debt = sale.Total - sale.Paid;
                        }
                        //_context.SaleDetails.AddRange(details);
                        _context.Sales.Add(sale);
                        Balance balance = await _context.Balances.FirstOrDefaultAsync(d => d.PaymentType == sale.PaymentType);
                        balance.Total += sale.Paid;
                        _context.Update(balance);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.StatusCustomer == Models.Enum.StatusCustomer.Activo).OrderBy(c => c.CustomerName), "CustomerId", "CustomerName", sale.CustomerId);
            ViewData["SalesPersonId"] = new SelectList(_context.SalesPersons.Where(s => s.StatusSalesPerson == Models.Enum.StatusSalesPerson.Activo).OrderBy(s => s.SalesPersonName), "SalesPersonId", "SalesPersonName", sale.SalesPersonId);
            ViewData["SaleId"] = sale.SaleId;
            return View(sale);
        }

        public IActionResult SaleDetailsCreate(int? i)
        {
            i--;
            ViewBag.i = i;

            ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Category == Models.Enum.CategoryProduct.Helado || p.Category == Models.Enum.CategoryProduct.Sorbete).Where(s => s.StatusProduct == Models.Enum.StatusProduct.Activo).Select(s => new { ProductId = s.ProductId, ProductName = s.ProductName + " - " + EnumHelper<Models.Enum.Envase>.GetDisplayValue(s.Envase) + " - " + s.UnitPrice.ToString("C") }).OrderBy(o => o.ProductName), "ProductId", "ProductName", 0);

            return PartialView();
        }
    }
}