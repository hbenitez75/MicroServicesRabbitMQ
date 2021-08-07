using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Data;

namespace ApiInvoices.Services
{
    public interface IGenerateInvoices
    {
        public Task<IEnumerable<Invoice>> GetInvoicesByRange(DateTime from, DateTime to);
    }
}
