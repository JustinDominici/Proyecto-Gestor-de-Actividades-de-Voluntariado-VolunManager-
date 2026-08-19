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

        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voluntario>()
                .HasMany(v => v.Jornadas)
                .WithOne(j => j.Voluntario)
                .HasForeignKey(j => j.VoluntarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Voluntarios)
                .WithOne(v => v.Rol)
                .HasForeignKey(v => v.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Roles base del sistema. Se usa un objeto anonimo porque Rol
            // no tiene un constructor publico sin parametros ni setters
            // publicos (encapsulamiento) - HasData solo necesita los valores
            // de columna para generar el INSERT en la migracion.
            modelBuilder.Entity<Rol>().HasData(
                new { Id = 1, Nombre = "Voluntario", Descripcion = "Persona que participa en jornadas y tareas de voluntariado." },
                new { Id = 2, Nombre = "Coordinador", Descripcion = "Organiza jornadas y supervisa el trabajo de los voluntarios." },
                new { Id = 3, Nombre = "Administrador", Descripcion = "Administra el sistema y gestiona los datos generales." }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
