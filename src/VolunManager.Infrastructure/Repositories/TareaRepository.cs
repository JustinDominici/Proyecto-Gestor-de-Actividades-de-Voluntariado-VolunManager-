using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly VolunManagerContext _context;

        public TareaRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync()
        {
            return await _context.Tareas
                .Include(t => t.Jornada)
                .Include(t => t.Voluntario)
                .ToListAsync();
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _context.Tareas
                .Include(t => t.Jornada)
                .Include(t => t.Voluntario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> ExisteJornadaAsync(int jornadaId)
        {
            return await _context.Jornadas.AnyAsync(j => j.Id == jornadaId);
        }

        public async Task<bool> ExisteVoluntarioAsync(int voluntarioId)
        {
            return await _context.Voluntarios.AnyAsync(v => v.Id == voluntarioId);
        }

        public async Task AddAsync(Tarea tarea)
        {
            await _context.Tareas.AddAsync(tarea);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return false;
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
