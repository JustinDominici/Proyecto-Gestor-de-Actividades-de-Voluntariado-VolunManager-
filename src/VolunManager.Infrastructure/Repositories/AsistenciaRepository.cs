using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class AsistenciaRepository : IAsistenciaRepository
    {
        private readonly VolunManagerContext _context;

        public AsistenciaRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asistencia>> GetAllAsync()
        {
            return await _context.Asistencias
                .Include(a => a.Voluntario)
                .Include(a => a.Jornada)
                .ToListAsync();
        }

        public async Task<Asistencia?> GetByIdAsync(int id)
        {
            return await _context.Asistencias
                .Include(a => a.Voluntario)
                .Include(a => a.Jornada)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> ExisteVoluntarioAsync(int voluntarioId)
        {
            return await _context.Voluntarios.AnyAsync(v => v.Id == voluntarioId);
        }

        public async Task<bool> ExisteJornadaAsync(int jornadaId)
        {
            return await _context.Jornadas.AnyAsync(j => j.Id == jornadaId);
        }

        public async Task<bool> ExisteAsistenciaAsync(int voluntarioId, int jornadaId)
        {
            return await _context.Asistencias
                .AnyAsync(a => a.VoluntarioId == voluntarioId && a.JornadaId == jornadaId);
        }

        public async Task AddAsync(Asistencia asistencia)
        {
            await _context.Asistencias.AddAsync(asistencia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return false;
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
