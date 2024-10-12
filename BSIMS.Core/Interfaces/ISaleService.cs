using BSIMS.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSIMS.Core.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task AddSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task DeleteSaleAsync(int id);
    }
}
