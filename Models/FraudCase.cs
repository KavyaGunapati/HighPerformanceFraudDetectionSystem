using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Models
{
    public class FraudCase
    {
        public int FraudCaseId { get; set; }
        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }
        public int FraudRuleId { get; set; }
        public FraudRule? FraudRule { get; set; } = null!;
        [Required]
        public DateTime DetectedDate { get; set; }
        public string? Remarks { get; set; }
        public FraudCaseStatus? Status { get; set; } = FraudCaseStatus.Added;

    }
        public enum FraudCaseStatus
        {
            Added,
            Modified,
            Deleted,
            Unchanged
        }
}
