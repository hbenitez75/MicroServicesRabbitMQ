using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Data;
using ApiInvoices.InvoiceManager;
using ApiInvoices.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiInvoices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicingController : ControllerBase
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IGenerateInvoices generateInvoices;
        private readonly IInvoiceService invoiceService;
        public InvoicingController(IInvoiceRepository _invoiceRepository, IGenerateInvoices _generateInvoices, IInvoiceService _invoiceService)
        {
            invoiceRepository = _invoiceRepository;
            generateInvoices = _generateInvoices;
            invoiceService = _invoiceService;
        }

        [HttpPut]     
        [Authorize]
        public async Task Put([FromBody]  Invoice invoice)
        {
            await invoiceRepository.Update(invoice);
        }
        
        [HttpGet]
        [Route("GetByRange")]
        [Authorize]
        public async Task<IEnumerable<Invoice>> GetByRange( DateTime from,DateTime to  )
        {
            
            return await generateInvoices.GetInvoicesByRange(from, to);
        }
        
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Invoice invoice)
        {
            await invoiceService.Create(invoice);
        }

        [HttpGet]
        [Route("Get")]
        [Authorize]
        public  async Task<IEnumerable<Invoice>> Get()
        {
             return await invoiceRepository.GetInvoices();
            
        }

        
    }
}
