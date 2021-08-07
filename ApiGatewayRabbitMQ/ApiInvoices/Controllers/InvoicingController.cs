using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Data;
using ApiInvoices.InvoiceManager;
using ApiInvoices.Services;


namespace ApiInvoices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicingController : ControllerBase
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IGenerateInvoices generateInvoices;
        public InvoicingController(IInvoiceRepository _invoiceRepository,
            IGenerateInvoices _generateInvoices)
        {
            invoiceRepository = _invoiceRepository;
            generateInvoices = _generateInvoices;
        }
        
        [HttpPut]     
        public async Task Put([FromBody]  Invoice invoice)
        {
            await invoiceRepository.Update(invoice);
        }
        
        [HttpGet]
        [Route("GetByRange")]
        public async Task<IEnumerable<Invoice>> GetByRange( DateTime from,DateTime to  )
        {
            
            return await generateInvoices.GetInvoicesByRange(from, to);
        }
        
        [HttpPost]
        public async Task Post([FromBody] Invoice invoice)
        {
            await invoiceRepository.Create(invoice);
        }

        [HttpGet]
        [Route("Get")]
        public  async Task<IEnumerable<Invoice>> Get()
        {
             return await invoiceRepository.GetInvoices();
            
        }

        
    }
}
