using Serilog;
using PensionBridge.Infrastructure.Persistence;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/pensionbridge-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting PensionBridge API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddOpenApi();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();

    // Add persistence
    builder.Services.AddPersistence(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseExceptionHandler();
    app.UseStatusCodePages();

    // Health endpoints
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
