namespace BSIMS.Core.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>(); 
        public ICollection<SupplierTransaction> SupplierTransactions { get; set; } = new List<SupplierTransaction>(); 
    }


}
