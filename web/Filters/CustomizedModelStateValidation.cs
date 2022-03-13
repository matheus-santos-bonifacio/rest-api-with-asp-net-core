using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Filters;

public class CustomizedModelStateValidation : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
      {
        if (!context.ModelState.IsValid)
        {
            var validateFieldViewModelOutput = new ValidateFieldViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors
                        .Select(s => s.ErrorMessage)));
            context.Result = new BadRequestObjectResult(validateFieldViewModelOutput);
        }
      }
}
