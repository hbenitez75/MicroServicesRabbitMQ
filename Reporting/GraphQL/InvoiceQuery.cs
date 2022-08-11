using Reporting.Data;
using Reporting.Data.Models;
using HotChocolate.Subscriptions;
namespace Reporting.GraphQL
{
    public class Query
    {
        public async Task<List<Invoice>> GetAllInvoices([Service] IInvoiceRepository InvoiceRepository, [Service] ITopicEventSender eventSender)
        {
            List<Invoice> invoices = InvoiceRepository.GetInvoices();
            await eventSender.SendAsync("ReturnedInvoices", invoices);
            return invoices;
        }
        public async Task<Invoice> GetInvoiceById([Service] IInvoiceRepository InvoiceRepository, [Service] ITopicEventSender eventSender, int id)
        {
            Invoice invoice = InvoiceRepository.GetInvoiceById(id);
            await eventSender.SendAsync("ReturnedInvoice", invoice);
            return invoice;
        }
        public async Task<List<InvoiceItem>> GetAllInvoiceItems([Service] IInvoiceItemRepository invoiceItemRepository, [Service] ITopicEventSender eventSender)
        {
            List<InvoiceItem> invoiceItems = invoiceItemRepository.GetInvoiceItems();
            await eventSender.SendAsync("ReturnedInvoiceItems", invoiceItems);
            return invoiceItems;
        }
        public async Task<InvoiceItem> GetInvoiceItemById([Service] IInvoiceItemRepository invoiceItemRepository, [Service] ITopicEventSender eventSender, int id)
        {
            InvoiceItem invoice = invoiceItemRepository.GetInvoiceItemById(id);
            await eventSender.SendAsync("ReturnedInvoice", invoice);
            return invoice;
        }
    }
}