using System.ComponentModel.DataAnnotations;

namespace BSIMS.Core.Entities
{
    public class SaleItem
    {
        [Key]
        public int SaleId { get; set; }
        public Sale Sale { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
