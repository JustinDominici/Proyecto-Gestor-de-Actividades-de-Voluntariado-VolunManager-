using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class VoluntarioRepository : IVoluntarioRepository
    {
        private readonly VolunManagerContext _context;

        public VoluntarioRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voluntario>> GetAllAsync()
        {
            return await _context.Voluntarios.ToListAsync();
        }

        public async Task<Voluntario?> GetByIdAsync(int id)
        {
            return await _context.Voluntarios.FindAsync(id);
        }

        public async Task<bool> ExisteCorreoAsync(string correo, int? idAExcluir = null)
        {
            return await _context.Voluntarios
                .AnyAsync(v => v.Correo == correo && (idAExcluir == null || v.Id != idAExcluir));
        }

        public async Task AddAsync(Voluntario voluntario)
        {
            await _context.Voluntarios.AddAsync(voluntario);
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
