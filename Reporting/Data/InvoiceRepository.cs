using Reporting.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Reporting.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public InvoiceRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            using (var _applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                _applicationDbContext.Database.EnsureCreated();
            }
        }
        public List<Invoice> GetInvoices()
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.Invoice.ToList();
            }
        }
        public Invoice GetInvoiceById(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.Invoice.SingleOrDefault(x => x.Id == id);
            }
        }
        public async Task<Invoice> CreateInvoice(Invoice Invoice)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                await applicationDbContext.Invoice.AddAsync(Invoice);
                await applicationDbContext.SaveChangesAsync();

                return Invoice;
            }
        }
    }
}