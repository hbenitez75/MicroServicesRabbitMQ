namespace WebClient.Pages.Transaction
{
    public class TransactionModel
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public DateTime BillDate { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; } 
        public DateTime TransactionDate { set; get; }
        public string InvoiceNumber { get; set; }   

    }
}
