
using System.ComponentModel.DataAnnotations;
using ISO810_ERP.Exceptions;
using ISO810_ERP.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ISO810_ERP.Filters;

public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        string message = context.Exception.Message;
        context.ExceptionHandled = true;

        if (context.Exception is AppException)
        {
            context.HttpContext.Response.StatusCode = 400;
        }

        context.Result = new JsonResult(ApiResponse.Failure(message));
    }
}