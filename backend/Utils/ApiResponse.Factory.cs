namespace ISO810_ERP.Utils;

public partial class ApiResponse
{
    public static ApiResponse Successful(string? message = null)
    {
        return new ApiResponse
        {
            Success = true,
            Message = message
        };
    }

    public static ApiResponse Failure(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message
        };
    }

    public static ApiResponse Json<T>(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data
        };
    }
}

