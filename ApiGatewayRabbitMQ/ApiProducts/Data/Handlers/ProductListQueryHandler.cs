using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Queries;
using ApiProducts.Data.Repositories;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductListQueryHandler : IRequestHandler<ProductListQuery, IEnumerable<Product>>
{
    private readonly IProductQueryRepository queryRepository;

    public ProductListQueryHandler(IProductQueryRepository queryRepository)
    {
        this.queryRepository = queryRepository;
    }

    public async Task<IEnumerable<Product>> Handle(ProductListQuery request, CancellationToken cancellationToken)
    {
        return await queryRepository.GetAll();
    }
}