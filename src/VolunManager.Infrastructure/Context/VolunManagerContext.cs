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

        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --------------------------------------------------------
            // 1. RESTRICCIONES DE CAMPOS E ÍNDICES ÚNICOS
            // --------------------------------------------------------

            modelBuilder.Entity<Voluntario>()
                .HasIndex(v => v.Correo)
                .IsUnique();
            modelBuilder.Entity<Voluntario>().Property(v => v.Nombre).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Voluntario>().Property(v => v.Apellido).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Voluntario>().Property(v => v.Correo).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Voluntario>().Property(v => v.Telefono).HasMaxLength(20);

            modelBuilder.Entity<Rol>()
                .HasIndex(r => r.Nombre)
                .IsUnique();
            modelBuilder.Entity<Rol>().Property(r => r.Nombre).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Rol>().Property(r => r.Descripcion).HasMaxLength(250);

            modelBuilder.Entity<Jornada>().Property(j => j.Titulo).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Jornada>().Property(j => j.Descripcion).HasMaxLength(500);
            modelBuilder.Entity<Jornada>().Property(j => j.Lugar).HasMaxLength(200);

            modelBuilder.Entity<Tarea>().Property(t => t.Titulo).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Tarea>().Property(t => t.Descripcion).HasMaxLength(500);


            // --------------------------------------------------------
            // 2. RELACIONES Y REGLAS DE BORRADO (Tus configuraciones originales)
            // --------------------------------------------------------

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

            // Si se borra la Jornada, sus Tareas no tienen sentido solas.
            modelBuilder.Entity<Jornada>()
                .HasMany(j => j.Tareas)
                .WithOne(t => t.Jornada)
                .HasForeignKey(t => t.JornadaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restrict (no Cascade) a proposito: si tambien cascadeara desde
            // Voluntario, SQL Server rechaza el esquema por tener dos caminos
            // de cascada distintos hacia la misma tabla (via Jornada y via
            // Voluntario directo). Ademas tiene sentido de negocio: no se
            // deberia poder borrar un voluntario con tareas activas.
            modelBuilder.Entity<Voluntario>()
                .HasMany(v => v.Tareas)
                .WithOne(t => t.Voluntario)
                .HasForeignKey(t => t.VoluntarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Guarda el enum como texto legible (Pendiente/EnProceso/Completada)
            // en vez del entero por defecto, para que la tabla se lea a simple vista.
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Estado)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Mismo criterio que con Tarea: cascada desde Jornada, restringida
            // desde Voluntario (evita el conflicto de doble camino de cascada
            // en SQL Server y protege registros de asistencia existentes).
            modelBuilder.Entity<Jornada>()
                .HasMany(j => j.Asistencias)
                .WithOne(a => a.Jornada)
                .HasForeignKey(a => a.JornadaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Voluntario>()
                .HasMany(v => v.Asistencias)
                .WithOne(a => a.Voluntario)
                .HasForeignKey(a => a.VoluntarioId)
                .OnDelete(DeleteBehavior.Restrict);


            // --------------------------------------------------------
            // 3. SEMILLAS (SEED DATA)
            // --------------------------------------------------------

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