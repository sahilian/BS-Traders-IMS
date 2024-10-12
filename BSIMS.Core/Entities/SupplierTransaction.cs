namespace BSIMS.Core.Entities
{
    public class SupplierTransaction
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public decimal Amount { get; set; }
        public decimal PendingAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
