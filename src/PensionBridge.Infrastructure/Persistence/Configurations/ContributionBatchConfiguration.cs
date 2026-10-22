using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionBridge.Domain.Batches;

namespace PensionBridge.Infrastructure.Persistence.Configurations;

public class ContributionBatchConfiguration : IEntityTypeConfiguration<ContributionBatch>
{
    public void Configure(EntityTypeBuilder<ContributionBatch> builder)
    {
        builder.ToTable("ContributionBatches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EmployerId)
            .IsRequired();

        builder.Property(x => x.SchemeId)
            .IsRequired();

        builder.Property(x => x.Period)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FileHash)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.IdempotencyKey)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.IdempotencyKey).IsUnique();
        builder.HasIndex(x => x.EmployerId);
        builder.HasIndex(x => x.SchemeId);
        builder.HasIndex(x => x.Status);
    }
}
