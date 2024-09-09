using Framework.Application;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace Framework;

public interface IEmailSenderService
{
    //Task<BaseResult> SendAsync(EmailModel email);
    Task<BaseResult> SendWelcome(EmailModel model);
    Task<BaseResult> SendVerification(EmailModel model);
}
/// <summary>
/// 
/// </summary>
public class EmailSenderService : IEmailSenderService
{

    private BaseResult _op = new();
    private readonly EmailOptions _emailOptions;


    public EmailSenderService(IOptionsSnapshot<EmailOptions> options)
    {
        _emailOptions = options.Value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    private async Task SendAsync(EmailModel email)
    {
        var op = new BaseResult();
        try
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress(_emailOptions.Address, email.Subject),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            email.To.ForEach(e => message.To.Add(e));

            using SmtpClient client = new SmtpClient(_emailOptions.Host, _emailOptions.Port);
            client.Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password);
            client.UseDefaultCredentials = _emailOptions.UseDefaultCredentials;
            client.Send(message);
            await Task.Delay(3000);
            await Task.CompletedTask;
            //return op.Set(HttpStatusCode.OK).Succeeded();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await Task.CompletedTask;

            //return op.Set(HttpStatusCode.BadRequest).Failed(e.Message);
        }
    }

    public async Task<BaseResult> SendWelcome(EmailModel model)
    {
        try
        {
            var content = await File.ReadAllTextAsync(GetPath("Welcome.txt"));
            content = Clean(content);
            content = string.Format(content, args: model.Parameters.ToArray());
            model.Body = content;
            model.Subject = "Welcome To the Tournomentor";
            await SendAsync(model);
            return _op.Set(HttpStatusCode.OK).Succeeded(OperationMessage.Send);
        }
        catch (Exception e)
        {
            return _op.Set(HttpStatusCode.BadRequest).Failed("Error");
        }
    }

    public async Task<BaseResult> SendVerification(EmailModel model)
    {
        try
        {
            var content = await File.ReadAllTextAsync(GetPath("Verification.txt"));
            content = Clean(content);
            content = string.Format(content, model.Parameters.ToArray());
            model.Body = content;
            model.Subject = "Verification Account ( Tournomentor )";
            await SendAsync(model);

            return _op.Set(HttpStatusCode.OK).Succeeded(OperationMessage.Send);

        }
        catch (Exception e)
        {
            return _op.Set(HttpStatusCode.BadRequest).Failed("Error");
        }

    }

    private string GetPath(string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MailTemplate", fileName);
    }
    private static string Clean(string content)
    {
        string pattern = @"(?<!\d)([{}])(?!\d)";
        content = Regex.Replace(content, pattern, m => m.Value == "{" ? "{{" : "}}");
        return content;
    }
}

/// <summary>
/// 
/// </summary>
public class EmailOptions
{
    public string Address { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool UseDefaultCredentials { get; set; }
}
public class EmailModel
{
    public List<string> To { get; set; } = new();
    public List<string> Parameters { get; set; } = new();
    public string Subject { get; set; }
    public string Body { get; set; }
    public EmailModel(List<string> to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public EmailModel(string to)
    {
        To = new List<string>() { to };
    }

    public EmailModel()
    {

    }

}