
namespace ISO810_ERP.Models;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    public ApiResponse(bool success, string? message = null)
    {
        Success = success;
        Message = message;
    }
}