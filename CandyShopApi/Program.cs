using CandyShopApi.BusinessRules;
using CandyShopApi.Models;
using CandyShopApi.Services;
using Microsoft.AspNetCore.Diagnostics;
using MongoDB.Bson.IO;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CandyShopDatabaseSettings>(
    builder.Configuration.GetSection("CandyShopDatabase"));

builder.Services.AddSingleton<CandiesBusiness>();
builder.Services.AddSingleton<CandiesService>();
builder.Services.AddSingleton<CategoriesBusiness>();
builder.Services.AddSingleton<CategoriesService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var errorMessage = new
            {
                Message = error.Error.Message,
                StackTrace = error.Error.StackTrace
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
        }
    });
});

app.MapControllers();

app.Run();
