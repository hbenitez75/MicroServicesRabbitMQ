using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiDomain.Data;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiProducts.Data;

public abstract class ProductRepository : IQueryRepository<Product, int>
{
    private readonly DatabaseProperties properties;

    protected ProductRepository(DatabaseProperties properties)
    {
        this.properties = properties;
    }
    public async Task<Product> Get(int id)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        return await connection.QueryFirstAsync<Product>($"SELECT * FROM Product WHERE id = {id};");
    } 

    public async Task<IEnumerable<Product>> GetAll()
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        return await connection.QueryAsync<Product>("SELECT * FROM Product;");
    }

    public Task<IEnumerable<Product>> Find(Func<Product, bool> predicate)
    {
        throw new NotImplementedException();
    }
}