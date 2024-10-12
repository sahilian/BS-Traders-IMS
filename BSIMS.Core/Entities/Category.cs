using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSIMS.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property for products under this category
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

