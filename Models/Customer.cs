using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighPerformanceFraudDetectionSystem.Models;

namespace HighPerformanceFraudDetectionSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        [EmailAddress,Required]
        public string Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required, StringLength(50)]
        public string Country { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
