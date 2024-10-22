using BarberBossManagement.Communication.Responses;
using BarberBossManagement.Exception;
using BarberBossManagement.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBossManagement.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BarberBossManagementException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossManagementException = context.Exception as BarberBossManagementException;
        var errorResponse = new ResponseErrorJson(barberBossManagementException!.GetErrors());

        context.HttpContext.Response.StatusCode = barberBossManagementException.StatusCode;
        context.Result = new ObjectResult(errorResponse);

    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
