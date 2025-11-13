using HighPerformanceFraudDetectionSystem.Data;
using HighPerformanceFraudDetectionSystem.Models;
using HighPerformanceFraudDetectionSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Implementations
{
    public class FraudDetectionService : IFraudDetectionService
    {
        private readonly AppDbContext _context;
        public FraudDetectionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task ApplyRulesToTransactionAsync(Transaction transaction)
        {
            var fraudCases=await DetectFraudAsync(transaction);
            if (fraudCases.Any()) {
                transaction.Status = TransactionStatus.Flagged;
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CompileAndExecuteFraudRuleAsync(FraudRule fraudRule, Transaction transaction)
        {
            if (transaction.Customer == null) return false;

            var param = Expression.Parameter(typeof(Transaction), "t");
            Expression? conditon = null;
            if (fraudRule.ConditionExpression.Contains("Amount"))
            {
                var propAmount = Expression.Property(param, "Amount");
                var amount = Expression.Constant(5000m);
                var amountCheck = Expression.GreaterThan(propAmount, amount);
                conditon = amountCheck;
            }
            if (fraudRule.ConditionExpression.Contains("Country"))
            {
                var countryProp = Expression.Property(param, "Country");
                var country = Expression.Constant("US");
                var countryCheck = Expression.Equal(countryProp, country);
                conditon = conditon != null ? Expression.AndAlso(conditon, countryCheck) : countryCheck;
            }
            if (conditon == null) return false;

            var lambda = Expression.Lambda<Func<Transaction, bool>>(conditon, param);
            var complied = lambda.Compile();
            return complied(transaction);
        }

        public async Task<List<FraudCase>> DetectFraudAsync(Transaction transaction)
        {
            var fraudCases=new List<FraudCase>();
            var rules = await _context.FraudRules.Where(fr => fr.IsActive).ToListAsync();
            foreach (var rule in rules)
            {
                bool isFraud = await CompileAndExecuteFraudRuleAsync(rule, transaction);
                if(isFraud)
                {
                    var fraudcase = new FraudCase
                    {
                        TransactionId = transaction.TransactionId,
                        FraudRuleId = rule.FraudRuleId,
                        DetectedDate = DateTime.Now,
                        Status = FraudCaseStatus.Added,
                        Remarks = $"Rule Matched:{rule.RuleName}"
                    };
                   await _context.FraudCases.AddAsync(fraudcase);
                    fraudCases.Add(fraudcase);
                }
            }
              await  _context.SaveChangesAsync();
                return fraudCases;
        }

        public async Task<List<Transaction>> ProcessBulkTransactionsAsync(List<Transaction> transactions)
        {
            var tasks= transactions.Select(t=>ApplyRulesToTransactionAsync(t));
            await Task.WhenAll(tasks);
            return transactions;
        }
    }
}
