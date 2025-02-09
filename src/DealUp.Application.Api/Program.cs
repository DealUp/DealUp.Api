using DealUp.Application.Api.Extensions;
using DealUp.Infrastructure.Configuration.Extensions;
using DealUp.Infrastructure.Configuration.Middlewares;
using DealUp.Infrastructure.Configuration.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Section));

builder.Services.AddControllers().SetCamelCaseResponse();
builder.Services.AddSwagger();
builder.Services.ConfigureServices();

builder.AddAuthentication();
builder.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.MapGet("api/health", context => context.Response.WriteAsync("Alive!"));

await app.RunAsync();