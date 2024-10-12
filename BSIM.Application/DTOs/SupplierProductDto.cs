using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSIMS.Application.DTOs
{
    public class SupplierProductDto
    {
        public int SupplierId { get; set; }
        public decimal CostPrice { get; set; }
        public int QuantitySupplied { get; set; }
    }
}
