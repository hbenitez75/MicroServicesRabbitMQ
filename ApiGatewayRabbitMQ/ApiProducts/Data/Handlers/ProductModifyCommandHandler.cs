using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Commands;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductModifyCommandHandler : IRequestHandler<ProductModifyCommand, Product>
{
    
    public Task<Product> Handle(ProductModifyCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}