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
    public class FraudRuleService : IFraudRuleService
    {
        private readonly AppDbContext _context;
        public FraudRuleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<FraudRule> AddFraudRuleAsync(FraudRule fraudRule)
        {
            await _context.FraudRules.AddAsync(fraudRule);
            await _context.SaveChangesAsync();
            return fraudRule;
        }

        public async Task DeleteFraudRuleAsync(int id)
        {
            var rule=await _context.FraudRules.FindAsync(id);
            if (rule != null)
            {
                _context.FraudRules.Remove(rule);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FraudRule>> GetAllFraudRulesAsync()
        {
           return await _context.FraudRules.Include(t=>t.Transactions).ToListAsync();
        }

        public Task<FraudRule?> GetFraudRuleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFraudRuleAsync(FraudRule fraudRule)
        {
            throw new NotImplementedException();
        }
    }
}
