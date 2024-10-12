using AutoMapper;
using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSIMS.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;
        private readonly ISupplierTransactionService _supplierTransactionService;

        public ProductService(IUnitOfWork uow, IMapper mapper, ISupplierTransactionService supplierTransactionService)
        {
            this.uow = uow;
            _mapper = mapper;
            _supplierTransactionService = supplierTransactionService;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                // Get the IQueryable<Product>
                var productsQuery = uow.Repository<Product>().GetAllAsync();

                // Use Include to eager load related entities
                var products = await productsQuery
                    .Include(p => p.SupplierProducts) // Eager load SupplierProducts
                    .ThenInclude(sp => sp.Supplier) // Eager load Supplier for each SupplierProduct
                    .ToListAsync() // Execute the query
                    ?? throw new KeyNotFoundException("No products found!");

                return products;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                throw new Exception("Failed to retrieve products.", ex);
            }
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await uow.Repository<Product>().GetByIdAsync(id)
                               ?? throw new KeyNotFoundException($"Product with ID {id} not found!");

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve product with ID {id}.", ex);
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            try
            {
                await uow.BeginTransactionAsync();

                await uow.Repository<Product>().AddAsync(product);

                // Update stock after adding the product
                int quantitySupplied = product.SupplierProducts?.Select(x => x.QuantitySupplied).FirstOrDefault() ?? 0;
                product.StockLevel += quantitySupplied;
                        

                var totalAmount = product.SupplierProducts.Sum(x => x.QuantitySupplied * x.CostPrice);
                await _supplierTransactionService.CreateSupplierTransactionAsync(product.SupplierProducts.Select(sp => sp.SupplierId).FirstOrDefault(), totalAmount);

                await uow.CommitAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception("Product addition failed.", ex);
            }
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            try
            {
                var existingProduct = await uow.Repository<Product>().GetByIdAsync(product.Id)
                                      ?? throw new KeyNotFoundException($"Product with ID {product.Id} doesn't exist!");

                // Update product properties
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;

                await uow.Repository<Product>().UpdateAsync(existingProduct);

                // Commit changes
                await uow.CommitAsync();

                return "Product updated successfully!";
            }
            catch (Exception ex)
            {
                throw new Exception("Product update failed.", ex);
            }
        }


        public async Task<string> DeleteProductAsync(int id)
        {
            try
            {
                await uow.BeginTransactionAsync();

                var product = await uow.Repository<Product>().GetByIdAsync(id)
                              ?? throw new KeyNotFoundException($"Product with ID {id} doesn't exist!");

                await uow.Repository<Product>().DeleteAsync(id);

                // Commit changes
                await uow.CommitAsync();

                return "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                uow.Rollback();
                throw new Exception("Product deletion failed.", ex);
            }
        }

        public async Task<string> UpdateProductStockAsync(int productId, int quantitySupplied, decimal costPrice)
        {
            try
            {
                var existingProduct = await uow.Repository<Product>().GetByIdAsync(productId)
                                      ?? throw new KeyNotFoundException($"Product with ID {productId} not found!");

                existingProduct.StockLevel += quantitySupplied;
                if(existingProduct.Price != costPrice)
                {
                    existingProduct.Price = costPrice;
                }

                await uow.Repository<Product>().UpdateAsync(existingProduct);

                // Commit changes
                await uow.CommitAsync();

                return $"Stock updated successfully! ProductId: {productId}";
            }
            catch (Exception ex)
            {
                throw new Exception("Stock update failed.", ex);
            }
        }
    }
}
