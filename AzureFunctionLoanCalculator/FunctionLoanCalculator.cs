using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyLegacyApp.Core;
using MyLegacyApp.Core.Model;

namespace AzureFunctionLoanCalculator;

public class FunctionLoanCalculator
{
    private readonly ILogger<FunctionLoanCalculator> _logger;
    private LoanCalculator _loanCalculator = new LoanCalculator();

    public FunctionLoanCalculator(ILogger<FunctionLoanCalculator> logger)
    {
        _logger = logger;
    }

    [Function("Info")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("This is my current Azure Function");
    }

    [Function("CalculateLoan")]
    public async Task<IActionResult> CalculateLoan(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request for loan calculation.");
        string requestBody;
        using (var reader = new StreamReader(req.Body))
        {
            requestBody = await reader.ReadToEndAsync();
        }
        var loanRequest = System.Text.Json.JsonSerializer.Deserialize<LoanRequest>(requestBody);
        if (loanRequest == null || loanRequest.Principal <= 0 || loanRequest.AnnualInterestRate <= 0 || loanRequest.NumberOfPayments <= 0)
        {
            return new BadRequestObjectResult("Invalid loan request parameters.");
        }
        decimal monthlyPayment = _loanCalculator.CalculateMonthlyPayment(loanRequest.Principal, loanRequest.AnnualInterestRate, loanRequest.NumberOfPayments);
        return new OkObjectResult(new { MonthlyPayment = monthlyPayment });
    }
}