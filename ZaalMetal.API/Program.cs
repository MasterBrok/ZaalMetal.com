using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Music.EfCore;
using System.Reflection;
using Music.Configuration;
using ZaalMetal.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cs = builder.Configuration.GetConnectionString("Key1");

builder.Services.AddMusicServices(cs);


builder.Services.AddSingleton<IFileUpload, FileUpload>();


builder.Services
    .AddControllers(co => co.Filters.Add(typeof(ReformatValidationProblemAttribute)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));


    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ZaalMetal",
        Description = "توسعه توسط تیم زال متال",
        TermsOfService = new Uri("https://www.ZaalMetal.com/Developers"),
        Contact = new OpenApiContact
        {
            Name = "ارتباط با ما",
            Url = new Uri("https://t.me/ZaalMetal")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
