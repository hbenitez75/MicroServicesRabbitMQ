using Reporting.Data.Models;
namespace Reporting.Data
{
    public interface IInvoiceRepository
    {
        public List<Invoice> GetInvoices();
        public Invoice GetInvoiceById(int id);
        public Task<Invoice> CreateInvoice(Invoice invoice);
    }
}
