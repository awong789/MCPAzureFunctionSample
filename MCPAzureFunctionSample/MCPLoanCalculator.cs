using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCPAzureFunctionSample;

public class MCPLoanCalculator
{
    private readonly ILogger<MCPLoanCalculator> _logger;

    public MCPLoanCalculator(ILogger<MCPLoanCalculator> logger)
    {
        _logger = logger;
    }

    [Function("Info")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}