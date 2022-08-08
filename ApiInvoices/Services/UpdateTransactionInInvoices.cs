using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Models;
using ApiInvoices.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.Extensions.Options;

namespace ApiInvoices.Services
{
    public class UpdateTransactionInInvoices :IUpdateTransactionInInvoices
    {
        private DataBaseName dataBaseName;
        public UpdateTransactionInInvoices(DataBaseName _dataBaseName)
        {
            dataBaseName = _dataBaseName;
        }

        public async Task  UpdateTransaction(Movements transaction)
        {
            using var connection = new SqliteConnection(dataBaseName.Name);
            var result = await connection.ExecuteAsync("INSERT INTO Movements(Description,BillDate,Amount,Status,TransactionDate,InvoiceNumber) " +
                         "VALUES(@Description, @BillDate, @Amount, @Status, @TransactionDate, @InvoiceNumber);", transaction);

        }

        public async Task<IEnumerable<Movements>> GetTransactions()
        { 
            using var connection = new SqliteConnection(dataBaseName.Name);
            var result = await connection.QueryAsync<Movements>("SELECT * FROM Movements; ");
            return result.ToList();
        }
    }
}

