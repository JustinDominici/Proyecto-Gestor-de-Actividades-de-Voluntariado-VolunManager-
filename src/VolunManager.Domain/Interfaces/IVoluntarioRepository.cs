using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    /// <summary>
    /// Abstraccion de persistencia para Voluntario. Vive en Domain para que
    /// Application dependa solo de esta interfaz, nunca de Infrastructure.
    /// </summary>
    public interface IVoluntarioRepository
    {
        Task<IEnumerable<Voluntario>> GetAllAsync();

        Task<Voluntario?> GetByIdAsync(int id);

        Task<bool> ExisteCorreoAsync(string correo, int? idAExcluir = null);

        Task<bool> ExisteRolAsync(int rolId);

        /// <summary>
        /// Usado para proteger el borrado: no se puede eliminar un voluntario
        /// que todavia tiene tareas asignadas.
        /// </summary>
        Task<bool> TieneTareasAsociadasAsync(int id);

        /// <summary>
        /// Usado para proteger el borrado: no se puede eliminar un voluntario
        /// que todavia tiene asistencias registradas.
        /// </summary>
        Task<bool> TieneAsistenciasAsociadasAsync(int id);

        Task AddAsync(Voluntario voluntario);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
