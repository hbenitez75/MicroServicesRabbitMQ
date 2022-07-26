using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Queries;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductQueryHandler : IRequestHandler<ProductQuery, Product>
{
    private readonly ProductRepository repository;
    
    public ProductQueryHandler(ProductRepository repository)
    {
        this.repository = repository;
    }
    
    public async Task<Product> Handle(ProductQuery request, CancellationToken cancellationToken)
    {
        return await repository.Get(request.Id);
    }
}