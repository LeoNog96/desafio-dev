using Cnab.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cnab.Api.Data.Context.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.Property(e => e.Id).IsRequired().HasColumnName("id");
            builder.Property(e => e.Login).IsRequired().HasMaxLength(50).HasColumnName("login");
            builder.Property(e => e.Password).IsRequired().HasMaxLength(10).HasColumnName("password");
            
            builder.Property(e => e.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).IsRequired(false).HasColumnName("updated_at");
            builder.Property(e => e.RemovedAt).IsRequired(false).HasColumnName("removed_at");
            builder.Property(e => e.Removed).IsRequired().HasColumnName("removed");

            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.Login);
        }
    }
}
