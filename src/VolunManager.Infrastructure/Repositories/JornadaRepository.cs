using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class JornadaRepository : IJornadaRepository
    {
        private readonly VolunManagerContext _context;

        public JornadaRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Jornada>> GetAllAsync()
        {
            return await _context.Jornadas
                .Include(j => j.Voluntario)
                .ToListAsync();
        }

        public async Task<Jornada?> GetByIdAsync(int id)
        {
            return await _context.Jornadas
                .Include(j => j.Voluntario)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<bool> ExisteVoluntarioAsync(int voluntarioId)
        {
            return await _context.Voluntarios.AnyAsync(v => v.Id == voluntarioId);
        }

        public async Task AddAsync(Jornada jornada)
        {
            await _context.Jornadas.AddAsync(jornada);
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
