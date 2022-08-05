using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBillableTransaction.TransactionManager;
using Microsoft.AspNetCore.Authorization;

namespace ApiBillableTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {        
        public readonly ITransactionRepository transactionRepository;
        public TransactionController(ITransactionRepository _transactionRepository)
        {
            transactionRepository = _transactionRepository;

        }

        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Movements transaction)
        {

             await transactionRepository.Create(transaction);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            
            var grants =  from c in User.Claims select new { c.Type, c.Value };
            if (grants.Any() && grants.Where(x => x.Type == "arquitecto" && x.Value == "si").Any())
            {
                var transactions = await transactionRepository.GetAll();    
                return Ok(transactions);
            }
            else
                return Unauthorized();
           
            
        }

        [HttpPut]
        public async Task Put([FromBody] Movements transaction)
        {
            await transactionRepository.Update(transaction);
        }


    }
}
