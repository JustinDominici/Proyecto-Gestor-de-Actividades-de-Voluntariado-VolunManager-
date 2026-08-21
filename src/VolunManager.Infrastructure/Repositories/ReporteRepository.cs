using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly VolunManagerContext _context;

        public ReporteRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asistencia>> ObtenerAsistenciasVoluntarioAsync(int voluntarioId)
        {
            return await _context.Asistencias
                .Where(a => a.VoluntarioId == voluntarioId)
                .Include(a => a.Jornada)
                .OrderByDescending(a => a.HoraEntrada)
                .ToListAsync();
        }

        public async Task<IEnumerable<Asistencia>> ObtenerAsistenciasVoluntarioAsync(
            int voluntarioId,
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            return await _context.Asistencias
                .Where(a => a.VoluntarioId == voluntarioId &&
                           a.HoraEntrada.Date >= fechaInicio.Date &&
                           a.HoraEntrada.Date <= fechaFin.Date)
                .Include(a => a.Jornada)
                .OrderByDescending(a => a.HoraEntrada)
                .ToListAsync();
        }

        public async Task<IEnumerable<Asistencia>> ObtenerAsistenciasJornadaAsync(int jornadaId)
        {
            return await _context.Asistencias
                .Where(a => a.JornadaId == jornadaId)
                .Include(a => a.Voluntario)
                .OrderBy(a => a.HoraEntrada)
                .ToListAsync();
        }

        public async Task<IEnumerable<Asistencia>> ObtenerAsistenciasJornadaAsync(
            int jornadaId,
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            return await _context.Asistencias
                .Where(a => a.JornadaId == jornadaId &&
                           a.HoraEntrada.Date >= fechaInicio.Date &&
                           a.HoraEntrada.Date <= fechaFin.Date)
                .Include(a => a.Voluntario)
                .OrderBy(a => a.HoraEntrada)
                .ToListAsync();
        }

        public async Task<Voluntario?> ObtenerVoluntarioAsync(int voluntarioId)
        {
            return await _context.Voluntarios
                .Include(v => v.Rol)
                .FirstOrDefaultAsync(v => v.Id == voluntarioId);
        }

        public async Task<Jornada?> ObtenerJornadaAsync(int jornadaId)
        {
            return await _context.Jornadas
                .FirstOrDefaultAsync(j => j.Id == jornadaId);
        }

        public async Task<bool> ExisteVoluntarioAsync(int voluntarioId)
        {
            return await _context.Voluntarios.AnyAsync(v => v.Id == voluntarioId);
        }

        public async Task<bool> ExisteJornadaAsync(int jornadaId)
        {
            return await _context.Jornadas.AnyAsync(j => j.Id == jornadaId);
        }
    }
}
