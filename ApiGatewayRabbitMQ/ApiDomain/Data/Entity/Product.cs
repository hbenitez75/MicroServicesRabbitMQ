namespace ApiDomain.Data.Entity;

public class Product
{
    public int Id { get; set; }
    public string? Sku { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public static ProductBuilder Builder()
    {
        return new ProductBuilder();
    }
    public static Product NewProduct()
    {
        var product = new ProductBuilder();
        product
            .Id(0)
            .Description("Default description")
            .Name("Default")
            .Sku(Guid.NewGuid().ToString());
        return product.Build();
    }
    
    public class ProductBuilder
    {
        private Product _product;
        public ProductBuilder()
        {
            _product = new Product();
        }
        public ProductBuilder Id(int id)
        {
            _product.Id = id;
            return this;
        }
        public ProductBuilder Sku(string sku)
        {
            _product.Sku = sku;
            return this;
        }
        public ProductBuilder Name(string name)
        {
            _product.Name = name;
            return this;
        }
        public ProductBuilder Description(string description)
        {
            _product.Description = description;
            return this;
        }
        public Product Build()
        {
            return _product;
        }
    }
}

public class Sample
{
    public Sample()
    {
        var product = Product.NewProduct();
    }
}