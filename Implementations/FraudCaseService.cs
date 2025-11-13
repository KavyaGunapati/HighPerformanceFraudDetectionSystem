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
    public class FraudCaseService : IFraudCaseService
    {
        private readonly AppDbContext _context;
        public FraudCaseService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<FraudCase> AddFraudCaseAsync(FraudCase fraudCase)
        {
            await _context.FraudCases.AddAsync(fraudCase);
            await _context.SaveChangesAsync();
            return fraudCase;
        }

        public async Task<List<FraudCase>> GetAllFraudCases()
        {
            return await _context.FraudCases.Include(fc=>fc.Transaction).Include(fc=>fc.FraudRule).ToListAsync();
        }

        public Task<FraudCase?> GetFraudCaseByIdAsync(int id)
        {
            return _context.FraudCases.Include(fc=>fc.Transaction).Include(fc=> fc.FraudRule).FirstOrDefaultAsync(fc=>fc.FraudCaseId==id);
        }

        public async Task UpdateFraudCaseStatusAsync(int caseId, FraudCaseStatus status)
        {
            var fraudCase=await _context.FraudCases.FirstOrDefaultAsync(fc=>fc.FraudCaseId == caseId);
            if (fraudCase != null)
            {
                fraudCase.Status = status;
                _context.FraudCases.Update(fraudCase);
                _context.SaveChangesAsync();
            }
        }
    }
}
