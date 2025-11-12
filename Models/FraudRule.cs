using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Models
{
   public class FraudRule
    {
        public int FraudRuleId { get; set; }
        [Required,StringLength(100)]
        public string? RuleName { get; set; }
        [Required]
        public string ConditionExpression { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public bool IsActive { get; set; }
        public ICollection<Transaction> Transactions { get; set; }= new List<Transaction>();
        public ICollection<FraudCase> FraudCases { get; set;} = new List<FraudCase>();
        public ICollection<TransactionFraudRule> TransactionFraudRules { get; set; } = new List<TransactionFraudRule>();
    }
}
