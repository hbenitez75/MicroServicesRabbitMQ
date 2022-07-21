namespace Reporting.Data.Models
{
    public class Invoice
    {
        public int Id { set; get; }
        public string Description { set; get; } = string.Empty;
        public DateTime InvoiceDate { set; get; }
        public DateTime PaidDate { set; get; }
        public Double Amount { set; get; }
        public string InvoiceNumber { set; get; } = string.Empty;
        public int Paid { set; get; }

    }
}
