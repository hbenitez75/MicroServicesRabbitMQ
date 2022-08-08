using ApiDomain.Data.Entity;
using MediatR;

namespace ApiProducts.Data.Commands;

public class ProductCreateCommand : IRequest<Product>
{
    public string Sku { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}