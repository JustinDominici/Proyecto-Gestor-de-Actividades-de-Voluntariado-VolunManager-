using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Jornadas;

namespace VolunManager.Application.Contract
{
    public interface IJornadaService
    {
        Task<ServiceResult<IEnumerable<JornadaDto>>> GetAllAsync();

        Task<ServiceResult<JornadaDto>> GetByIdAsync(int id);

        Task<ServiceResult<JornadaDto>> CreateAsync(JornadaCreateDto dto);

        Task<ServiceResult<bool>> UpdateAsync(int id, JornadaUpdateDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}