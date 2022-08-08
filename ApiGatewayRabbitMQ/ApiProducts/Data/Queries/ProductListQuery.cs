using System.Collections.Generic;
using ApiDomain.Data.Entity;
using MediatR;

namespace ApiProducts.Data.Queries;

public class ProductListQuery : IRequest<IEnumerable<Product>>
{
    public ProductListQuery()
    {
        
    }
}