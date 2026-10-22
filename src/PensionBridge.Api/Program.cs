using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PensionBridge.Infrastructure.Persistence;
using PensionBridge.Infrastructure.Ingestion;
using PensionBridge.Infrastructure.BusinessCentral;
using PensionBridge.Application.Schemes;
using PensionBridge.Application.Ingestion;
using PensionBridge.Application.Reconciliation;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/pensionbridge-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting PensionBridge API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddOpenApi();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddScoped<ISchemeService, SchemeService>();
    builder.Services.AddScoped<IContributionIngestionService, ContributionIngestionService>();
    builder.Services.AddScoped<IReconciliationService, ReconciliationService>();
    builder.Services.AddHttpClient<IBusinessCentralClient, BusinessCentralClient>();
    builder.Services.Configure<BusinessCentralOptions>(builder.Configuration.GetSection("BusinessCentral"));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["Authentication:Authority"];
            options.Audience = builder.Configuration["Authentication:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true
            };
        });

    builder.Services.AddAuthorization();

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.UseExceptionHandler();
    app.UseStatusCodePages();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.MapGet("/health/live", () => Results.Ok(new { status = "healthy" }));
    app.MapGet("/health/ready", () => Results.Ok(new { status = "ready" }));
    app.MapControllers();

    Log.Information("PensionBridge API started successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
