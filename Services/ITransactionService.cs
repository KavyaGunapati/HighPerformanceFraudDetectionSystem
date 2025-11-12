using HighPerformanceFraudDetectionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Services
{
    public interface ITransactionService
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction?> GetTransactionByIdAsync(int transactionId);
        Task<List<Transaction>> GetTransactionsByCustomerAsync(int customerId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetSuspiciousTransactionsAsync(decimal threshold);
        Task UpdateTransactionStatusAsync(int transactionId, TransactionStatus status);
    }
}
