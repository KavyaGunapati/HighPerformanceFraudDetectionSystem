using HighPerformanceFraudDetectionSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighPerformanceFraudDetectionSystem.Services
{
    public interface IFraudCaseService
    {
        Task<FraudCase> AddFraudCaseAsync(FraudCase fraudCase);
        Task<FraudCase?> GetFraudCaseByIdAsync(int id);
        Task<List<FraudCase>> GetAllFraudCases();
        Task UpdateFraudCaseStatusAsync(int caseId, FraudCaseStatus status);
       
    }
}
