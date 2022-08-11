using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiDomain.Data;
using ApiDomain.Data.Entity;
using ApiProducts.Data.Cache;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiProducts.Data.Repositories;

public class ProductQueryRepository : IProductQueryRepository
{
    private const string CachedListKey = "products";
    private readonly DatabaseProperties properties;
    private readonly IDistributedCache cache;

    public ProductQueryRepository(DatabaseProperties properties, IDistributedCache cache)
    {
        this.properties = properties;
        this.cache = cache;
    }
    
    public async Task<Product> Get(int index)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        return await connection.QueryFirstAsync<Product>($"SELECT * FROM Product WHERE id = {index};");
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        var output = await cache.GetRecordAsync<IEnumerable<Product>>(CachedListKey);
        if (output is not null)
            return output;
        
        await using var connection = new SqliteConnection(properties.DataSource);
        output = (await connection.QueryAsync<Product>("SELECT * FROM Product;")).ToList();
        
        await cache.SetRecordAsync(CachedListKey, output);

        return output;
    }
}