namespace WebClient.Pages.Invoicing
{
    public class InvoiceModel
    {
        public int Id { get; set; } 
        public string Description { set; get; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaidDate { get; set; }
        public double Amount { set; get; }
        public string InvoiceNumber { set; get; }
        public int Paid { set; get; }

    }
}
