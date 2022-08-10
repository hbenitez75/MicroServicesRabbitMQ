using ApiDomain.Data.Entity;
using MediatR;

namespace ApiProducts.Data.Queries;

public class ProductQuery : IRequest<Product>
{
    public int Id { get; }

    public ProductQuery(int id)
    {
        Id = id;
    }
}