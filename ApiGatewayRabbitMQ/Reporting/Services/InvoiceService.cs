using Reporting.Data;
using Reporting.Data.Models;
using Serilog;

namespace Reporting.Services
{
    public class InvoiceService : IInvoiceService
    {
        public readonly IInvoiceRepository invoiceRepository;
        public InvoiceService(IInvoiceRepository _invoiceRepository)
        {
            invoiceRepository = _invoiceRepository;
        }

        public async Task<Invoice> CreateInvoice(Invoice invoice) {
            try
            {
                invoice =  await invoiceRepository.CreateInvoice(invoice);
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
            return invoice;
        }
    }
}
