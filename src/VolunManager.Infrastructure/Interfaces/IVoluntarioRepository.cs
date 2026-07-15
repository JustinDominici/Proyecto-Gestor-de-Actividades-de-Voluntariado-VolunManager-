using VolunManager.Infrastructure.Models;

namespace VolunManager.Infrastructure.Interfaces
{
    public interface IVoluntarioRepository
    {
        Task<IEnumerable<VoluntarioDto>> GetAllAsync();

        Task<VoluntarioDto?> GetByIdAsync(int id);

        Task<VoluntarioDto> CreateAsync(VoluntarioCreateDto voluntarioCreateDto);

        Task<bool> UpdateAsync(int id, VoluntarioUpdateDto voluntarioUpdateDto);

        Task<bool> DeleteAsync(int id);
    }
}
