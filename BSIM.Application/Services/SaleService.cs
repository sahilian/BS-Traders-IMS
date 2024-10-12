using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSIMS.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;

        public SaleService(IGenericRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            return _saleRepository.GetAllAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _saleRepository.GetByIdAsync(id);
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _saleRepository.AddAsync(sale);
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            await _saleRepository.UpdateAsync(sale);
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteAsync(id);
        }
    }
}
