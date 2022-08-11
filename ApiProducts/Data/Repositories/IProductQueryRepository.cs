using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;

namespace ApiProducts.Data.Repositories;

public interface IProductQueryRepository : IQueryRepository<Product, int>
{
    
}