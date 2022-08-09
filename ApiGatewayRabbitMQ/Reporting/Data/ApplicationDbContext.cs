using Reporting.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Reporting.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions
        <ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            CreateInovice(modelBuilder);
            CreateInoviceItem(modelBuilder);
        }

        private static void CreateInovice(ModelBuilder modelBuilder)
        {
            Invoice invoice1 = new Invoice
            {
                Id = 1,
                Amount = 100,
                Description = "invoice 1",
                InvoiceDate = DateTime.Now,
                InvoiceNumber = "123456",
                Paid = 0,
                PaidDate = DateTime.Now
            };

            Invoice invoice2 = new Invoice
            {
                Id = 2,
                Amount = 100,
                Description = "invoice 2",
                InvoiceDate = DateTime.Now,
                InvoiceNumber = "123456",
                Paid = 0,
                PaidDate = DateTime.Now
            };

            Invoice invoice3 = new Invoice
            {
                Id = 3,
                Amount = 100,
                Description = "invoice 3",
                InvoiceDate = DateTime.Now,
                InvoiceNumber = "123456",
                Paid = 0,
                PaidDate = DateTime.Now
            };

            modelBuilder.Entity<Invoice>().HasData(invoice1, invoice2, invoice3);
        } 
     
        private static void CreateInoviceItem(ModelBuilder modelBuilder)
        {
           var item1 = new InvoiceItem
            {
                Id = 1,
                InvoiceId = 1,
                Qty = 1,
                UnitPrice = 100,
                Description = "item 1",

            };
            var item2 = new InvoiceItem
            {
                Id = 2,
                InvoiceId = 1,
                Qty = 2,
                UnitPrice = 200,
                Description = "Item 2"

            };
            
            var item3 = new InvoiceItem
            {
                Id = 3,
                InvoiceId = 1,
                Qty = 3,
                UnitPrice = 100,
                Description = "Item 3"
            };
            modelBuilder.Entity<InvoiceItem>().HasData(item1, item2, item3);
        }
    }
}