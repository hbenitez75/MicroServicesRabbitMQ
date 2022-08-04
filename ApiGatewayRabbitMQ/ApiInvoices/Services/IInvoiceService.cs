using ApiInvoices.Data;
using System.Threading.Tasks;

namespace ApiInvoices.Services
{
    public interface IInvoiceService
    {
        public Task Create(Invoice invoice);
    }
}
