using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    public interface ITareaRepository
    {
        Task<IEnumerable<Tarea>> GetAllAsync();

        Task<Tarea?> GetByIdAsync(int id);

        Task<bool> ExisteJornadaAsync(int jornadaId);

        Task<bool> ExisteVoluntarioAsync(int voluntarioId);

        Task AddAsync(Tarea tarea);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
