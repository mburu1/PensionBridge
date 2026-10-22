using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PensionBridge.Application.Abstractions;
using PensionBridge.Domain.Schemes;
using PensionBridge.Domain.Employers;
using PensionBridge.Domain.Members;
using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Contributions;
using PensionBridge.Domain.Audit;
using PensionBridge.Domain.Reconciliation;
using PensionBridge.Domain.Exceptions;
using PensionBridge.Domain.Posting;
using PensionBridge.Infrastructure.Persistence.Implementations;

namespace PensionBridge.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PensionBridge");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'PensionBridge' is not configured.");
        }

        services.AddDbContext<PensionBridgeDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IRepository<Scheme>, Repository<Scheme>>();
        services.AddScoped<IRepository<Employer>, Repository<Employer>>();
        services.AddScoped<IRepository<Member>, Repository<Member>>();
        services.AddScoped<IRepository<ContributionBatch>, Repository<ContributionBatch>>();
        services.AddScoped<IRepository<ContributionLine>, Repository<ContributionLine>>();
        services.AddScoped<IRepository<AuditRecord>, Repository<AuditRecord>>();
        services.AddScoped<IRepository<ReconciliationResult>, Repository<ReconciliationResult>>();
        services.AddScoped<IRepository<ExceptionCase>, Repository<ExceptionCase>>();
        services.AddScoped<IRepository<OutboxMessage>, Repository<OutboxMessage>>();

        return services;
    }
}
