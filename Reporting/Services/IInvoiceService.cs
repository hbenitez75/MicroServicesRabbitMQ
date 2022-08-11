using Reporting.Data.Models;

namespace Reporting.Services
{
    public interface IInvoiceService
    {
        public Task<Invoice> CreateInvoice(Invoice invoice);
    }
}
