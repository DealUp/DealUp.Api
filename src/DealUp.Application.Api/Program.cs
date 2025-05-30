using DealUp.Application.Api.Extensions;
using DealUp.Database.Extensions;
using DealUp.Infrastructure.Extensions;
using DealUp.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().SetCamelCaseResponse();
builder.Services.AddApiReference();
builder.Services.ConfigureServices(builder.Configuration);
builder.AddPostgresqlDatabase();

builder.AddAuthentication();
builder.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.SetIsOriginAllowed(origin =>
        {
            var host = new Uri(origin).Host;
            return host is "localhost" or "127.0.0.1";
        }).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", context => context.Response.WriteAsync("Alive!"));
app.MapControllers();
app.UseApiReferenceIfDevelopment();

if (app.Environment.IsDevelopment())
{
    await app.ExecuteMigrationsAsync();
}

await app.RunAsync();