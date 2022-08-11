using Reporting.Data.Models;

namespace Reporting.Data
{
    public interface IInvoiceItemRepository
    {
        Task<InvoiceItem> CreateInvoiceItem(InvoiceItem invoiceItem);
        InvoiceItem GetInvoiceItemById(int id);
        List<InvoiceItem> GetInvoiceItems();
    }
}