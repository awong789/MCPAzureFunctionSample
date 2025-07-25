using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using MyLegacyApp.Core;

namespace MCPAzureFunctionSample;

public class MCPLoanCalculator
{
    private readonly ILogger<MCPLoanCalculator> _logger;
    private LoanCalculator _loanCalculator = new LoanCalculator();

    public MCPLoanCalculator(ILogger<MCPLoanCalculator> logger)
    {
        _logger = logger;
    }


    [Function(nameof(CalculateLoan))]
    public async Task<string> CalculateLoan(
   [McpToolTrigger("CalculateLoan", "Calculate Loan Repayment amount")] ToolInvocationContext context,
   [McpToolProperty("Principal", "decimal", "The principal amount currently in the loan")] decimal Principal,
   [McpToolProperty("AnnualInterestRate", "decimal", "The current interest rate annually")] decimal AnnualInterestRate,
   [McpToolProperty("NumberOfPayments", "int", "Number of payments intended")] int NumberOfPayments)
    {
        _logger.LogInformation($"Calculate Loan called");

        if (Principal <= 0 || AnnualInterestRate <= 0 || NumberOfPayments <= 0)
        {
            _logger.LogError("Invalid loan request parameters.");
            return "Unable to calculate, loan request parameters error";
        }

        decimal monthlyPayment = _loanCalculator.CalculateMonthlyPayment(Principal, AnnualInterestRate, NumberOfPayments);
        return $"The monthly payment is {monthlyPayment:C2}.";
    }
}