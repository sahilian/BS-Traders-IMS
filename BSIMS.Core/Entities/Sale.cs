namespace BSIMS.Core.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; } // Products sold in the transaction
        public decimal TotalAmount { get; set; }
        public bool IsCreditSale { get; set; } // Credit or cash sale
    }

}
