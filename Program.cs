using HighPerformanceFraudDetectionSystem.Data;
using HighPerformanceFraudDetectionSystem.Implementations;
using HighPerformanceFraudDetectionSystem.Models;
using HighPerformanceFraudDetectionSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var service = new ServiceCollection();
        service.AddDbContext<AppDbContext>();
        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<IFraudCaseService, FraudCaseService>();
        service.AddScoped<ITransactionService, TransactionService>();
        service.AddScoped<IFraudRuleService,FraudRuleService>();
        service.AddScoped<IFraudDetectionService, FraudDetectionService>();
        var provider = service.BuildServiceProvider();
        var customerService=provider.GetRequiredService<ICustomerService>();
        var transactionService=provider.GetRequiredService<ITransactionService>();
        var fraudCaseService=provider.GetRequiredService<IFraudCaseService>();
        var fraudRuleService = provider.GetRequiredService<IFraudRuleService>();
        var fraudDetectionService = provider.GetRequiredService<IFraudDetectionService>();
        ////Add customer
        //var customer = await customerService.AddCustomerAsync(new Customer
        //{

        //    CustomerName = "John Doe",
        //    Email = "john@example.com",
        //    Country = "US"


        //});
        //Console.WriteLine($"Customer added: {customer.CustomerName}");
        //var customer1 = await customerService.AddCustomerAsync(new Customer
        //{
        //    CustomerName = "Alice",
        //    Email = "alice@example.com",
        //    Country = "US"
        //});

        var customer2 = await customerService.AddCustomerAsync(new Customer
        {
            CustomerName = "Boblue",
            Email = "boblue@example.com",
            Country = "Us"
        });

        //Console.WriteLine($"Added customers: {customer1.CustomerName}, {customer2.CustomerName}");

        //var transaction = await transactionService.AddTransactionAsync(new Transaction
        //{
        //    CustomerId = customer.CustomerId,
        //    Amount = 6000,
        //    TransactionDate = DateTime.Now.AddMinutes(-10),
        //    Location = "New York"
        //});
        //Console.WriteLine($"Transaction added: {transaction.TransactionId}");
        //var transaction1 = await transactionService.AddTransactionAsync(new Transaction
        //{
        //    CustomerId = customer1.CustomerId,
        //    Amount = 6000,
        //    TransactionDate = DateTime.Now.AddMinutes(-30),
        //    Location = "New York"
        //});
        //Console.WriteLine($"Transaction added: {transaction1.TransactionId}");
        var transaction2 = await transactionService.AddTransactionAsync(new Transaction
        {
            CustomerId = customer2.CustomerId,
            Amount = 3500,
            TransactionDate = DateTime.Now.AddMinutes(-25),
            Location = "London"
        });
        Console.WriteLine($"Transaction added: {transaction2.TransactionId}");
        //var fraudRule=await fraudRuleService.AddFraudRuleAsync(new FraudRule
        //{

        //    RuleName = "High Amount US",
        //    ConditionExpression = "Amount > 5000 && Country == \"US\"",
        //    IsActive = true
        //});

        //Console.WriteLine($"Fraud rule added: {fraudRule.RuleName}");
        //var fraudRule1=  await fraudRuleService.AddFraudRuleAsync(new FraudRule
        //{
        //    RuleName = "High Amount US",
        //    ConditionExpression = "Amount > 5000 && Country == \"US\"",
        //    IsActive = true
        //});
        //Console.WriteLine($"Fraud rule added: {fraudRule1.RuleName}");
        //var fraudRule2=await fraudRuleService.AddFraudRuleAsync(new FraudRule
        //{
        //    RuleName = "High Amount UK",
        //    ConditionExpression = "Amount > 4000 && Country == \"UK\"",
        //    IsActive = true
        //});
        //Console.WriteLine($"Fraud rule added: {fraudRule2.RuleName}");
        var fraudCases = await fraudDetectionService.DetectFraudAsync(transaction2);
        Console.WriteLine($"Fraud cases detected: {fraudCases.Count}");
    }
}