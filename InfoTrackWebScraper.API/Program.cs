using System.Net;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddCors(options => 
{
    options.AddPolicy("ReactFrontEnd", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
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

app.UseAuthorization();

// Adding middleware lambda for exception handling
app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (XmlException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
    catch (NullReferenceException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.UseCors("ReactFrontEnd");

app.MapControllers();

app.Run();
