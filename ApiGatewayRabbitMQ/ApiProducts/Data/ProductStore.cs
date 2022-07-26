using System.Threading.Tasks;
using ApiDomain.Data;
using ApiDomain.Data.Entity;
using ApiDomain.Data.Repository;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiProducts.Data;

public class ProductStore : ICommandRepository<Product, int>
{
    private readonly DatabaseProperties properties;

    public ProductStore(DatabaseProperties properties)
    {
        this.properties = properties;
    }

    public async Task<Product> Save(Product entity)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        await connection.ExecuteAsync(
            $"INSERT INTO Product (Name, Sku, Description) VALUES ('{entity.Name}','{entity.Sku}','{entity.Description}');"); 
        return await connection.QueryFirstAsync<Product>("SELECT * FROM Product WHERE Id = last_insert_rowid();");
    }
    public async Task Update(Product entity)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        await connection.ExecuteAsync(
            $"UPDATE Product Name = '{entity.Name}', Sku = '{entity.Sku}', Description = '{entity.Description}' WHERE Id = {entity.Id};"); 
    }
    public async Task<bool> Delete(int id)
    {
        await using var connection = new SqliteConnection(properties.DataSource);
        var result = await connection.ExecuteAsync(
            $"DELETE FROM Product WHERE Id = {id};");

        return result == 1;
    }
}