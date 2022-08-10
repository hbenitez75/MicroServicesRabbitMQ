using Reporting.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Reporting.Data
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public InvoiceItemRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            using (var _applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                _applicationDbContext.Database.EnsureCreated();
            }
        }
        public List<InvoiceItem> GetInvoiceItems()
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.InvoiceItem.ToList();
            }
        }
        public InvoiceItem GetInvoiceItemById(int id)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                return applicationDbContext.InvoiceItem.SingleOrDefault(it => it.Id == id);
            }
        }
        public async Task<InvoiceItem> CreateInvoiceItem(InvoiceItem invoiceItem)
        {
            using (var applicationDbContext = _dbContextFactory.CreateDbContext())
            {
                await applicationDbContext.InvoiceItem.AddAsync(invoiceItem);
                await applicationDbContext.SaveChangesAsync();

                return invoiceItem;
            }
        }
    }
}