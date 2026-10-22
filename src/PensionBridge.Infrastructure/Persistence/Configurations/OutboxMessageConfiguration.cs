using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionBridge.Domain.Posting;

namespace PensionBridge.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.MessageType).IsRequired();
        builder.Property(x => x.Payload).IsRequired();
        builder.Property(x => x.IdempotencyKey).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.IdempotencyKey).IsUnique();
    }
}
