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
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context) {
            _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
          await  _context.Customers.AddAsync(customer);
           await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Include(c=>c.Transactions).ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.Include(c=>c.Transactions).FirstOrDefaultAsync(c=>c.CustomerId== id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            
        }
    }
}
