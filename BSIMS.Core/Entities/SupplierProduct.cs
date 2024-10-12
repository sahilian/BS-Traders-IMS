namespace BSIMS.Core.Entities
{
    public class SupplierProduct
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal CostPrice { get; set; } // Cost price from supplier
        public int QuantitySupplied { get; set; }
    }

}