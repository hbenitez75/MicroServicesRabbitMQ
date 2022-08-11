
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiInvoices.Data;
using ApiInvoices.InvoiceManager;
using ApiInvoices.Messaging;

namespace ApiInvoices.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;
        private ISendInvoiceMsg sendInvoiceMsg;
        public InvoiceService(IInvoiceRepository _invoiceRepository, ISendInvoiceMsg _sendInvoiceMsg)
        {
            invoiceRepository = _invoiceRepository;
            sendInvoiceMsg = _sendInvoiceMsg;
        }
        public async Task Create(Invoice invoice)
        {
            var numberOfAffectedRows = await invoiceRepository.Create(invoice);
            if (numberOfAffectedRows > 0)
            {
                sendInvoiceMsg.SendMsg(invoice);
            }
        }
    }
}
