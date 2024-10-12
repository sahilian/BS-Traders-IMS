using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSIMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            var sales = await _saleService.GetSalesAsync();
            return Ok(sales);
        }

        // GET: api/sale/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSaleById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        // POST: api/sale
        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale sale)
        {
            if (sale == null)
                return BadRequest("Sale cannot be null");

            await _saleService.AddSaleAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
        }

        // PUT: api/sale/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] Sale sale)
        {
            if (sale == null || id != sale.Id)
                return BadRequest("Sale ID mismatch or sale data is null");

            await _saleService.UpdateSaleAsync(sale);
            return NoContent();
        }

        // DELETE: api/sale/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
                return NotFound();

            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}
