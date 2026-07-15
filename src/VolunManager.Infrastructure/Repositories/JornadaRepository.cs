using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Infrastructure.Context;
using VolunManager.Infrastructure.Interfaces;
using VolunManager.Infrastructure.Models;

namespace VolunManager.Infrastructure.Repositories
{
    public class JornadaRepository : IJornadaRepository
    {
        private readonly VolunManagerContext _context;

        public JornadaRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JornadaDto>> GetAllAsync()
        {
            return await _context.Jornadas
                .Include(j => j.Voluntario)
                .Select(j => new JornadaDto
                {
                    Id = j.Id,
                    Titulo = j.Titulo,
                    Descripcion = j.Descripcion,
                    Fecha = j.Fecha,
                    Lugar = j.Lugar,
                    HorasEstimadas = j.HorasEstimadas,
                    VoluntarioId = j.VoluntarioId,
                    NombreVoluntario = j.Voluntario != null
                        ? j.Voluntario.Nombre + " " + j.Voluntario.Apellido
                        : null
                })
                .ToListAsync();
        }

        public async Task<JornadaDto?> GetByIdAsync(int id)
        {
            return await _context.Jornadas
                .Include(j => j.Voluntario)
                .Where(j => j.Id == id)
                .Select(j => new JornadaDto
                {
                    Id = j.Id,
                    Titulo = j.Titulo,
                    Descripcion = j.Descripcion,
                    Fecha = j.Fecha,
                    Lugar = j.Lugar,
                    HorasEstimadas = j.HorasEstimadas,
                    VoluntarioId = j.VoluntarioId,
                    NombreVoluntario = j.Voluntario != null
                        ? j.Voluntario.Nombre + " " + j.Voluntario.Apellido
                        : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<JornadaDto> CreateAsync(JornadaCreateDto jornadaCreateDto)
        {
            var voluntarioExiste = await _context.Voluntarios
                .AnyAsync(v => v.Id == jornadaCreateDto.VoluntarioId);

            if (!voluntarioExiste)
            {
                throw new Exception($"No existe un voluntario con el ID {jornadaCreateDto.VoluntarioId}.");
            }

            var jornada = new Jornada
            {
                Titulo = jornadaCreateDto.Titulo,
                Descripcion = jornadaCreateDto.Descripcion,
                Fecha = jornadaCreateDto.Fecha,
                Lugar = jornadaCreateDto.Lugar,
                HorasEstimadas = jornadaCreateDto.HorasEstimadas,
                VoluntarioId = jornadaCreateDto.VoluntarioId
            };

            _context.Jornadas.Add(jornada);
            await _context.SaveChangesAsync();

            var jornadaDto = await GetByIdAsync(jornada.Id);

            return jornadaDto!;
        }

        public async Task<bool> UpdateAsync(int id, JornadaUpdateDto jornadaUpdateDto)
        {
            var jornada = await _context.Jornadas.FindAsync(id);

            if (jornada == null)
            {
                return false;
            }

            var voluntarioExiste = await _context.Voluntarios
                .AnyAsync(v => v.Id == jornadaUpdateDto.VoluntarioId);

            if (!voluntarioExiste)
            {
                throw new Exception($"No existe un voluntario con el ID {jornadaUpdateDto.VoluntarioId}.");
            }

            jornada.Titulo = jornadaUpdateDto.Titulo;
            jornada.Descripcion = jornadaUpdateDto.Descripcion;
            jornada.Fecha = jornadaUpdateDto.Fecha;
            jornada.Lugar = jornadaUpdateDto.Lugar;
            jornada.HorasEstimadas = jornadaUpdateDto.HorasEstimadas;
            jornada.VoluntarioId = jornadaUpdateDto.VoluntarioId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var jornada = await _context.Jornadas.FindAsync(id);

            if (jornada == null)
            {
                return false;
            }

            _context.Jornadas.Remove(jornada);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}