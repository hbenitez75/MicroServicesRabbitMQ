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

        protected override void OnModelCreating
        (ModelBuilder modelBuilder)
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
    }
}