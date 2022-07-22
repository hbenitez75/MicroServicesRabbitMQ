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
        //[Authorize]
        public async Task Post([FromBody] Movements transaction)
        {

             await transactionRepository.Create(transaction);
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Movements>> Get()
        {
            //var grants = JsonResult(from c in User.Claims select new { c.Type, c.Value });
            return await transactionRepository.GetAll();
        }

        [HttpPut]
        public async Task Put([FromBody] Movements transaction)
        {
            await transactionRepository.Update(transaction);
        }


    }
}
