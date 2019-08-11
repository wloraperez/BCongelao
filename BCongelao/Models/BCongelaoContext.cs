using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCongelao.Models;

namespace BCongelao.Models
{
    public class BCongelaoContext : DbContext
    {
        public BCongelaoContext (DbContextOptions<BCongelao.Models.BCongelaoContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TransferPayment> TransferPayments{ get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
    }
}
