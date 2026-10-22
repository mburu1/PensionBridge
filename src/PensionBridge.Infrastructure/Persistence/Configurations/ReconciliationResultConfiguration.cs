using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionBridge.Domain.Reconciliation;

namespace PensionBridge.Infrastructure.Persistence.Configurations;

public class ReconciliationResultConfiguration : IEntityTypeConfiguration<ReconciliationResult>
{
    public void Configure(EntityTypeBuilder<ReconciliationResult> builder)
    {
        builder.ToTable("ReconciliationResults");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BatchId).IsRequired();
        builder.Property(x => x.Rule).IsRequired();
        builder.Property(x => x.Severity).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasIndex(x => x.BatchId);
    }
}
