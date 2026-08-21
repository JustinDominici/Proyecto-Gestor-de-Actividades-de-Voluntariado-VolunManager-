using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Asistencias;

namespace VolunManager.Application.Contract
{
    public interface IAsistenciaService
    {
        Task<ServiceResult<IEnumerable<AsistenciaDto>>> GetAllAsync();

        Task<ServiceResult<AsistenciaDto>> GetByIdAsync(int id);

        Task<ServiceResult<AsistenciaDto>> CreateAsync(AsistenciaCreateDto dto);

        Task<ServiceResult<bool>> UpdateAsync(int id, AsistenciaUpdateDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
