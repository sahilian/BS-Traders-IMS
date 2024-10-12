using AutoMapper;
using BSIMS.Application.DTOs;
using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSIMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        // GET: api/supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetSuppliersAsync();
            return Ok(suppliers);
        }

        // GET: api/supplier/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        // POST: api/supplier
        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateSupplier([FromBody] SupplierDto supplierDto)
        {
            if (supplierDto == null)
                return BadRequest("Supplier cannot be null");
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierService.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Id }, supplier);
        }

        // PUT: api/supplier/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (supplier == null || id != supplier.Id)
                return BadRequest("Supplier ID mismatch or supplier data is null");

            await _supplierService.UpdateSupplierAsync(supplier);
            return NoContent();
        }

        // DELETE: api/supplier/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();

            await _supplierService.DeleteSupplierAsync(id);
            return NoContent();
        }
    }
}
