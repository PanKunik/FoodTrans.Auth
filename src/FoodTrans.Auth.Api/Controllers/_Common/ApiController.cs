using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrans.Auth.Controllers.Common;

[ApiController]
public class ApiController : ControllerBase
{
    public const string Errors = "errors";

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[Errors] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}