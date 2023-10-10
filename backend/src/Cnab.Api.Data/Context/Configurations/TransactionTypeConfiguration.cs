using Cnab.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cnab.Api.Data.Context.Configurations
{
    internal class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionTypes>
    {
        public void Configure(EntityTypeBuilder<TransactionTypes> builder)
        {
            builder.ToTable("transaction_types");
            builder.Property(e => e.Id).IsRequired().HasColumnName("id");
            builder.Property(e => e.Kind).IsRequired().HasMaxLength(8).HasColumnName("kind");
            builder.Property(e => e.Signal).IsRequired().HasMaxLength(1).HasColumnName("signal");
            builder.Property(e => e.Description).IsRequired().HasColumnName("description");

            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.Kind);
        }
    }
}
