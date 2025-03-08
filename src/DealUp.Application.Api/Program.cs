using DealUp.Application.Api.Extensions;
using DealUp.Database.Extensions;
using DealUp.Infrastructure.Configuration.Extensions;
using DealUp.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().SetCamelCaseResponse();
builder.Services.AddSwagger();
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
            return host == "localhost" || host == "127.0.0.1";
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

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", context => context.Response.WriteAsync("Alive!"));
app.MapControllers();

if (app.Environment.IsProduction())
{
    await app.ExecuteMigrationsAsync();
}

await app.RunAsync();