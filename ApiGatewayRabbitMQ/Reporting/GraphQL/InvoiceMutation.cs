using Reporting.Data;
using Reporting.Data.Models;
using HotChocolate.Subscriptions;
namespace Reporting.GraphQL
{
    public class InvoiceMutation
    {
        public async Task<Invoice> CreateInvoice([Service] IInvoiceRepository InvoiceRepository, [Service] ITopicEventSender eventSender, 
            int id, double amount, string description, DateTime invoiceDate, string invoiceNumber, int paid, DateTime paidDate)
        {
            var data = new Invoice
            {
                Id = id,
                Amount = amount,
                Description = description,
                InvoiceDate = invoiceDate,
                InvoiceNumber = invoiceNumber,
                Paid = paid,
                PaidDate = paidDate
            };
            var result = await InvoiceRepository.CreateInvoice(data);
            await eventSender.SendAsync("InvoiceCreated", result);
            return result;
        }
    }
}