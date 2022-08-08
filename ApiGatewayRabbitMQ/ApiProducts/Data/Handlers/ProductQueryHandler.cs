using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Queries;
using ApiProducts.Data.Repositories;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductQueryHandler : IRequestHandler<ProductQuery, Product>
{
    private readonly IProductQueryRepository queryRepository;
    
    public ProductQueryHandler(IProductQueryRepository queryRepository)
    {
        this.queryRepository = queryRepository;
    }
    
    public async Task<Product> Handle(ProductQuery request, CancellationToken cancellationToken)
    {
        return await queryRepository.Get(request.Id);
    }
}