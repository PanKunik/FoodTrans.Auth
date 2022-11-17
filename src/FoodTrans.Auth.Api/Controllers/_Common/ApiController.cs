using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FoodTrans.Auth.Controllers.Common;

[ApiController]
public class ApiController : ControllerBase
{
    public const string Errors = "errors";

    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[Errors] = errors;

        if(errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach(var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }

        if(errors.Any(error => error.Type == ErrorType.Unexpected))
        {
            var error = errors.First(x => x.Type == ErrorType.Unexpected);
            return Problem(statusCode: 500, title: error.Description);
        }

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