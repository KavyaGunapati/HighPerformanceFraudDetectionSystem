using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Models
{
   public class TransactionFraudRule
    {
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        public int FraudRuleId { get; set; }
        public FraudRule? FraudRule { get; set; } = null;
        public decimal? ConfidenceScore { get; set; }
        public DateTime? DetectedAt { get; set; } = DateTime.Now;
    }
}
