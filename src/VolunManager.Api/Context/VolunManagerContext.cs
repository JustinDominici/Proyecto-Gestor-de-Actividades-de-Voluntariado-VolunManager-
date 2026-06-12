using Microsoft.EntityFrameworkCore;
using VolunManager.Api.Models;

namespace VolunManager.Api.Context
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
                .HasForeignKey(j => j.VoluntarioId);

            base.OnModelCreating(modelBuilder);
        }
    }
}