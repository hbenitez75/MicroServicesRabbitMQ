using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;

namespace ApiProducts.Data.Repositories;

public interface IProductCommandRepository : ICommandRepository<Product, int>
{
    
}