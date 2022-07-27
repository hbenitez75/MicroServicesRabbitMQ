using Reporting.Data.Models;
namespace Reporting.GraphQL
{
    public class InvoiceItemType : ObjectType<InvoiceItem>
    {
        protected override void Configure(IObjectTypeDescriptor<InvoiceItem> descriptor)
        {
            descriptor.Field(ii => ii.Id).Type<IdType>();
            descriptor.Field(ii => ii.Description).Type<StringType>();
            descriptor.Field(ii => ii.InvoiceId).Type<IntType>();
            descriptor.Field(ii => ii.Qty).Type<IntType>();
            descriptor.Field(ii => ii.UnitPrice).Type<DecimalType>(); 
            descriptor.Field(ii => ii.Total).Type<DecimalType>();
            descriptor.Field<InvoiceResolver>(i => i.GetInvoice(default, default));
        }
    }
}