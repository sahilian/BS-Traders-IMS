namespace BSIMS.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockLevel { get; set; }
        public decimal Price { get; set; } // Sales Price
        public decimal? Discount { get; set; }

        // Link to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>(); // Link to suppliers
    }
}

