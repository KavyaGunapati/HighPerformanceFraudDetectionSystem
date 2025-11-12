using HighPerformanceFraudDetectionSystem.Data;
using HighPerformanceFraudDetectionSystem.Models;
using HighPerformanceFraudDetectionSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        public TransactionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Include(t=>t.Customer).ToListAsync();
        }

        public async Task<List<Transaction>> GetSuspiciousTransactionsAsync(decimal threshold)
        {
            return await _context.Transactions.Where(t=>t.Amount>threshold).OrderByDescending(t=>t.Amount).Include(t=>t.Customer).ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int transactionId)
        {
            return await _context.Transactions.Include(t => t.Customer).Include(t => t.FraudCases).FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<List<Transaction>> GetTransactionsByCustomerAsync(int customerId)
        {
            return await _context.Transactions.Where(t=>t.CustomerId==customerId).Include(t=>t.Customer).ToListAsync();
        }

        public async Task UpdateTransactionStatusAsync(int transactionId, TransactionStatus status)
        {
            var transaction=await _context.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                transaction.Status = status;
                _context.Transactions.Update(transaction);
               await _context.SaveChangesAsync();
            }
        }
    }
}
