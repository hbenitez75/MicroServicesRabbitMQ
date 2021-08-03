using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Services;

namespace ApiInvoices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUpdateTransactionInInvoices updateTransactionInInvoices;
        public TransactionController(IUpdateTransactionInInvoices _updateTransactionInInvoices)
        {
            updateTransactionInInvoices = _updateTransactionInInvoices;
        }
        [HttpGet]
        public async Task<IEnumerable<Models.Movements>> Get()
        {
           return await updateTransactionInInvoices.GetTransactions();
        }
    }
}
