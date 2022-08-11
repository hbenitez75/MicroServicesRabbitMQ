namespace ApiDomain.Data.Entity;

public class Inventory
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Amount { get; set; }

    public static InventoryBuilder Builder()
    {
        return new InventoryBuilder();
    }
    
    public class InventoryBuilder
    {
        private Inventory _inventory;
        public InventoryBuilder()
        {
            _inventory = new Inventory();
        }

        public InventoryBuilder Id(int id)
        {
            _inventory.Id = id;
            return this;
        }

        public InventoryBuilder ProductId(int productId)
        {
            _inventory.ProductId = productId;
            return this;
        }

        public InventoryBuilder Amount(decimal amount)
        {
            _inventory.Amount = amount;
            return this;
        }

        public Inventory Build()
        {
            return _inventory;
        }
    }
}