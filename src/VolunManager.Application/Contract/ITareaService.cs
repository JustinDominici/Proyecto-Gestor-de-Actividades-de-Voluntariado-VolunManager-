using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Tareas;

namespace VolunManager.Application.Contract
{
    public interface ITareaService
    {
        Task<ServiceResult<IEnumerable<TareaDto>>> GetAllAsync();

        Task<ServiceResult<TareaDto>> GetByIdAsync(int id);

        Task<ServiceResult<TareaDto>> CreateAsync(TareaCreateDto dto);

        Task<ServiceResult<bool>> UpdateAsync(int id, TareaUpdateDto dto);

        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
