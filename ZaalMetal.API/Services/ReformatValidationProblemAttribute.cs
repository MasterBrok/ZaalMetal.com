using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZaalMetal.API.Services;

public class ReformatValidationProblemAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is BadRequestObjectResult { Value: ValidationProblemDetails })
        {
            var error = new BaseResult(false);
            var list = new List<string>();
            context.ModelState.ToList().ForEach(a =>
            {
                string str = "";
                str = $"{a.Key}  {a.Value.Errors.Aggregate(str, (current, msg) => current + (msg.ErrorMessage + "\n"))}";
                list.Add(str);
            });
            error.Set(HttpStatusCode.BadRequest);
            error.Messages = list.ToArray();
            context.Result = new JsonResult(error);
        }

        base.OnResultExecuting(context);
    }
}