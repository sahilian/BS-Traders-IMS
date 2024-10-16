using AutoMapper;
using BSIMS.Application.DTOs;
using BSIMS.Application.Services;
using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSIMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDto);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _productService.GetProductByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }
            var product = _mapper.Map<Product>(productDto);
            await _productService.UpdateProductAsync(product);

            return NoContent(); // 204 No Content
        }

        [HttpPut("{productId}/stock")]
        public async Task<IActionResult> UpdateProductStock(int productId, [FromBody] int quantitySupplied, decimal costPrice)
        {
            if (quantitySupplied <= 0)
            {
                return BadRequest("Quantity supplied must be greater than zero.");
            }

            try
            {
                await _productService.UpdateProductStockAsync(productId, quantitySupplied, costPrice);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // 404 Not Found if product is not found
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
