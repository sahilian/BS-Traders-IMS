using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSIMS.Core.Interfaces
{
    public interface ISupplierTransactionService
    {
        Task<string> CreateSupplierTransactionAsync(int supplierId, decimal amount);
        Task<string> UpdateSupplierTransactionAsync(int transactionId, decimal paymentAmount);

    }
}
