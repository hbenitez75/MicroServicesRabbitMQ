using HotChocolate.AspNetCore.Authorization;

namespace Reporting.Data.Models
{
    [Authorize]
    public class Invoice
    {
        [Authorize]
        public int Id { set; get; }
        [Authorize]
        public string Description { set; get; } = string.Empty;
        [Authorize]
        public DateTime InvoiceDate { set; get; }
        [Authorize]
        public DateTime PaidDate { set; get; }
        [Authorize]
        public Double Amount { set; get; }
        [Authorize]
        public string InvoiceNumber { set; get; } = string.Empty;
        [Authorize]
        public int Paid { set; get; }

    }
}
