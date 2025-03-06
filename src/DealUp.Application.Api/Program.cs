using DealUp.Application.Api.Extensions;
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
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

await app.ExecuteMigrationsAsync();

await app.RunAsync();