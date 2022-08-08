using System.Linq;
using ApiDomain.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiProducts.Data;

public class DatabaseCreate : IDatabaseCreate
{
    private readonly DatabaseProperties databaseName;
    
    public DatabaseCreate(DatabaseProperties databaseName)
    {
        this.databaseName = databaseName;
    }

    public void Setup()
    {
        var connection = new SqliteConnection(databaseName.DataSource);
        string query = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Product';";
        var invoiceTable = connection.Query<string>(query);
        var invoiceTableName = invoiceTable.FirstOrDefault();
        if (!(!string.IsNullOrEmpty(invoiceTableName) && invoiceTableName == "Product"))
        {
            connection.ExecuteAsync("create table Product (" +
                                    "Id integer not null  constraint Product_pk primary key autoincrement," +
                                    "Sku varchar(50) not null, " +
                                    "Name varchar(50) not null, " +
                                    "Description varchar(100));" +
                                    "create unique index Product_Sku_uindex on Product (Sku);");
        }

        var table = connection.Query<string>("SELECT name FROM sqlite_master " +
                                             "WHERE type='table' AND name = 'InventoryStock';");
        var tableName = table.FirstOrDefault();
        
        if (!string.IsNullOrEmpty(tableName) && tableName == "InventoryStock")
            return;

        connection.Execute("create table InventoryStock (" +
                           "Id integer constraint InventoryStock_pk primary key autoincrement, " +
                           "ProductId integer references Product not null, " +
                           "Amount numeric default 0);");
    }
}