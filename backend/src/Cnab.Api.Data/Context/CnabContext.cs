using Cnab.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cnab.Api.Data.Context
{
    public partial class CnabContext : DbContext
    {
        public CnabContext()
        {
        }

        public CnabContext(DbContextOptions<CnabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionTypes> TransactionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=54321;Database=assinador;Username=assinador;Password=assinador");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CnabContext).Assembly);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
