using System.Threading;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Commands;
using ApiProducts.Data.Repositories;
using MediatR;

namespace ApiProducts.Data.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductCommandRepository command;

    public ProductCreateCommandHandler(IProductCommandRepository command)
    {
        this.command = command;
    }
    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        return await command.Save(
            Product.Builder()
            .Description(request.Description)
            .Name(request.Name)
            .Sku(request.Sku)
            .Build());
    }
}