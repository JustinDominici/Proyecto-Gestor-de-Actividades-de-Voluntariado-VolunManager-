using Microsoft.EntityFrameworkCore;
using VolunManager.Domain.Entities;
using VolunManager.Domain.Interfaces;
using VolunManager.Infrastructure.Context;

namespace VolunManager.Infrastructure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly VolunManagerContext _context;

        public RolRepository(VolunManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.Voluntarios)
                .ToListAsync();
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.Voluntarios)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int? idAExcluir = null)
        {
            return await _context.Roles
                .AnyAsync(r => r.Nombre == nombre && (idAExcluir == null || r.Id != idAExcluir));
        }

        public async Task<bool> TieneVoluntariosAsociadosAsync(int id)
        {
            return await _context.Voluntarios.AnyAsync(v => v.RolId == id);
        }

        public async Task AddAsync(Rol rol)
        {
            await _context.Roles.AddAsync(rol);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return false;
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
