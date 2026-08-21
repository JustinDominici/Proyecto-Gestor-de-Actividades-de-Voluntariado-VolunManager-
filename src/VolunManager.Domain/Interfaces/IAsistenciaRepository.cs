using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    public interface IAsistenciaRepository
    {
        Task<IEnumerable<Asistencia>> GetAllAsync();

        Task<Asistencia?> GetByIdAsync(int id);

        Task<bool> ExisteVoluntarioAsync(int voluntarioId);

        Task<bool> ExisteJornadaAsync(int jornadaId);

        /// <summary>
        /// Evita que un mismo voluntario tenga dos registros de asistencia
        /// para la misma jornada.
        /// </summary>
        Task<bool> ExisteAsistenciaAsync(int voluntarioId, int jornadaId);

        Task AddAsync(Asistencia asistencia);

        Task<bool> DeleteAsync(int id);

        Task SaveChangesAsync();
    }
}
