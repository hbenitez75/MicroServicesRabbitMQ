using Reporting.Data;
using Reporting.Data.Models;
using HotChocolate.Resolvers;

namespace Reporting.GraphQL
{
    public class InvoiceResolver
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceResolver([Service]IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public Invoice  GetInvoice([Parent] InvoiceItem invoiceItem, IResolverContext ctx)
        {
            return _invoiceRepository.GetInvoices().Where(i => i.Id == invoiceItem.InvoiceId).FirstOrDefault();
        }
    }
}
