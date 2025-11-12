using HighPerformanceFraudDetectionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Services
{
    public interface IFraudDetectionService
    {
        Task<List<FraudCase>> DetectFraudAsync(FraudCase fraudCase);
        Task ApplyRulesToTransactionAsync(Transaction transaction);
        Task<List<Transaction>> ProcessBulkTransactionsAsync(List<Transaction> transactions);
        Task<bool> CompileAndExecuteFraudRuleAsync(FraudRule fraudRule);
    }
}
