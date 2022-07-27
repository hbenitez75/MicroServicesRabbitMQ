using Reporting.Data.Models;
namespace Reporting.GraphQL
{
    public class InvoiceType : ObjectType<Invoice>
    {
        protected override void Configure(IObjectTypeDescriptor<Invoice> descriptor)
        {
            descriptor.Field(i => i.Id).Type<IdType>();
            descriptor.Field(i => i.Description).Type<StringType>();
            descriptor.Field(i => i.InvoiceDate).Type<DateTimeType>(); 
            descriptor.Field(i => i.PaidDate).Type<DateTimeType>();
            descriptor.Field(i => i.Amount).Type<FloatType>();
            descriptor.Field(i => i.InvoiceNumber).Type<StringType>();
            descriptor.Field(i => i.Paid).Type<IntType>();
            descriptor.Field<InvoiceItemResolver>(ii => ii.GetInvoiceItems(default, default));
        }
    }
}