using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    /// <summary>
    /// Abstraccion de persistencia para Jornada. Vive en Domain para que
    /// Application dependa solo de esta interfaz, nunca de Infrastructure.
    /// </summary>
    public interface IJornadaRepository
    {
        Task<IEnumerable<Jornada>> GetAllAsync();

        Task<Jornada?> GetByIdAsync(int id);

        Task<bool> ExisteVoluntarioAsync(int voluntarioId);

        Task AddAsync(Jornada jornada);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
