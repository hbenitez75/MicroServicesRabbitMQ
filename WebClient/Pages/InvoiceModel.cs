namespace WebClient.Pages
{
    public class InvoiceModel
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public DateTime InvoiceDate { set; get; } 
        public DateTime PaidDate { set; get; }
        public Double Amount { set; get; }
        public string InvoiceNumber { set; get; }
        public int Paid { set; get; } 
    }
}
