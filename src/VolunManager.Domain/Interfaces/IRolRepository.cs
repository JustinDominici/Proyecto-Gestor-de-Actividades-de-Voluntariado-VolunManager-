using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync();

        Task<Rol?> GetByIdAsync(int id);

        Task<bool> ExisteNombreAsync(string nombre, int? idAExcluir = null);

        /// <summary>
        /// Usado para proteger el borrado: no se puede eliminar un rol
        /// que todavia tiene voluntarios asignados.
        /// </summary>
        Task<bool> TieneVoluntariosAsociadosAsync(int id);

        Task AddAsync(Rol rol);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
