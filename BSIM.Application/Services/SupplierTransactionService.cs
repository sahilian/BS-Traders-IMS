using BSIMS.Core.Entities;
using BSIMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSIMS.Application.Services
{
    internal class SupplierTransactionService : ISupplierTransactionService
    {
        private readonly IUnitOfWork uow;
        public SupplierTransactionService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        //Create a supplier transaction when products are supplied
        public async Task<string> CreateSupplierTransactionAsync(int supplierId, decimal amount)
        {
            try
            {
                var transaction = new SupplierTransaction
                {
                    SupplierId = supplierId,
                    Amount = amount,
                    PendingAmount = amount,
                    TransactionDate = DateTime.UtcNow,
                    IsPaid = false
                };

                await uow.Repository<SupplierTransaction>().AddAsync(transaction);

                return $"Supplier transaction created successfully for SupplierId: {supplierId}, Amount: {amount}";
            }
            catch (Exception ex)
            {
                // You can log the exception here using a logging service
                return $"Failed to create supplier transaction. Error: {ex.Message}";
            }
        }



        // Update transaction when payment is made
        public async Task<string> UpdateSupplierTransactionAsync(int transactionId, decimal paymentAmount)
        {
            try
            {
                var transaction = await uow.Repository<SupplierTransaction>().GetByIdAsync(transactionId)
                                  ?? throw new KeyNotFoundException("Transaction not found");

                transaction.PendingAmount -= paymentAmount;

                if (transaction.PendingAmount <= 0)
                {
                    transaction.PendingAmount = 0;
                    transaction.IsPaid = true;
                }

                await uow.Repository<SupplierTransaction>().UpdateAsync(transaction);
                await uow.CommitAsync();

                return $"Supplier transaction updated successfully. Payment: {paymentAmount}, Remaining Pending Amount: {transaction.PendingAmount}";
            }
            catch (KeyNotFoundException ex)
            {
                return $"Supplier transaction not found. Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Failed to update supplier transaction. Error: {ex.Message}";
            }
        }
    }
}
