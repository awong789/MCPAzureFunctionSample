# MCP Azure Function Sample

This project demonstrates how to transform existing Azure Functions into Model Context Protocol (MCP) tools, enabling seamless integration with modern LLM applications. The sample showcases a loan calculator that can be accessed both as a traditional REST API and as an MCP tool.

## 📖 Original Blog Post

This project accompanies the blog post: **[Implement Azure Function MCP to Connect Your Legacy API with Modern LLM Applications](https://aylwinwong.wordpress.com/2025/07/26/implement-azure-function-mcp-to-connect-your-legacy-api-with-modern-llm-applications/)** by Aylwin Wong.

## 🎯 Purpose

The purpose of this project is to demonstrate building MCP (Model Context Protocol) tools using Azure Functions. It shows how you can:

- Transform existing Azure Functions into MCP-compatible tools
- Maintain backward compatibility with traditional REST APIs
- Enable LLM applications to interact with your legacy systems
- Leverage the new MCP Tool bindings for Azure Functions

## 🏗️ Project Structure

The solution contains three main projects:

### 1. MyLegacyApp.Core
Contains the core business logic for loan calculations:
- `LoanCalculator.cs` - Core loan calculation logic
- `Model/LoanRequest.cs` - Data model for loan requests

### 2. AzureFunctionLoanCalculator
Traditional Azure Function API that exposes the loan calculator as REST endpoints:
- `FunctionLoanCalculator.cs` - HTTP-triggered functions
- Provides `CalculateLoan` endpoint for loan calculations
- Uses standard HTTP triggers and JSON serialization

### 3. MCPAzureFunctionSample
MCP-enabled Azure Function that exposes the same functionality as MCP tools:
- `MCPLoanCalculator.cs` - MCP tool implementation
- Uses `McpToolTrigger` and `McpToolProperty` attributes
- Enables LLM applications to call the loan calculator directly

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 (or VS Code)
- .NET 8.0 SDK
- API Client Tester (Bruno, Postman, etc.)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/awong789/MCPAzureFunctionSample.git
cd MCPAzureFunctionSample
```

2. Open the solution in Visual Studio 2022 or VS Code

3. Restore NuGet packages:
```bash
dotnet restore
```

## 🔧 Configuration

### MCP Azure Function Setup

The MCP Azure Function requires the following NuGet packages:
- `Microsoft.Azure.Functions.Worker.Extensions.Mcp` (version 1.0.0-preview.6 or later)
- `Microsoft.Azure.Functions.Worker.Sdk` (version 2.0.2 or later)

### Program.cs Configuration

The MCP functionality is enabled in `Program.cs`:

```csharp
// Enable MCP metadata
builder.EnableMcpToolMetadata();
builder.Services.AddSingleton<LoanCalculator>();
```

## 🧪 Testing

### Testing the Traditional Azure Function

1. Set `AzureFunctionLoanCalculator` as the startup project
2. Press F5 to run the function
3. Use an API client to test the `CalculateLoan` endpoint:

```json
POST /api/CalculateLoan
Content-Type: application/json

{
  "Principal": 100000,
  "AnnualInterestRate": 5.5,
  "NumberOfPayments": 360
}
```

### Testing the MCP Azure Function

1. Set `MCPAzureFunctionSample` as the startup project
2. Press F5 to run the function
3. The console will display the SSE endpoint (e.g., `http://localhost:7047/runtime/webhooks/mcp/sse`)
4. Open the endpoint in a web browser to verify the MCP server is running

### Testing with Claude Desktop

1. Ensure the MCP Azure Function is running
2. Edit your `claude_desktop_config.json`:

```json
{
  "mcpServers": {
    "loan-calculator": {
      "command": "npx",
      "args": [
        "-y",
        "mcp-remote",
        "http://localhost:7047/runtime/webhooks/mcp/sse",
        "--allow-http"
      ]
    }
  }
}
```

3. Restart Claude Desktop
4. Ask Claude to calculate a loan payment using the MCP tool

## 🔍 MCP Tool Implementation

The MCP tool is implemented using the following attributes:

```csharp
[McpToolTrigger("CalculateLoan", "Calculate Loan Repayment amount")]
[McpToolProperty("Principal", "decimal", "The principal amount currently in the loan")]
[McpToolProperty("AnnualInterestRate", "decimal", "The current interest rate annually")]
[McpToolProperty("NumberOfPayments", "int", "Number of payments intended")]
```

This provides:
- Tool name and description for LLM understanding
- Parameter definitions with types and descriptions
- Automatic parameter validation and conversion

## ⚠️ Important Notes

- **Preview Feature**: The Azure Function MCP extension is currently in preview and may have specification changes
- **Isolated Worker Model**: Currently only supports isolated worker model
- **Production Readiness**: Not yet recommended for production use
- **Security Considerations**: Exposing systems to LLM tools introduces new security considerations (prompt injection, etc.)

## 🔗 Useful Links

- [MCP Inspector](https://modelcontextprotocol.io/inspector) - Debug your MCP server
- [Model Context Protocol Documentation](https://modelcontextprotocol.io/)
- [Azure Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)

## 🤝 Contributing

This is a sample project accompanying the blog post. For issues or questions, please refer to the original blog post or create an issue in the repository.

## 📄 License

This project is provided as-is for educational purposes. Please refer to the original blog post for more details.

---

**Note**: This project demonstrates the integration of legacy Azure Functions with modern LLM applications through MCP. The loan calculator is a simple example - in real-world scenarios, you would apply this pattern to more complex business logic and APIs. 
