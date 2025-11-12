using HighPerformanceFraudDetectionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Services
{
    internal interface IFraudRuleService
    {
        Task<FraudRule> AddFraudRuleAsync(FraudRule fraudRule);
        Task<FraudRule?> GetFraudRuleByIdAsync(int id);
        Task<List<FraudRule>> GetAllFraudRulesAsync();
        Task UpdateFraudRuleAsync(FraudRule fraudRule);
        Task DeleteFraudRuleAsync(int id);
    }
}
