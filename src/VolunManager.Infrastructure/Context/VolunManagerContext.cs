using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;

namespace VolunManager.Infrastructure.Context
{
    public class VolunManagerContext : DbContext
    {
        public VolunManagerContext(DbContextOptions<VolunManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Voluntario> Voluntarios { get; set; }

        public DbSet<Jornada> Jornadas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voluntario>()
                .HasMany(v => v.Jornadas)
                .WithOne(j => j.Voluntario)
                .HasForeignKey(j => j.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}