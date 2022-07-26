using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Queries;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductListQueryHandler : IRequestHandler<ProductListQuery, IEnumerable<Product>>
{
    private readonly ProductRepository repository;

    public ProductListQueryHandler(ProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Product>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAll();
    }
}