using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionBridge.Domain.Exceptions;

namespace PensionBridge.Infrastructure.Persistence.Configurations;

public class ExceptionCaseConfiguration : IEntityTypeConfiguration<ExceptionCase>
{
    public void Configure(EntityTypeBuilder<ExceptionCase> builder)
    {
        builder.ToTable("ExceptionCases");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BatchId).IsRequired();
        builder.Property(x => x.ExceptionType).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasIndex(x => x.BatchId);
    }
}
