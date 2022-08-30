using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace PollStar.Core.Exceptions;

public class PollStarExceptionsFilter : ExceptionFilterAttribute
{

    private readonly ILogger<PollStarExceptionsFilter> _logger;

    public PollStarExceptionsFilter(ILogger<PollStarExceptionsFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);

        if (context.Exception is PollStarException exception)
        {
            _logger.LogError(exception, "Error {code} occured while processing a request", exception.ErrorCode.Code);
            context.Result = new ObjectResult(new ErrorMessageDto
            {
                ErrorCode = exception.ErrorCode.Code,
                TranslationKey = exception.ErrorCode.TranslationKey,
                ErrorMessage = exception.Message,
                Substitutions = exception.Substitutes
            })
            {
                StatusCode = 409
            };
            context.ExceptionHandled = true;
        }
    }

}