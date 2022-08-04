using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDomain.Data;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Memory;

namespace ApiProducts.Data;

public abstract class ProductRepository : IQueryRepository<Product, int>
{
    private const string CachedListKey = "products"; 
    private readonly DatabaseProperties properties;
    private readonly IMemoryCache memoryCache;

    protected ProductRepository(DatabaseProperties properties, IMemoryCache memoryCache)
    {
        this.properties = properties;
        this.memoryCache = memoryCache;
    }
    public async Task<Product> Get(int id)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        return await connection.QueryFirstAsync<Product>($"SELECT * FROM Product WHERE id = {id};");
    } 

    public async Task<IEnumerable<Product>> GetAll()
    {
        var output = await GetAllCached();
        if (output is not null)
        {
            return output;
        }
        
        await using var connection = new SqliteConnection(properties.DataSource);
        output = await connection.QueryAsync<Product>("SELECT * FROM Product;");
        
        var enumerable = output as Product[] ?? output.ToArray();
        memoryCache.Set(CachedListKey, enumerable, TimeSpan.FromMinutes(1));

        return enumerable;
    }

    private Task<IEnumerable<Product>> GetAllCached()
    {
        var output = memoryCache.Get<IEnumerable<Product>>(CachedListKey);
        return Task.FromResult(output);
    }
}