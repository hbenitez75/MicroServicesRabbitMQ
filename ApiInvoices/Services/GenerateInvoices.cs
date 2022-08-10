using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiInvoices.Data;
using ApiInvoices.InvoiceManager;
using Dapper;
using Microsoft.Data.Sqlite;
using ApiInvoices.Models;
using System.Globalization;

namespace ApiInvoices.Services
{
    public class GenerateInvoices :IGenerateInvoices
    {
        public readonly DataBaseName dataBaseName;
        public readonly IInvoiceRepository invoiceRepository;
        public GenerateInvoices(DataBaseName _databaseName,IInvoiceRepository _invoiceRepository)
        {
            dataBaseName = _databaseName;
            invoiceRepository = _invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesByRange(DateTime from, DateTime to )
        {
            
            var connection = new SqliteConnection(dataBaseName.Name);
            string guid = Guid.NewGuid().ToString("D");
            var transactionstoBill= await connection.QueryAsync<Movements>(@"SELECT rowId as Id, Description, BillDate, Amount, Status, InvoiceNumber " +
               " FROM Movements WHERE  TransactionDate < @TransactionDateTo "+
               "AND TransactionDate > @TransactionDateFrom ", new {TransactionDateTo = to,TransactionDateFrom = from});
            int[] ids = (from x in transactionstoBill select x.Id).ToArray();
            foreach(int id in ids)
            { 
               await connection.ExecuteAsync(@"UPDATE Movements SET InvoiceNumber = @InvNumber WHERE rowId =@Id ", new { Id = id,
                                                                                                                         InvNumber = guid   });
            }
            
            var invoices = connection.QueryAsync<Invoice>("SELECT rowid as Id,Amount,Description FROM Movements " +
                           "WHERE InvoiceNumber = @InvNumber;",new {
                                 InvNumber = guid 
                           });
            var totalInvoice = invoices.Result.Sum(x => x.Amount);
            
            Invoice invoice = new Invoice{
                                           Amount = totalInvoice,Description = "New Invoice",
                                           InvoiceNumber =guid,InvoiceDate = DateTime.Now 
            };
            
            await invoiceRepository.Create(invoice);
            var list = new List<Invoice>();
            list.Add(invoice);
            return list.AsEnumerable();

        }
    }
}
