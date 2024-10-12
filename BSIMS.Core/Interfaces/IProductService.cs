using BSIMS.Core.Entities;

namespace BSIMS.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<string> UpdateProductAsync(Product product);
        Task<string> DeleteProductAsync(int id);
        Task<string> UpdateProductStockAsync(int productId, int quantitySupplied, decimal costPrice);
    }
}
