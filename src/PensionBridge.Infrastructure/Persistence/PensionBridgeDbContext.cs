using Microsoft.EntityFrameworkCore;

namespace PensionBridge.Infrastructure.Persistence;

public class PensionBridgeDbContext : DbContext
{
    public PensionBridgeDbContext(DbContextOptions<PensionBridgeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PensionBridgeDbContext).Assembly);
    }
}
