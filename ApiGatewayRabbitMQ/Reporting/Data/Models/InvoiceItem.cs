using HotChocolate.AspNetCore.Authorization;
namespace Reporting.Data.Models

{
    [Authorize]
    public class InvoiceItem
    {
        private decimal total;

        [Authorize]
        public int Id { get; set; }
        [Authorize]
        public string Description { get; set; } = "";
        [Authorize]
        public int InvoiceId { get; set; }
        [Authorize]
        public int Qty { get; set; }
        [Authorize]
        public decimal UnitPrice  { get; set; }

        [Authorize]
        public decimal Total
        {
            get { return Qty * UnitPrice; }
        }
    }
}
