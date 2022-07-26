using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;
using ApiProducts.Data.Commands;
using ApiProducts.Data.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiProducts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductController(IQueryRepository<Product, int> productRepository, IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await mediator.Send(new ProductListQuery());
    }
    
    [HttpGet("/{id:int}")]
    public async Task<Product> GetProduct([FromRoute] int id)
    {
        var query = new ProductQuery(id);
        var result = await mediator.Send(query);

        return result;
    }
    
    [HttpPost]
    public async Task<Product> CreateProduct([FromBody] ProductCreateCommand command)
    {
        var result = await mediator.Send(command);
        return result;
    }
}