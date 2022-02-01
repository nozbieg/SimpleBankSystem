using System;
using System.Linq;

namespace Application.Responses;

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public BaseResponse(bool success = true)
    {
        Success = success;
    }
    public BaseResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
