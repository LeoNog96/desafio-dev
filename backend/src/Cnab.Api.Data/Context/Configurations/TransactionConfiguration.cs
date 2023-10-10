using Cnab.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cnab.Api.Data.Context.Configurations
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");
            builder.Property(e => e.Id).IsRequired().HasColumnName("id");
            builder.Property(e => e.Type).IsRequired().HasColumnName("type");
            builder.Property(e => e.StoreOwner).IsRequired().HasMaxLength(14).HasColumnName("store_owner");
            builder.Property(e => e.StoreName).IsRequired().HasMaxLength(19).HasColumnName("store_name");
            builder.Property(e => e.Cpf).IsRequired().HasMaxLength(11).HasColumnName("cpf");
            builder.Property(e => e.Card).IsRequired().HasMaxLength(12).HasColumnName("card");
            builder.Property(e => e.Date).IsRequired().HasColumnName("date");
            builder.Property(e => e.UploadedBy).IsRequired().HasColumnName("uploaded_by");
            builder.Property(e => e.Value).IsRequired().HasColumnName("value");

            builder.Property(e => e.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).IsRequired(false).HasColumnName("updated_at");
            builder.Property(e => e.RemovedAt).IsRequired(false).HasColumnName("removed_at");
            builder.Property(e => e.Removed).IsRequired().HasColumnName("removed");
            
            builder.HasKey(x => x.Id);
            
            builder.HasIndex(e => e.Cpf);
            builder.HasIndex(e => e.StoreOwner);
            builder.HasIndex(e => e.StoreName);
            builder.HasIndex(e => e.Card);
            builder.HasIndex(e => e.Date);
            builder.HasIndex(e => e.Type);
            builder.HasIndex(e => e.UploadedBy);
            builder.HasIndex(e => new { e.UploadedBy, e.Date });
            builder.HasIndex(e => new { e.UploadedBy, e.Date, e.Cpf });
            builder.HasIndex(e => new { e.UploadedBy, e.Date, e.Cpf, e.Type });

            builder.HasOne(x => x.User)
                        .WithMany(c => c.Transactions)
                        .HasForeignKey(v => v.UploadedBy)
                        .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.TransactionType)
                       .WithMany(c => c.Transactions)
                       .HasForeignKey(v => v.Type)
                       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
