using System.Net;
using Microsoft.AspNetCore.Identity;


namespace Framework.Application;

public class OperationResult<T> : BaseResult
{
    public string[]? Messages { get; set; }
    public T? Data { get; set; } = default;
    public OperationResult<T> Succeeded(string? message = OperationMessage.Done)
    {
        Success = true;
        //base.Messages = !string.IsNullOrWhiteSpace(message) ? message : OperationMessage.Done;
        Messages = new string[] { message };
        return this;
    }

    public OperationResult<T> Failed(params string[] messages)
    {
        Success = false;
        //base.Messages = OperationMessage.Failed;
        Messages = messages;
        return this;
    }

    public OperationResult<T> Set(HttpStatusCode code)
    {
        HttpCode = (int)code;
        return this;
    }


    public OperationResult<T> NotFound()
    {
        HttpCode = (int)HttpStatusCode.NotFound;
        Messages = new[] { OperationMessage.NotFound };
        Success = false;
        return this;
    }
    public OperationResult<T> Done()
    {
        HttpCode = (int)HttpStatusCode.OK;
        Messages = new[] { OperationMessage.Done };
        Success = true;
        return this;
    }

    public OperationResult<T> What(int code = 0)
    {
        HttpCode = Data is not null ? (int)HttpStatusCode.OK : code;
        Messages = Data is not null ? new[] { OperationMessage.Done } : Array.Empty<string>();
        Success = Data is not null;
        return this;
    }
    
}

public class ApiResponse<A> : BaseApiResponse
{
    public string[]? Messages { get; set; }
    public A? Response { get; set; } = default;

    public ApiResponse<A> Succeeded(string message = OperationMessage.Done)
    {
        ((BaseResult)this).Messages = new string[] { message };
        Success = true;
        return this;
    }

    public ApiResponse<A> Failed(string[] messages)
    {
        ((BaseResult)this).Messages = new string[] { OperationMessage.Failed };
        Success = false;
        return this;
    }

    public ApiResponse<A> FailedErrors(params string[] messages)
    {
        ((BaseResult)this).Messages = messages;
        Success = false;
        return this;
    }
    public ApiResponse<A> Set(HttpStatusCode code)
    {
        HttpCode = (int)code;
        return this;
    }

    public static explicit operator ApiResponse<A>(OperationResult<A> op)
    {
        return new ApiResponse<A>()
        {
            Response = op.Data,
            HttpCode = op.HttpCode,
            Messages = op.Messages,
            Success = op.Success
        };
    }
}

public abstract class BaseApiResponse : BaseResult
{


}