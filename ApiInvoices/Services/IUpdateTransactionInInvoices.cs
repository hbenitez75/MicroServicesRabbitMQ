using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Models;

namespace ApiInvoices.Services
{
    public interface IUpdateTransactionInInvoices
    {
        public Task UpdateTransaction(Movements transaction);
        public Task<IEnumerable<Movements>> GetTransactions();
    }
}
