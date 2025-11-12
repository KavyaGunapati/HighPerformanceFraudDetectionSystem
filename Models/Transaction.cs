using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string? Location { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public ICollection<FraudRule> FraudRules { get; set; }= new List<FraudRule>();
        public ICollection<FraudCase> FraudCases { get; set; }= new List<FraudCase>();
        public ICollection<TransactionFraudRule> TransactionFraudRules { get; set; }=new List<TransactionFraudRule>();
    }
        public enum TransactionStatus
        {
            Pending,
           Processed,
           Flagged
        } 
}
