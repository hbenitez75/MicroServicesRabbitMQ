using ApiInvoices.Data;

namespace ApiInvoices.Messaging
{
    public interface ISendInvoiceMsg
    {
        public void SendMsg(Invoice invoice);
    }
}
