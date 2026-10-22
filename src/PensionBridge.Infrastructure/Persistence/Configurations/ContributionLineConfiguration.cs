using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionBridge.Domain.Contributions;

namespace PensionBridge.Infrastructure.Persistence.Configurations;

public class ContributionLineConfiguration : IEntityTypeConfiguration<ContributionLine>
{
    public void Configure(EntityTypeBuilder<ContributionLine> builder)
    {
        builder.ToTable("ContributionLines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BatchId)
            .IsRequired();

        builder.Property(x => x.MemberId)
            .IsRequired();

        builder.Property(x => x.SourceRowNumber)
            .IsRequired();

        builder.Property(x => x.EmployerContribution)
            .IsRequired();

        builder.Property(x => x.EmployeeContribution)
            .IsRequired();

        builder.Property(x => x.VoluntaryContribution)
            .IsRequired();

        builder.Property(x => x.IsValid)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.BatchId);
        builder.HasIndex(x => x.MemberId);
    }
}
