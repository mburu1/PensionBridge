using Microsoft.EntityFrameworkCore;
using PensionBridge.Domain.Schemes;
using PensionBridge.Domain.Employers;
using PensionBridge.Domain.Members;
using PensionBridge.Domain.Batches;
using PensionBridge.Domain.Contributions;
using PensionBridge.Domain.Audit;

namespace PensionBridge.Infrastructure.Persistence;

public class PensionBridgeDbContext : DbContext
{
    public PensionBridgeDbContext(DbContextOptions<PensionBridgeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Scheme> Schemes => Set<Scheme>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<ContributionBatch> ContributionBatches => Set<ContributionBatch>();
    public DbSet<ContributionLine> ContributionLines => Set<ContributionLine>();
    public DbSet<AuditRecord> AuditRecords => Set<AuditRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PensionBridgeDbContext).Assembly);
    }
}
