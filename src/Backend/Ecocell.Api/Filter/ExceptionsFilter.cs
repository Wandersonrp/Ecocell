using System.Net;
using Ecocell.Communication.Response.Error;
using Ecocell.Exceptions;
using Ecocell.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecocell.Api.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is EcocellException)
        {
            HandleEcocellException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }
    }

    private void HandleEcocellException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorsException)
        {
            HandleValidationException(context);
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var validationErrorException = context.Exception as ValidationErrorsException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseError(validationErrorException.ErrorMessages.ToList()));
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseError(ResourceErrorMessage.UNKNOWN_ERROR));
    }
}