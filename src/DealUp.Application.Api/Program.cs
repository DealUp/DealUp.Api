using DealUp.Application.Api.Extensions;
using DealUp.Constants;
using DealUp.Database.Extensions;
using DealUp.Infrastructure.Configuration.Extensions;
using DealUp.Infrastructure.Configuration.Middlewares;
using DealUp.Infrastructure.Configuration.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));

builder.Services.AddControllers().SetCamelCaseResponse();
builder.Services.AddSwagger();
builder.Services.ConfigureServices();
builder.AddPostgresqlDatabase();

builder.AddAuthentication();
builder.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPoliciesConstants.ALLOW_ALL, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(CorsPoliciesConstants.ALLOW_ALL);

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.MapGet("api/health", context => context.Response.WriteAsync("Alive!"));

await app.ExecuteMigrationsAsync();

await app.RunAsync();