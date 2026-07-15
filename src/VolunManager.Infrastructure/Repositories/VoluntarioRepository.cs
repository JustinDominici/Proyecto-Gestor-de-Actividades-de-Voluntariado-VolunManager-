using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Infrastructure.Context;
using VolunManager.Infrastructure.Interfaces;
using VolunManager.Infrastructure.Models;

namespace VolunManager.Infrastructure.Repositories
{
    public class VoluntarioRepository : IVoluntarioRepository
    {
        private readonly VolunManagerContext _context;

        public VoluntarioRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VoluntarioDto>> GetAllAsync()
        {
            return await _context.Voluntarios
                .Select(v => new VoluntarioDto
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Correo = v.Correo,
                    Telefono = v.Telefono,
                    Activo = v.Activo
                })
                .ToListAsync();
        }

        public async Task<VoluntarioDto?> GetByIdAsync(int id)
        {
            return await _context.Voluntarios
                .Where(v => v.Id == id)
                .Select(v => new VoluntarioDto
                {
                    Id = v.Id,
                    Nombre = v.Nombre,
                    Apellido = v.Apellido,
                    Correo = v.Correo,
                    Telefono = v.Telefono,
                    Activo = v.Activo
                })
                .FirstOrDefaultAsync();
        }

        public async Task<VoluntarioDto> CreateAsync(VoluntarioCreateDto voluntarioCreateDto)
        {
            var voluntario = new Voluntario
            {
                Nombre = voluntarioCreateDto.Nombre,
                Apellido = voluntarioCreateDto.Apellido,
                Correo = voluntarioCreateDto.Correo,
                Telefono = voluntarioCreateDto.Telefono,
                Activo = true
            };

            _context.Voluntarios.Add(voluntario);
            await _context.SaveChangesAsync();

            return new VoluntarioDto
            {
                Id = voluntario.Id,
                Nombre = voluntario.Nombre,
                Apellido = voluntario.Apellido,
                Correo = voluntario.Correo,
                Telefono = voluntario.Telefono,
                Activo = voluntario.Activo
            };
        }

        public async Task<bool> UpdateAsync(int id, VoluntarioUpdateDto voluntarioUpdateDto)
        {
            var voluntario = await _context.Voluntarios.FindAsync(id);

            if (voluntario == null)
            {
                return false;
            }

            voluntario.Nombre = voluntarioUpdateDto.Nombre;
            voluntario.Apellido = voluntarioUpdateDto.Apellido;
            voluntario.Correo = voluntarioUpdateDto.Correo;
            voluntario.Telefono = voluntarioUpdateDto.Telefono;
            voluntario.Activo = voluntarioUpdateDto.Activo;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var voluntario = await _context.Voluntarios.FindAsync(id);

            if (voluntario == null)
            {
                return false;
            }

            _context.Voluntarios.Remove(voluntario);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}