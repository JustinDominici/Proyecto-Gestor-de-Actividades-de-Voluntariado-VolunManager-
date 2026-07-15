using VolunManager.Infrastructure.Models;

namespace VolunManager.Infrastructure.Interfaces
{
    public interface IJornadaRepository
    {
        Task<IEnumerable<JornadaDto>> GetAllAsync();

        Task<JornadaDto?> GetByIdAsync(int id);

        Task<JornadaDto> CreateAsync(JornadaCreateDto jornadaCreateDto);

        Task<bool> UpdateAsync(int id, JornadaUpdateDto jornadaUpdateDto);

        Task<bool> DeleteAsync(int id);
    }
}