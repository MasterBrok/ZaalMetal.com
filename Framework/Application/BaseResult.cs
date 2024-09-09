using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Framework.Application;

public class BaseResult
{
    public BaseResult(bool success, string[]? messages, int httpCode)
    {
        Success = success;
        Messages = messages;
        HttpCode = httpCode;
    }

    public BaseResult(bool success)
    {
        Success = success;
    }

    public BaseResult()
    {

    }
    public bool Success { get; protected set; }
    public string[]? Messages { get; set; }
    public int HttpCode { get; protected set; }


    public BaseResult Succeeded(params string[]? messages)
    {
        Success = true;
        Messages = messages;
        //Messages = !string.IsNullOrWhiteSpace(message) ? message : OperationMessage.Done;
        return this;
    }

    public BaseResult Failed(params string[] messages)
    {
        Success = false;
        //Messages = string.Join(',', messages);
        Messages = messages;
        return this;
    }

    public BaseResult Set(HttpStatusCode code)
    {
        HttpCode = (int)code;
        return this;
    }

    public static explicit operator BaseResult(IdentityResult result)
    {
        return new BaseResult(result.Succeeded)
        {
            Messages = result.Errors.Select(e => $"{e.Description} || {e.Code}").ToArray()
        };
    }

    public static explicit operator BaseResult(bool result)
    {
        return new()
        {
            HttpCode = result ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
            Messages = result ? new[] { OperationMessage.Done } : new string[] { OperationMessage.Database },
            Success = result
        };
    }

    public BaseResult NotFound()
    {
        HttpCode = (int)HttpStatusCode.NotFound;
        Messages = new[] { OperationMessage.NotFound };
        Success = false;
        return this;

    }

    public BaseResult Ok()
    {
        HttpCode = (int)HttpStatusCode.OK;
        Messages = new[] { OperationMessage.Done };
        Success = true;
        return this;
    }

}
