using Reporting.Data;
using Reporting.Data.Models;
using HotChocolate.Subscriptions;
namespace Reporting.GraphQL
{
    public class Query
    {
        public async Task<List<Invoice>> GetAllInvoices([Service] IInvoiceRepository InvoiceRepository, [Service] ITopicEventSender eventSender)
        {
            List<Invoice> Invoices = InvoiceRepository.GetInvoices();
            await eventSender.SendAsync("ReturnedInvoices", Invoices);
            return Invoices;
        }
        public async Task<Invoice> GetInvoiceById([Service] IInvoiceRepository InvoiceRepository, [Service] ITopicEventSender eventSender, int id)
        {
            Invoice Invoice = InvoiceRepository.GetInvoiceById(id);
            await eventSender.SendAsync("ReturnedInvoice", Invoice);
            return Invoice;
        }
    }
}