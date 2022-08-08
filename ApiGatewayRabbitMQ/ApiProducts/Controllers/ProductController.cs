using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;
using ApiProducts.Data.Commands;
using ApiProducts.Data.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiProducts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Route("all")]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await mediator.Send(new ProductListQuery());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<Product> GetProduct([FromRoute] int id)
    {
        return await mediator.Send(new ProductQuery(id));
    }
    
    [HttpPost]
    public async Task<Product> CreateProduct([FromBody] ProductCreateCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
    
    [HttpPut]
    public async Task<Product> ModifyProduct([FromBody] ProductModifyCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
}