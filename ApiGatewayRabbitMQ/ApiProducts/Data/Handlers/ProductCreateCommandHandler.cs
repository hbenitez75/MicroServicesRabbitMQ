using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Commands;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    public Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}