using Reporting.Data;
using Reporting.Data.Models;
using HotChocolate.Resolvers;

namespace Reporting.GraphQL
{
    public class InvoiceItemResolver
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoiceItemResolver([Service]IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }
        public IEnumerable<InvoiceItem> GetInvoiceItems ([Parent] Invoice invoice, IResolverContext ctx)
        {
            return _invoiceItemRepository.GetInvoiceItems().Where(ii => ii.InvoiceId == invoice.Id);
        }
    }
}
